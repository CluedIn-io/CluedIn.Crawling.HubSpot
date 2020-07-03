using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.HubSpot.Vocabularies;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class BlogPostClueProducer : BaseClueProducer<BlogPost>
    {
        private readonly IClueFactory _factory;
        private IHubSpotFileFetcher _fileFetcher;

        public BlogPostClueProducer(IClueFactory factory, IHubSpotFileFetcher fileFetcher)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _fileFetcher = fileFetcher ?? throw new ArgumentNullException(nameof(fileFetcher));
        }

        protected override Clue MakeClueImpl(BlogPost input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.News, input.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);

            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.Description = input.post_summary;

            if (input.created != null)
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.created.Value);

            if (input.updated != null)
                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updated.Value);

            if (!string.IsNullOrEmpty(input.published_url))
                data.Uri = new Uri(input.published_url);

            if (input.url != null && Uri.TryCreate(input.url, UriKind.Absolute, out var uri))
                data.Uri = uri;

            data.Properties[HubSpotVocabulary.BlogPost.Archived] = input.archived.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.BlogAuthor] = input.blog_author.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.Campaign] = input.campaign.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.CampaignName] = input.campaign_name.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.ClonedFrom] = input.cloned_from.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.CommentCount] = input.comment_count.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.DeletedAt] = input.deleted_at.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v.Value));
            data.Properties[HubSpotVocabulary.BlogPost.FeaturedImage] = input.featured_image;
            data.Properties[HubSpotVocabulary.BlogPost.FooterHtml] = input.footer_html;
            data.Properties[HubSpotVocabulary.BlogPost.FreezeDate] = input.freeze_date.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v.Value));
            data.Properties[HubSpotVocabulary.BlogPost.HasUserChanges] = input.has_user_changes.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.HeadHtml] = input.head_html;
            data.Properties[HubSpotVocabulary.BlogPost.HtmlTitle] = input.html_title;
            data.Properties[HubSpotVocabulary.BlogPost.IsDraft] = input.is_draft.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.MetaDescription] = input.meta_description;
            data.Properties[HubSpotVocabulary.BlogPost.MetaKeywords] = input.meta_keywords.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.PerformableUrl] = input.performable_url.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.PostBody] = input.post_body;
            data.Properties[HubSpotVocabulary.BlogPost.PreviewImageSrc] = input.preview_image_src.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.PreviewKey] = input.preview_key;
            data.Properties[HubSpotVocabulary.BlogPost.ProcessingStatus] = input.processing_status;
            data.Properties[HubSpotVocabulary.BlogPost.PublishDate] = input.publish_date.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v.Value));
            data.Properties[HubSpotVocabulary.BlogPost.PublishImmediately] = input.publish_immediately.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.PublishedUrl] = input.published_url;
            data.Properties[HubSpotVocabulary.BlogPost.RssBody] = input.rss_body;
            data.Properties[HubSpotVocabulary.BlogPost.RssSummary] = input.rss_summary;
            data.Properties[HubSpotVocabulary.BlogPost.Slug] = input.slug;
            data.Properties[HubSpotVocabulary.BlogPost.State] = input.state;
            data.Properties[HubSpotVocabulary.BlogPost.StyleOverrideId] = input.style_override_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.BlogPost.Subcategory] = input.subcategory;
            data.Properties[HubSpotVocabulary.BlogPost.TopicsIds] = input.topic_ids.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.WidgetContainers] = input.widget_containers.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.WidgetContainers] = input.widgetcontainers.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.BlogPost.Widgets] = input.widgets.PrintIfAvailable();

            if (input.author_user_id != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.CreatedBy, input, input.author_user_id.Value.ToString());

            if (input.blog_author_id != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.CreatedBy, input, input.blog_author_id.Value.ToString());

            if (input.campaign != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.For, input, input.campaign.ToString());

            if (input.content_group_id != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Tag, EntityEdgeType.For, input, input.content_group_id.Value.ToString());

            if (input.portal_id != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portal_id.ToString(), s => "HubSpot");

            if (! String.IsNullOrWhiteSpace(input.featured_image))
            {
                var previewImagePart = _fileFetcher.FetchAsRawDataPart(input.featured_image, "/RawData/PreviewImage", "preview_{0}".FormatWith(clue.OriginEntityCode.Key));
                if (previewImagePart != null)
                {
                    clue.Details.RawData.Add(previewImagePart);
                    clue.Data.EntityData.PreviewImage = new ImageReferencePart(previewImagePart);
                }
            }

            return clue;
        }

    }
}
