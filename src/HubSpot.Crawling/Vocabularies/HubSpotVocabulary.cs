// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    public static class HubSpotVocabulary
    {
        static HubSpotVocabulary()
        {
            Blog                      = new HubSpotBlogVocabulary();
            BlogPost                  = new HubSpotBlogPostVocabulary();
            Broadcast                 = new HubSpotBroadcastVocabulary();
            CalendarEvent             = new HubSpotCalendarEventVocabulary();
            CampaignEvent             = new HubSpotCampaignEventVocabulary();
            Campaign                  = new HubSpotCampaignVocabulary();
            Channel                   = new HubSpotChannelVocabulary();
            Company                   = new HubSpotCompanyVocabulary();
            ContactList               = new HubSpotContactListVocabulary();
            PropertyDefinition        = new HubSpotPropertyDefinitionVocabulary();
            Contact                   = new HubSpotContactVocabulary();
            DealPipeline              = new HubSpotDealPipelineVocabulary();
            Deal                      = new HubSpotDealVocabulary();
            Domain                    = new HubSpotDomainVocabulary();
            Engagement                = new HubSpotEngagementVocabulary();
            FileMetaData              = new HubSpotFileMetaDataVocabulary();
            Form                      = new HubSpotFormVocabulary();
            Keyword                   = new HubSpotKeywordVocabulary();
            Owner                     = new HubSpotOwnerVocabulary();
            SiteMap                   = new HubSpotSiteMapVocabulary();
            SmtpToken                 = new HubSpotSmtpTokenVocabulary();
            Stage                     = new HubSpotStageMapVocabulary();
            Template                  = new HubSpotTemplateVocabulary();
            Topic                     = new HubSpotTopicVocabulary();
            UrlMapping                = new HubSpotUrlMappingVocabulary();
            Workflow                  = new HubSpotWorkflowVocabulary();
            Email                     = new HubSpotEmailVocabulary();
            Note                      = new HubSpotNoteVocabulary();
            Call                      = new HubSpotCallVocabulary();
            Meeting                   = new HubSpotMeetingVocabulary();
            Task                      = new HubSpotTaskVocabulary();
            EmailPerson               = new HubSpotEmailPersonVocabulary();
            Product                   = new HubSpotProductVocabulary();
            LineItem                  = new HubSpotLineItemVocabulary();
            Ticket                    = new HubSpotTicketVocabulary();
            Table                     = new HubSpotTableVocabulary();
            Row                       = new HubSpotRowVocabulary();
        }

        public static HubSpotBlogVocabulary Blog { get; private set; }
        public static HubSpotBlogPostVocabulary BlogPost { get; private set; }
        public static HubSpotBroadcastVocabulary Broadcast { get; private set; }
        public static HubSpotCalendarEventVocabulary CalendarEvent { get; private set; }
        public static HubSpotCampaignEventVocabulary CampaignEvent { get; private set; }
        public static HubSpotCampaignVocabulary Campaign { get; private set; }
        public static HubSpotChannelVocabulary Channel { get; private set; }
        public static HubSpotCompanyVocabulary Company { get; private set; }
        public static HubSpotContactListVocabulary ContactList { get; private set; }
        public static HubSpotPropertyDefinitionVocabulary PropertyDefinition { get; private set; }
        public static HubSpotContactVocabulary Contact { get; private set; }
        public static HubSpotDealPipelineVocabulary DealPipeline { get; private set; }
        public static HubSpotDealVocabulary Deal { get; private set; }
        public static HubSpotDomainVocabulary Domain { get; private set; }
        public static HubSpotEngagementVocabulary Engagement { get; private set; }
        public static HubSpotFileMetaDataVocabulary FileMetaData { get; private set; }
        public static HubSpotFormVocabulary Form { get; private set; }
        public static HubSpotKeywordVocabulary Keyword { get; private set; }
        public static HubSpotOwnerVocabulary Owner { get; private set; }
        public static HubSpotSiteMapVocabulary SiteMap { get; private set; }
        public static HubSpotSmtpTokenVocabulary SmtpToken { get; private set; }
        public static HubSpotStageMapVocabulary Stage { get; private set; }
        public static HubSpotTemplateVocabulary Template { get; private set; }
        public static HubSpotTopicVocabulary Topic { get; private set; }
        public static HubSpotUrlMappingVocabulary UrlMapping { get; private set; }
        public static HubSpotWorkflowVocabulary Workflow { get; private set; }
        public static HubSpotEmailVocabulary Email { get; private set; }
        public static HubSpotNoteVocabulary Note { get; private set; }
        public static HubSpotCallVocabulary Call { get; private set; }
        public static HubSpotMeetingVocabulary Meeting { get; private set; }
        public static HubSpotTaskVocabulary Task { get; private set; }
        public static HubSpotEmailPersonVocabulary EmailPerson { get; private set; }
        public static HubSpotProductVocabulary Product { get; private set; }
        public static HubSpotLineItemVocabulary LineItem { get; private set; }
        public static HubSpotTicketVocabulary Ticket { get; private set; }
        public static HubSpotTableVocabulary Table { get; private set; }
        public static HubSpotRowVocabulary Row { get; private set; }
    }
}