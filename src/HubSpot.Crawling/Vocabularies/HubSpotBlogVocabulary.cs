using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    public class HubSpotBlogVocabulary : SimpleVocabulary
    {
        public HubSpotBlogVocabulary()
        {
            VocabularyName = "HubSpot Blog";
            KeyPrefix      = "hubspot.blog";
            KeySeparator   = ".";
            Grouping       = EntityType.News;

            AllowComments                = Add(new VocabularyKey("AllowComments"));
            Body                         = Add(new VocabularyKey("Body", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            CaptchaAfterDays             = Add(new VocabularyKey("CaptchaAfterDays"));
            CaptchaAlways                = Add(new VocabularyKey("CaptchaAlways"));
            CategoryId                   = Add(new VocabularyKey("CategoryId", VocabularyKeyVisiblity.Hidden));
            CloseCommentsOlder = Add(new VocabularyKey("CloseCommentsOlder", VocabularyKeyVisiblity.Hidden));
            CommentDateFormat = Add(new VocabularyKey("CommentDateFormat", VocabularyKeyVisiblity.Hidden));
            CommentModeration            = Add(new VocabularyKey("CommentModeration"));
            CommentNotificationEmails    = Add(new VocabularyKey("CommentNotificationEmails", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            CommentVerificationText      = Add(new VocabularyKey("CommentVerificationText"));
            DailyNotificationEmailId     = Add(new VocabularyKey("DailyNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            DefaultGroupStyleId          = Add(new VocabularyKey("DefaultGroupStyleId", VocabularyKeyVisiblity.Hidden));
            DefaultNotificationFromName  = Add(new VocabularyKey("DefaultNotificationFromName"));
            DefaultNotificationReplyTo   = Add(new VocabularyKey("DefaultNotificationReplyTo"));
            DeletedAt                    = Add(new VocabularyKey("DeletedAt"));
            Domain                       = Add(new VocabularyKey("Domain"));
            EmailApiSubscriptionId = Add(new VocabularyKey("EmailApiSubscriptionId", VocabularyKeyVisiblity.Hidden));
            EnableSocialAutoPublishing   = Add(new VocabularyKey("EnableSocialAutoPublishing"));
            Header                       = Add(new VocabularyKey("Header", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            HtmlFooter = Add(new VocabularyKey("HtmlFooter", VocabularyKeyVisiblity.Hidden));
            HtmlFooterIsShared = Add(new VocabularyKey("HtmlFooterIsShared", VocabularyKeyVisiblity.Hidden));
            HtmlHead = Add(new VocabularyKey("HtmlHead", VocabularyKeyVisiblity.Hidden));
            HtmlHeadIsShared = Add(new VocabularyKey("HtmlHeadIsShared", VocabularyKeyVisiblity.Hidden));
            HtmlKeywords                 = Add(new VocabularyKey("HtmlKeywords", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            HtmlTitle                    = Add(new VocabularyKey("HtmlTitle"));
            InstantNotificationEmailId = Add(new VocabularyKey("InstantNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            ItemLayoutId                 = Add(new VocabularyKey("ItemLayoutId", VocabularyKeyVisiblity.Hidden));
            ItemTemplateIsShared = Add(new VocabularyKey("ItemTemplateIsShared", VocabularyKeyVisiblity.Hidden));
            ItemTemplatePath = Add(new VocabularyKey("ItemTemplatePath", VocabularyKeyVisiblity.Hidden));
            Language                     = Add(new VocabularyKey("Language"));
            LegacyGuid                   = Add(new VocabularyKey("LegacyGuid", VocabularyKeyVisiblity.Hidden));
            LegacyModuleId               = Add(new VocabularyKey("LegacyModuleId", VocabularyKeyVisiblity.Hidden));
            LegacyTabId                  = Add(new VocabularyKey("LegacyTabId", VocabularyKeyVisiblity.Hidden));
            ListingLayoutId              = Add(new VocabularyKey("ListingLayoutId", VocabularyKeyVisiblity.Hidden));
            ListingTemplatePath          = Add(new VocabularyKey("ListingTemplatePath"));
            MonthFilterFormat = Add(new VocabularyKey("MonthFilterFormat", VocabularyKeyVisiblity.Hidden));
            MonthlyNotificationEmailId = Add(new VocabularyKey("MonthlyNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            PortalId                     = Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            PostHtmlFooter = Add(new VocabularyKey("PostHtmlFooter", VocabularyKeyVisiblity.Hidden));
            PostsPerListingPage = Add(new VocabularyKey("PostsPerListingPage", VocabularyKeyVisiblity.Hidden));
            PublicTitle                  = Add(new VocabularyKey("PublicTitle"));
            PublishDateFormat = Add(new VocabularyKey("PublishDateFormat", VocabularyKeyVisiblity.Hidden));
            RootUrl = Add(new VocabularyKey("RootUrl", VocabularyKeyVisiblity.Hidden));
            RssCustomFeed = Add(new VocabularyKey("RssCustomFeed", VocabularyKeyDataType.Uri, VocabularyKeyVisiblity.Hidden));
            RssDescription               = Add(new VocabularyKey("RssDescription"));
            RssItemFooter = Add(new VocabularyKey("RssItemFooter", VocabularyKeyVisiblity.Hidden));
            RssItemHeader = Add(new VocabularyKey("RssItemHeader", VocabularyKeyVisiblity.Hidden));
            ShowSumamryInEmails          = Add(new VocabularyKey("ShowSumamryInEmails"));
            ShowSummarInListing          = Add(new VocabularyKey("ShowSummarInListing"));
            ShowSummaryInRss             = Add(new VocabularyKey("ShowSummaryInRss"));
            Slug = Add(new VocabularyKey("Slug", VocabularyKeyVisiblity.Hidden));
            SocialAccountTwitter         = Add(new VocabularyKey("SocialAccountTwitter"));
            SocialPublishingSlug         = Add(new VocabularyKey("SocialPublishingSlug"));
            SocialSharingDelicious       = Add(new VocabularyKey("SocialSharingDelicious"));
            SocialSharingDigg            = Add(new VocabularyKey("SocialSharingDigg"));
            SocialSharingEmail           = Add(new VocabularyKey("SocialSharingEmail"));
            SocialSharingFacebookLike    = Add(new VocabularyKey("SocialSharingFacebookLike"));
            SocialSharingFacebookSend    = Add(new VocabularyKey("SocialSharingFacebookSend"));
            SocialSharingGoogleBuzz      = Add(new VocabularyKey("SocialSharingGoogleBuzz"));
            SocialSharingGooglePlusOne   = Add(new VocabularyKey("SocialSharingGooglePlusOne"));
            SocialSharingLinkedIn        = Add(new VocabularyKey("SocialSharingLinkedIn"));
            SocialSharingReddit          = Add(new VocabularyKey("SocialSharingReddit"));
            SocialSharingStumbleUpon     = Add(new VocabularyKey("SocialSharingStumbleUpon"));
            SocialSharingTwitter         = Add(new VocabularyKey("SocialSharingTwitter"));
            SocialSharingTwitterAccount  = Add(new VocabularyKey("SocialSharingTwitterAccount"));
            SubscriptionContactsProperty = Add(new VocabularyKey("SubscriptionContactsProperty", VocabularyKeyVisiblity.Hidden));
            SubscriptionEmailType        = Add(new VocabularyKey("SubscriptionEmailType"));
            SubscriptionFormGuid = Add(new VocabularyKey("SubscriptionFormGuid", VocabularyKeyVisiblity.Hidden));
            SubscriptionListsByType = Add(new VocabularyKey("SubscriptionListsByType", VocabularyKeyVisiblity.Hidden));
            Title                        = Add(new VocabularyKey("Title"));
            WeeklyNotificationEmailId = Add(new VocabularyKey("WeeklyNotificationEmailId", VocabularyKeyVisiblity.Hidden));
            PostHtmlHead = Add(new VocabularyKey("PostHtmlHead", VocabularyKeyVisiblity.Hidden));

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
