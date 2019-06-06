using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    public class HubSpotBlogVocabulary : SimpleVocabulary
    {
        public HubSpotBlogVocabulary()
        {
            this.VocabularyName = "HubSpot Blog";
            this.KeyPrefix      = "hubspot.blog";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.News;

            this.AllowComments                = this.Add(new VocabularyKey("AllowComments"));
            this.Body                         = this.Add(new VocabularyKey("Body", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.CaptchaAfterDays             = this.Add(new VocabularyKey("CaptchaAfterDays"));
            this.CaptchaAlways                = this.Add(new VocabularyKey("CaptchaAlways"));
            this.CategoryId                   = this.Add(new VocabularyKey("CategoryId", VocabularyKeyVisiblity.Hidden));
            this.CloseCommentsOlder = this.Add(new VocabularyKey("CloseCommentsOlder", VocabularyKeyVisiblity.Hidden));
            this.CommentDateFormat = this.Add(new VocabularyKey("CommentDateFormat", VocabularyKeyVisiblity.Hidden));
            this.CommentModeration            = this.Add(new VocabularyKey("CommentModeration"));
            this.CommentNotificationEmails    = this.Add(new VocabularyKey("CommentNotificationEmails", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.CommentVerificationText      = this.Add(new VocabularyKey("CommentVerificationText"));
            this.DailyNotificationEmailId     = this.Add(new VocabularyKey("DailyNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            this.DefaultGroupStyleId          = this.Add(new VocabularyKey("DefaultGroupStyleId", VocabularyKeyVisiblity.Hidden));
            this.DefaultNotificationFromName  = this.Add(new VocabularyKey("DefaultNotificationFromName"));
            this.DefaultNotificationReplyTo   = this.Add(new VocabularyKey("DefaultNotificationReplyTo"));
            this.DeletedAt                    = this.Add(new VocabularyKey("DeletedAt"));
            this.Domain                       = this.Add(new VocabularyKey("Domain"));
            this.EmailApiSubscriptionId = this.Add(new VocabularyKey("EmailApiSubscriptionId", VocabularyKeyVisiblity.Hidden));
            this.EnableSocialAutoPublishing   = this.Add(new VocabularyKey("EnableSocialAutoPublishing"));
            this.Header                       = this.Add(new VocabularyKey("Header", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.HtmlFooter = this.Add(new VocabularyKey("HtmlFooter", VocabularyKeyVisiblity.Hidden));
            this.HtmlFooterIsShared = this.Add(new VocabularyKey("HtmlFooterIsShared", VocabularyKeyVisiblity.Hidden));
            this.HtmlHead = this.Add(new VocabularyKey("HtmlHead", VocabularyKeyVisiblity.Hidden));
            this.HtmlHeadIsShared = this.Add(new VocabularyKey("HtmlHeadIsShared", VocabularyKeyVisiblity.Hidden));
            this.HtmlKeywords                 = this.Add(new VocabularyKey("HtmlKeywords", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.HtmlTitle                    = this.Add(new VocabularyKey("HtmlTitle"));
            this.InstantNotificationEmailId = this.Add(new VocabularyKey("InstantNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            this.ItemLayoutId                 = this.Add(new VocabularyKey("ItemLayoutId", VocabularyKeyVisiblity.Hidden));
            this.ItemTemplateIsShared = this.Add(new VocabularyKey("ItemTemplateIsShared", VocabularyKeyVisiblity.Hidden));
            this.ItemTemplatePath = this.Add(new VocabularyKey("ItemTemplatePath", VocabularyKeyVisiblity.Hidden));
            this.Language                     = this.Add(new VocabularyKey("Language"));
            this.LegacyGuid                   = this.Add(new VocabularyKey("LegacyGuid", VocabularyKeyVisiblity.Hidden));
            this.LegacyModuleId               = this.Add(new VocabularyKey("LegacyModuleId", VocabularyKeyVisiblity.Hidden));
            this.LegacyTabId                  = this.Add(new VocabularyKey("LegacyTabId", VocabularyKeyVisiblity.Hidden));
            this.ListingLayoutId              = this.Add(new VocabularyKey("ListingLayoutId", VocabularyKeyVisiblity.Hidden));
            this.ListingTemplatePath          = this.Add(new VocabularyKey("ListingTemplatePath"));
            this.MonthFilterFormat = this.Add(new VocabularyKey("MonthFilterFormat", VocabularyKeyVisiblity.Hidden));
            this.MonthlyNotificationEmailId = this.Add(new VocabularyKey("MonthlyNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            this.PortalId                     = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.PostHtmlFooter = this.Add(new VocabularyKey("PostHtmlFooter", VocabularyKeyVisiblity.Hidden));
            this.PostsPerListingPage = this.Add(new VocabularyKey("PostsPerListingPage", VocabularyKeyVisiblity.Hidden));
            this.PublicTitle                  = this.Add(new VocabularyKey("PublicTitle"));
            this.PublishDateFormat = this.Add(new VocabularyKey("PublishDateFormat", VocabularyKeyVisiblity.Hidden));
            this.RootUrl = this.Add(new VocabularyKey("RootUrl", VocabularyKeyVisiblity.Hidden));
            this.RssCustomFeed = this.Add(new VocabularyKey("RssCustomFeed", VocabularyKeyDataType.Uri, VocabularyKeyVisiblity.Hidden));
            this.RssDescription               = this.Add(new VocabularyKey("RssDescription"));
            this.RssItemFooter = this.Add(new VocabularyKey("RssItemFooter", VocabularyKeyVisiblity.Hidden));
            this.RssItemHeader = this.Add(new VocabularyKey("RssItemHeader", VocabularyKeyVisiblity.Hidden));
            this.ShowSumamryInEmails          = this.Add(new VocabularyKey("ShowSumamryInEmails"));
            this.ShowSummarInListing          = this.Add(new VocabularyKey("ShowSummarInListing"));
            this.ShowSummaryInRss             = this.Add(new VocabularyKey("ShowSummaryInRss"));
            this.Slug = this.Add(new VocabularyKey("Slug", VocabularyKeyVisiblity.Hidden));
            this.SocialAccountTwitter         = this.Add(new VocabularyKey("SocialAccountTwitter"));
            this.SocialPublishingSlug         = this.Add(new VocabularyKey("SocialPublishingSlug"));
            this.SocialSharingDelicious       = this.Add(new VocabularyKey("SocialSharingDelicious"));
            this.SocialSharingDigg            = this.Add(new VocabularyKey("SocialSharingDigg"));
            this.SocialSharingEmail           = this.Add(new VocabularyKey("SocialSharingEmail"));
            this.SocialSharingFacebookLike    = this.Add(new VocabularyKey("SocialSharingFacebookLike"));
            this.SocialSharingFacebookSend    = this.Add(new VocabularyKey("SocialSharingFacebookSend"));
            this.SocialSharingGoogleBuzz      = this.Add(new VocabularyKey("SocialSharingGoogleBuzz"));
            this.SocialSharingGooglePlusOne   = this.Add(new VocabularyKey("SocialSharingGooglePlusOne"));
            this.SocialSharingLinkedIn        = this.Add(new VocabularyKey("SocialSharingLinkedIn"));
            this.SocialSharingReddit          = this.Add(new VocabularyKey("SocialSharingReddit"));
            this.SocialSharingStumbleUpon     = this.Add(new VocabularyKey("SocialSharingStumbleUpon"));
            this.SocialSharingTwitter         = this.Add(new VocabularyKey("SocialSharingTwitter"));
            this.SocialSharingTwitterAccount  = this.Add(new VocabularyKey("SocialSharingTwitterAccount"));
            this.SubscriptionContactsProperty = this.Add(new VocabularyKey("SubscriptionContactsProperty", VocabularyKeyVisiblity.Hidden));
            this.SubscriptionEmailType        = this.Add(new VocabularyKey("SubscriptionEmailType"));
            this.SubscriptionFormGuid = this.Add(new VocabularyKey("SubscriptionFormGuid", VocabularyKeyVisiblity.Hidden));
            this.SubscriptionListsByType = this.Add(new VocabularyKey("SubscriptionListsByType", VocabularyKeyVisiblity.Hidden));
            this.Title                        = this.Add(new VocabularyKey("Title"));
            this.WeeklyNotificationEmailId = this.Add(new VocabularyKey("WeeklyNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            this.PostHtmlHead = this.Add(new VocabularyKey("PostHtmlHead", VocabularyKeyVisiblity.Hidden));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey PostHtmlHead { get; private set; }
        public VocabularyKey SocialSharingEmail { get; private set; }
        public VocabularyKey SocialSharingFacebookLike { get; private set; }
        public VocabularyKey SocialSharingFacebookSend { get; private set; }
        public VocabularyKey SocialSharingGoogleBuzz { get; private set; }
        public VocabularyKey SocialSharingGooglePlusOne { get; private set; }
        public VocabularyKey SocialSharingLinkedIn { get; private set; }
        public VocabularyKey SocialSharingReddit { get; private set; }
        public VocabularyKey SocialSharingStumbleUpon { get; private set; }
        public VocabularyKey SocialSharingTwitter { get; private set; }
        public VocabularyKey SocialSharingTwitterAccount { get; private set; }
        public VocabularyKey SubscriptionContactsProperty { get; private set; }
        public VocabularyKey SubscriptionEmailType { get; private set; }
        public VocabularyKey SubscriptionFormGuid { get; private set; }
        public VocabularyKey SubscriptionListsByType { get; private set; }
        public VocabularyKey Title { get; private set; }
        public VocabularyKey WeeklyNotificationEmailId { get; private set; }


        public VocabularyKey AllowComments { get; private set; }
        public VocabularyKey Body { get; private set; }
        public VocabularyKey CaptchaAfterDays { get; private set; }
        public VocabularyKey CaptchaAlways { get; private set; }
        public VocabularyKey CategoryId { get; private set; }
        public VocabularyKey CloseCommentsOlder { get; private set; }
        public VocabularyKey CommentDateFormat { get; private set; }
        public VocabularyKey CommentModeration { get; private set; }
        public VocabularyKey CommentNotificationEmails { get; private set; }
        public VocabularyKey CommentVerificationText { get; private set; }
        public VocabularyKey DailyNotificationEmailId { get; private set; }
        public VocabularyKey DefaultGroupStyleId { get; private set; }
        public VocabularyKey DefaultNotificationFromName { get; private set; }
        public VocabularyKey DefaultNotificationReplyTo { get; private set; }
        public VocabularyKey DeletedAt { get; private set; }
        public VocabularyKey Domain { get; private set; }
        public VocabularyKey EmailApiSubscriptionId { get; private set; }
        public VocabularyKey EnableSocialAutoPublishing { get; private set; }
        public VocabularyKey Header { get; private set; }
        public VocabularyKey HtmlFooter { get; private set; }
        public VocabularyKey HtmlFooterIsShared { get; private set; }
        public VocabularyKey HtmlHead { get; private set; }
        public VocabularyKey HtmlHeadIsShared { get; private set; }
        public VocabularyKey HtmlKeywords { get; private set; }
        public VocabularyKey HtmlTitle { get; private set; }
        public VocabularyKey InstantNotificationEmailId { get; private set; }
        public VocabularyKey ItemLayoutId { get; private set; }
        public VocabularyKey ItemTemplateIsShared { get; private set; }
        public VocabularyKey ItemTemplatePath { get; private set; }
        public VocabularyKey Language { get; private set; }
        public VocabularyKey LegacyGuid { get; private set; }
        public VocabularyKey LegacyModuleId { get; private set; }
        public VocabularyKey LegacyTabId { get; private set; }
        public VocabularyKey ListingLayoutId { get; private set; }
        public VocabularyKey ListingTemplatePath { get; private set; }
        public VocabularyKey MonthFilterFormat { get; private set; }
        public VocabularyKey MonthlyNotificationEmailId { get; private set; }
        public VocabularyKey PortalId { get; private set; }
        public VocabularyKey PostHtmlFooter { get; private set; }
        public VocabularyKey PostsPerListingPage { get; private set; }
        public VocabularyKey PublicTitle { get; private set; }
        public VocabularyKey PublishDateFormat { get; private set; }
        public VocabularyKey RootUrl { get; private set; }
        public VocabularyKey RssCustomFeed { get; private set; }
        public VocabularyKey RssDescription { get; private set; }
        public VocabularyKey RssItemFooter { get; private set; }
        public VocabularyKey RssItemHeader { get; private set; }
        public VocabularyKey ShowSumamryInEmails { get; private set; }
        public VocabularyKey ShowSummarInListing { get; private set; }
        public VocabularyKey ShowSummaryInRss { get; private set; }
        public VocabularyKey Slug { get; private set; }
        public VocabularyKey SocialAccountTwitter { get; private set; }
        public VocabularyKey SocialPublishingSlug { get; private set; }
        public VocabularyKey SocialSharingDelicious { get; private set; }
        public VocabularyKey SocialSharingDigg { get; private set; }

    }
}