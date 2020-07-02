// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotBlogPostVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotBlogPostVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot blog post vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotBlogPostVocabulary : SimpleVocabulary
    {
        public HubSpotBlogPostVocabulary()
        {
            VocabularyName = "HubSpot Blog Post";
            KeyPrefix      = "hubspot.blog.post";
            KeySeparator   = ".";
            Grouping       = EntityType.News;

            AddGroup("HubSpot Blog Details", group =>
            {
                Archived           = group.Add(new VocabularyKey("Archived", VocabularyKeyDataType.Boolean));
                BlogAuthor         = group.Add(new VocabularyKey("BlogAuthor", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                Campaign           = group.Add(new VocabularyKey("Campaign", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                CampaignName       = group.Add(new VocabularyKey("CampaignName"));
                ClonedFrom         = group.Add(new VocabularyKey("ClonedFrom", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                CommentCount       = group.Add(new VocabularyKey("CommentCount", VocabularyKeyDataType.Integer));
                DeletedAt          = group.Add(new VocabularyKey("DeletedAt", VocabularyKeyDataType.DateTime));
                FeaturedImage      = group.Add(new VocabularyKey("FeaturedImage"));
                FooterHtml = group.Add(new VocabularyKey("FooterHtml", VocabularyKeyVisibility.Hidden));
                FreezeDate         = group.Add(new VocabularyKey("FreezeDate", VocabularyKeyDataType.DateTime));
                HasUserChanges     = group.Add(new VocabularyKey("HasUserChanges", VocabularyKeyDataType.Boolean));
                HeadHtml = group.Add(new VocabularyKey("HeadHtml", VocabularyKeyVisibility.Hidden));
                HtmlTitle          = group.Add(new VocabularyKey("HtmlTitle"));
                IsDraft            = group.Add(new VocabularyKey("IsDraft", VocabularyKeyDataType.Boolean));
                MetaDescription    = group.Add(new VocabularyKey("MetaDescription"));
                MetaKeywords       = group.Add(new VocabularyKey("MetaKeywords", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                PerformableUrl     = group.Add(new VocabularyKey("PerformableUrl", VocabularyKeyDataType.Uri));
                PostBody           = group.Add(new VocabularyKey("PostBody"));
                PreviewImageSrc    = group.Add(new VocabularyKey("PreviewImageSrc"));
                PreviewKey = group.Add(new VocabularyKey("PreviewKey", VocabularyKeyVisibility.Hidden));
                ProcessingStatus   = group.Add(new VocabularyKey("ProcessingStatus"));
                PublishDate        = group.Add(new VocabularyKey("PublishDate", VocabularyKeyDataType.DateTime));
                PublishImmediately = group.Add(new VocabularyKey("PublishImmediately", VocabularyKeyDataType.Boolean));
                PublishedUrl       = group.Add(new VocabularyKey("PublishedUrl", VocabularyKeyDataType.Uri));
                RssBody = group.Add(new VocabularyKey("RssBody", VocabularyKeyVisibility.Hidden));
                RssSummary = group.Add(new VocabularyKey("RssSummary", VocabularyKeyVisibility.Hidden));
                Slug = group.Add(new VocabularyKey("Slug", VocabularyKeyVisibility.Hidden));
                State              = group.Add(new VocabularyKey("State"));
                StyleOverrideId    = group.Add(new VocabularyKey("StyleOverrideId", VocabularyKeyVisibility.Hidden));
                Subcategory        = group.Add(new VocabularyKey("Subcategory"));
                TopicsIds          = group.Add(new VocabularyKey("TopicsIds", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                Url                = group.Add(new VocabularyKey("Url", VocabularyKeyDataType.Uri));
                WidgetContainers   = group.Add(new VocabularyKey("WidgetContainers", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                Widgets            = group.Add(new VocabularyKey("Widgets"));
            });

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey PerformableUrl { get; private set; }
        public VocabularyKey PostBody { get; private set; }
        public VocabularyKey PreviewImageSrc { get; private set; }
        public VocabularyKey PreviewKey { get; private set; }
        public VocabularyKey ProcessingStatus { get; private set; }
        public VocabularyKey PublishDate { get; private set; }
        public VocabularyKey PublishImmediately { get; private set; }
        public VocabularyKey PublishedUrl { get; private set; }
        public VocabularyKey RssBody { get; private set; }
        public VocabularyKey RssSummary { get; private set; }
        public VocabularyKey Slug { get; private set; }
        public VocabularyKey State { get; private set; }
        public VocabularyKey StyleOverrideId { get; private set; }
        public VocabularyKey Subcategory { get; private set; }
        public VocabularyKey TopicsIds { get; private set; }
        public VocabularyKey Url { get; private set; }
        public VocabularyKey WidgetContainers { get; private set; }
        public VocabularyKey Widgets { get; private set; }


        public VocabularyKey Archived { get; private set; }
        public VocabularyKey BlogAuthor { get; private set; }
        public VocabularyKey Campaign { get; private set; }
        public VocabularyKey CampaignName { get; private set; }
        public VocabularyKey ClonedFrom { get; private set; }
        public VocabularyKey CommentCount { get; private set; }
        public VocabularyKey DeletedAt { get; private set; }
        public VocabularyKey FeaturedImage { get; private set; }
        public VocabularyKey FooterHtml { get; private set; }
        public VocabularyKey FreezeDate { get; private set; }
        public VocabularyKey HasUserChanges { get; private set; }
        public VocabularyKey HeadHtml { get; private set; }
        public VocabularyKey HtmlTitle { get; private set; }
        public VocabularyKey IsDraft { get; private set; }
        public VocabularyKey MetaDescription { get; private set; }
        public VocabularyKey MetaKeywords { get; private set; }



    }
}
