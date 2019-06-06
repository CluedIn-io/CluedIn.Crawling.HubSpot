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
            this.VocabularyName = "HubSpot Blog Post";
            this.KeyPrefix      = "hubspot.blog.post";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.News;

            this.AddGroup("Hubspot Blog Details", group =>
            {
                this.Archived           = group.Add(new VocabularyKey("Archived", VocabularyKeyDataType.Boolean));
                this.BlogAuthor         = group.Add(new VocabularyKey("BlogAuthor", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.Campaign           = group.Add(new VocabularyKey("Campaign", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.CampaignName       = group.Add(new VocabularyKey("CampaignName"));
                this.ClonedFrom         = group.Add(new VocabularyKey("ClonedFrom", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.CommentCount       = group.Add(new VocabularyKey("CommentCount", VocabularyKeyDataType.Integer));
                this.DeletedAt          = group.Add(new VocabularyKey("DeletedAt", VocabularyKeyDataType.DateTime));
                this.FeaturedImage      = group.Add(new VocabularyKey("FeaturedImage"));
                this.FooterHtml = group.Add(new VocabularyKey("FooterHtml", VocabularyKeyVisiblity.Hidden));
                this.FreezeDate         = group.Add(new VocabularyKey("FreezeDate", VocabularyKeyDataType.DateTime));
                this.HasUserChanges     = group.Add(new VocabularyKey("HasUserChanges", VocabularyKeyDataType.Boolean));
                this.HeadHtml = group.Add(new VocabularyKey("HeadHtml", VocabularyKeyVisiblity.Hidden));
                this.HtmlTitle          = group.Add(new VocabularyKey("HtmlTitle"));
                this.IsDraft            = group.Add(new VocabularyKey("IsDraft", VocabularyKeyDataType.Boolean));
                this.MetaDescription    = group.Add(new VocabularyKey("MetaDescription"));
                this.MetaKeywords       = group.Add(new VocabularyKey("MetaKeywords", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.PerformableUrl     = group.Add(new VocabularyKey("PerformableUrl", VocabularyKeyDataType.Uri));
                this.PostBody           = group.Add(new VocabularyKey("PostBody"));
                this.PreviewImageSrc    = group.Add(new VocabularyKey("PreviewImageSrc"));
                this.PreviewKey = group.Add(new VocabularyKey("PreviewKey", VocabularyKeyVisiblity.Hidden));
                this.ProcessingStatus   = group.Add(new VocabularyKey("ProcessingStatus"));
                this.PublishDate        = group.Add(new VocabularyKey("PublishDate", VocabularyKeyDataType.DateTime));
                this.PublishImmediately = group.Add(new VocabularyKey("PublishImmediately", VocabularyKeyDataType.Boolean));
                this.PublishedUrl       = group.Add(new VocabularyKey("PublishedUrl", VocabularyKeyDataType.Uri));
                this.RssBody = group.Add(new VocabularyKey("RssBody", VocabularyKeyVisiblity.Hidden));
                this.RssSummary = group.Add(new VocabularyKey("RssSummary", VocabularyKeyVisiblity.Hidden));
                this.Slug = group.Add(new VocabularyKey("Slug", VocabularyKeyVisiblity.Hidden));
                this.State              = group.Add(new VocabularyKey("State"));
                this.StyleOverrideId    = group.Add(new VocabularyKey("StyleOverrideId", VocabularyKeyVisiblity.Hidden));
                this.Subcategory        = group.Add(new VocabularyKey("Subcategory"));
                this.TopicsIds          = group.Add(new VocabularyKey("TopicsIds", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.Url                = group.Add(new VocabularyKey("Url", VocabularyKeyDataType.Uri));
                this.WidgetContainers   = group.Add(new VocabularyKey("WidgetContainers", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.Widgets            = group.Add(new VocabularyKey("Widgets"));
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