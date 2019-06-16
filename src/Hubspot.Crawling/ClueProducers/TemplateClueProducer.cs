using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class TemplateClueProducer : BaseClueProducer<Template>
    {
        private readonly IClueFactory _factory;

        public TemplateClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Template input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, input.id.ToString(), accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;
            data.Name = input.label;
            data.Description = input.source;
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updated);

            data.Properties[HubSpotVocabulary.Template.CategoryId] = input.category_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.CdnMinifiedUrl] = input.cdn_minified_url;
            data.Properties[HubSpotVocabulary.Template.CdnUrl] = input.cdn_url;
            data.Properties[HubSpotVocabulary.Template.DeletedAt] = input.deleted_at.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.Folder] = input.folder;
            data.Properties[HubSpotVocabulary.Template.GeneratedFromLayoutId] = input.generated_from_layout_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.IsAvailableForNewContent] = input.is_available_for_new_content.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.IsFromLayout] = input.is_from_layout.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.IsReadOnly] = input.is_read_only.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.LinkedInStyleLayout] = input.linked_style_id;
            data.Properties[HubSpotVocabulary.Template.Path] = input.path;
            data.Properties[HubSpotVocabulary.Template.PortalId] = input.portal_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.TemplateType] = input.template_type;
            data.Properties[HubSpotVocabulary.Template.ThumbnailWidth] = input.thumbnail_width.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Template.Type] = input.type;
            data.Properties[HubSpotVocabulary.Template.UpdatedBy] = input.updated_by;

            if (input.category_id != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Tag, EntityEdgeType.PartOf, input, s => s.category_id.Value.ToString());

            if (input.portal_id != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portal_id.ToString(), s => "HubSpot");



            return clue;
        }
    }
}
