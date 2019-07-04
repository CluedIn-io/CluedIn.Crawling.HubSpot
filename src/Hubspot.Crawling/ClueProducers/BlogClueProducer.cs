using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.HubSpot.Vocabularies;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Core.Utilities;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class BlogClueProducer : BaseClueProducer<Blog>
    {
        private readonly IClueFactory _factory;

        public BlogClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        protected override Clue MakeClueImpl(Blog input, Guid accountId)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.News, input.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.Description = input.description;

            if (input.created != null)
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.created.Value);

            if (input.updated != null)
                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updated.Value);

            if (input.root_url != null && Uri.TryCreate(input.root_url, UriKind.Absolute, out var uri))
                data.Uri = uri;

            data.Properties[HubSpotVocabulary.Blog.AllowComments] = input.allow_comments.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.Body] = input.body.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Blog.CaptchaAfterDays] = input.captcha_after_days.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.CaptchaAlways] = input.captcha_always.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.CategoryId] = input.category_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.CloseCommentsOlder] = input.close_comments_older.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.CommentDateFormat] = input.comment_date_format;
            data.Properties[HubSpotVocabulary.Blog.CommentModeration] = input.comment_moderation.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.CommentNotificationEmails] = input.comment_notification_emails.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Blog.CommentVerificationText] = input.comment_verification_text;
            data.Properties[HubSpotVocabulary.Blog.DailyNotificationEmailId] = input.daily_notification_email_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.DefaultGroupStyleId] = input.default_group_style_id;
            data.Properties[HubSpotVocabulary.Blog.DefaultNotificationFromName] = input.default_notification_from_name;
            data.Properties[HubSpotVocabulary.Blog.DefaultNotificationReplyTo] = input.default_notification_reply_to;
            data.Properties[HubSpotVocabulary.Blog.DeletedAt] = input.deleted_at.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.Domain] = input.domain;
            data.Properties[HubSpotVocabulary.Blog.EmailApiSubscriptionId] = input.email_api_subscription_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.EnableSocialAutoPublishing] = input.enable_social_auto_publishing.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.Header] = input.header.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Blog.HtmlFooter] = input.html_footer;
            data.Properties[HubSpotVocabulary.Blog.HtmlFooterIsShared] = input.html_footer_is_shared.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.HtmlHead] = input.html_head;
            data.Properties[HubSpotVocabulary.Blog.HtmlHeadIsShared] = input.html_head_is_shared.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.HtmlKeywords] = input.html_keywords.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Blog.HtmlTitle] = input.html_title;
            data.Properties[HubSpotVocabulary.Blog.InstantNotificationEmailId] = input.instant_notification_email_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ItemTemplateIsShared] = input.item_template_is_shared.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ItemTemplatePath] = input.item_template_path;
            data.Properties[HubSpotVocabulary.Blog.Language] = input.language;
            data.Properties[HubSpotVocabulary.Blog.LegacyGuid] = input.legacy_guid.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.LegacyModuleId] = input.legacy_module_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.LegacyTabId] = input.legacy_tab_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ListingLayoutId] = input.listing_layout_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ListingTemplatePath] = input.listing_template_path;
            data.Properties[HubSpotVocabulary.Blog.MonthFilterFormat] = input.month_filter_format;
            data.Properties[HubSpotVocabulary.Blog.MonthlyNotificationEmailId] = input.monthly_notification_email_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.PortalId] = input.portal_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.PostHtmlFooter] = input.post_html_footer;
            data.Properties[HubSpotVocabulary.Blog.PostHtmlHead] = input.post_html_head;
            data.Properties[HubSpotVocabulary.Blog.PostsPerListingPage] = input.posts_per_listing_page.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.PublicTitle] = input.public_title;
            data.Properties[HubSpotVocabulary.Blog.PublishDateFormat] = input.publish_date_format;
            data.Properties[HubSpotVocabulary.Blog.RssCustomFeed] = input.rss_custom_feed.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.RssDescription] = input.rss_description.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.RssItemFooter] = input.rss_item_footer.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.RssItemHeader] = input.rss_item_header.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ShowSumamryInEmails] = input.show_summary_in_emails.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ShowSummarInListing] = input.show_summary_in_listing.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.ShowSummaryInRss] = input.show_summary_in_rss.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.Slug] = input.slug;
            data.Properties[HubSpotVocabulary.Blog.SocialAccountTwitter] = input.social_account_twitter;
            data.Properties[HubSpotVocabulary.Blog.SocialPublishingSlug] = input.social_publishing_slug.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingDelicious] = input.social_sharing_delicious.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingDigg] = input.social_sharing_digg.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingEmail] = input.social_sharing_email.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingFacebookLike] = input.social_sharing_facebook_like.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingFacebookSend] = input.social_sharing_facebook_send.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingGoogleBuzz] = input.social_sharing_googlebuzz.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingGooglePlusOne] = input.social_sharing_googleplusone.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingLinkedIn] = input.social_sharing_linkedin.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingReddit] = input.social_sharing_reddit.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingStumbleUpon] = input.social_sharing_stumbleupon.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingTwitter] = input.social_sharing_twitter.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SocialSharingTwitterAccount] = input.social_sharing_twitter_account.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SubscriptionContactsProperty] = input.subscription_contacts_property;
            data.Properties[HubSpotVocabulary.Blog.SubscriptionEmailType] = input.subscription_email_type.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.SubscriptionFormGuid] = input.subscription_form_guid;
            data.Properties[HubSpotVocabulary.Blog.SubscriptionListsByType] = input.subscription_lists_by_type.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.Title] = input.title.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Blog.WeeklyNotificationEmailId] = input.weekly_notification_email_id.PrintIfAvailable();

            if (input.item_layout_id != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Template, EntityEdgeType.IsType, input.item_layout_id.Value.ToString());
                
            return clue;
        }

    }
}
