using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class FileMetaDataClueProducer : BaseClueProducer<FileMetaData>
    {
        private readonly IClueFactory _factory;

        public FileMetaDataClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(FileMetaData input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, input.id.ToString(), accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.created);
            data.DocumentSize = input.size;

            if (input.url != null)
                data.Uri = new Uri(input.url);

            data.Properties[HubSpotVocabulary.FileMetaData.AltKey] = input.alt_key;
            data.Properties[HubSpotVocabulary.FileMetaData.AltKeyHash] = input.alt_key_hash;
            data.Properties[HubSpotVocabulary.FileMetaData.AltUrl] = input.alt_url;
            data.Properties[HubSpotVocabulary.FileMetaData.Archived] = input.archived.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.DeletedAt] = input.deleted_at.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.Extension] = input.extension;
            data.Properties[HubSpotVocabulary.FileMetaData.FolderId] = input.folder_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.Height] = input.height.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.IsCtaImage] = input.is_cta_image.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.Meta] = input.meta.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.FileMetaData.PortalId] = input.portal_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.Title] = input.title;
            data.Properties[HubSpotVocabulary.FileMetaData.Type] = input.type;
            data.Properties[HubSpotVocabulary.FileMetaData.Updated] = input.updated.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.EmbedUrl] = input.url;
            data.Properties[HubSpotVocabulary.FileMetaData.EditUrl] = input.url;
            data.Properties[HubSpotVocabulary.FileMetaData.Version] = input.version.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.FileMetaData.Width] = input.width.PrintIfAvailable();

            if (input.portal_id != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portal_id.Value.ToString(), s => "HubDpot");

            // TODO Figure out how to do file indexing
            //if (value.name != null)
            //{
            //    try
            //    {
            //        using (var tempFile = new TemporaryFile($"{value.name}.{value.extension}"))
            //        {
            //            using (var webClient = new WebClient())
            //            {
            //                using (var stream = webClient.OpenRead(value.url))
            //                {
            //                    using (var fs = new FileStream(tempFile.FileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write))
            //                    {
            //                        stream.CopyTo(fs);
            //                    }
            //                }
            //            }

            //            FileCrawlingUtility.IndexFile(tempFile, clue.Data, clue, this.state, this.context);
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        this.state.Log.Warn(() => "Could not download HubSpot File", exception);
            //    }
            //}

            return clue;
        }
    }
}
