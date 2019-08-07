using System;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Agent.Jobs;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data;
using CluedIn.Core.Logging;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Indexing;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class FileMetaDataClueProducer : BaseClueProducer<FileMetaData>
    {
        private readonly IClueFactory _factory;
        private readonly ILogger _log;
        private readonly IHubSpotFileFetcher _fileFetcher;
        private readonly ApplicationContext _context;
        private readonly IAgentJobProcessorState<CrawlJobData> _state;

        public FileMetaDataClueProducer(IClueFactory factory, IHubSpotFileFetcher fileFetcher, ILogger log, ApplicationContext context, IAgentJobProcessorState<CrawlJobData> state)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _fileFetcher = fileFetcher ?? throw new ArgumentNullException(nameof(_fileFetcher));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        protected override Clue MakeClueImpl(FileMetaData input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Files.File, input.id.ToString(), accountId);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_002_Unknown_VocabularyKey_Used);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.DATA_001_File_MustBeIndexed);

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
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portal_id.Value.ToString(), s => "HubDpot");

            // TODO Figure out how to do file indexing
            if (input.name != null)
            {
                //try
                //{
                //    string filename = $"{input.name}.{input.extension}";
                //    var fileData = _fileFetcher.FetchAsBytes(input.url);

                //    if (_state.TaskFactory == null)
                //        _state.TaskFactory = new TaskFactory(); // BF - this will prevent indexing occuring if not present while testing

                //    var fileIndexer = new HubSpotFileIndexer(_state, _context);
                //    fileIndexer.Index(fileData, filename, clue).Wait();
                //}
                //catch (Exception exception)
                //{
                //    _log.Warn(() => "Could not download HubSpot File", exception);
                //}
            }

            return clue;
        }
    }
}
