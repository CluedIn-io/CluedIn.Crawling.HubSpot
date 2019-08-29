// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotDealVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotDealVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot deal vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotDealVocabulary : SimpleVocabulary
    {
        public HubSpotDealVocabulary()
        {
            VocabularyName = "HubSpot Deal";
            KeyPrefix      = "hubspot.deal";
            KeySeparator   = ".";
            Grouping       = EntityType.Sales.Deal;

            Associations                           = Add(new VocabularyKey("Associations", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            LastMeetingBooked                      = Add(new VocabularyKey("LastMeetingBooked", VocabularyKeyDataType.Text));
            LastMeetingBookedCampaign              = Add(new VocabularyKey("LastMeetingBookedCampaign", VocabularyKeyDataType.Text));
            LastMeetingBookedMedium                = Add(new VocabularyKey("LastMeetingBookedMedium", VocabularyKeyDataType.Text));
            LastMeetingBookedSource                = Add(new VocabularyKey("LastMeetingBookedSource", VocabularyKeyDataType.Text));
            DealInformationHubSpotOwner            = Add(new VocabularyKey("HubSpotOwner", VocabularyKeyDataType.Text));
            DealInformationLastContacted           = Add(new VocabularyKey("LastContacted", VocabularyKeyDataType.DateTime));
            DealInformationLastActivityDate        = Add(new VocabularyKey("LastActivityDate", VocabularyKeyDataType.DateTime));
            DealInformationNextActivityDate        = Add(new VocabularyKey("NextActivityDate", VocabularyKeyDataType.DateTime));
            DealInformationNumberoftimescontacted  = Add(new VocabularyKey("NumberOfTimesContacted", VocabularyKeyDataType.Integer));
            DealInformationNumberofSalesActivities = Add(new VocabularyKey("NumberOfSalesActivities", VocabularyKeyDataType.Integer));
            DealInformationHubSpotCreateDate       = Add(new VocabularyKey("HubSpotCreateDate", VocabularyKeyDataType.DateTime));
            DealInformationHubSpotTeam             = Add(new VocabularyKey("HubSpotTeam", VocabularyKeyDataType.Text));
            DealInformationDealType                = Add(new VocabularyKey("DealType", VocabularyKeyDataType.Text));
            DealInformationDealDescription         = Add(new VocabularyKey("DealDescription", VocabularyKeyDataType.Text));
            DealInformationNumberofContacts        = Add(new VocabularyKey("NumberofContacts", VocabularyKeyDataType.Integer));
            DealInformationClosedLostReason        = Add(new VocabularyKey("ClosedLostReason", VocabularyKeyDataType.Text));
            DealInformationClosedWonReason         = Add(new VocabularyKey("ClosedWonReason", VocabularyKeyDataType.Text));
            DealInformationLastModifiedDate        = Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.DateTime));
            DealInformationOwnerAssignedDate       = Add(new VocabularyKey("OwnerAssignedDate", VocabularyKeyDataType.Text));
            DealInformationDealName                = Add(new VocabularyKey("DealName", VocabularyKeyDataType.Text));
            DealInformationAmount                  = Add(new VocabularyKey("Amount", VocabularyKeyDataType.Text));
            DealInformationDealStage               = Add(new VocabularyKey("DealStage", VocabularyKeyDataType.Text));
            DealInformationPipeline                = Add(new VocabularyKey("Pipeline", VocabularyKeyDataType.Text));
            DealInformationCloseDate               = Add(new VocabularyKey("CloseDate", VocabularyKeyDataType.DateTime));
            DealInformationCreateDate              = Add(new VocabularyKey("CreateDate", VocabularyKeyDataType.DateTime));

            AddGroup("HubSpot Deal Analytics Information Details", group =>
            {
                AnalyticsInformationOriginalSourceType  = group.Add(new VocabularyKey("OriginalSourceType", VocabularyKeyDataType.Text));
                AnalyticsInformationOriginalSourceData1 = group.Add(new VocabularyKey("OriginalSourceData1", VocabularyKeyDataType.Text));
                AnalyticsInformationOriginalSourceData2 = group.Add(new VocabularyKey("OriginalSourceData2", VocabularyKeyDataType.Text));
            });
          
        }

        public VocabularyKey Associations { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceType { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceData1 { get; private set; }

        public VocabularyKey AnalyticsInformationOriginalSourceData2 { get; private set; }

        public VocabularyKey DealInformationLastModifiedDate { get; private set; }

        public VocabularyKey DealInformationOwnerAssignedDate { get; private set; }

        public VocabularyKey DealInformationDealName { get; private set; }

        public VocabularyKey DealInformationAmount { get; private set; }

        public VocabularyKey DealInformationDealStage { get; private set; }

        public VocabularyKey DealInformationPipeline { get; private set; }

        public VocabularyKey DealInformationCloseDate { get; private set; }

        public VocabularyKey DealInformationCreateDate { get; private set; }

        public VocabularyKey LastMeetingBooked { get; private set; }

        public VocabularyKey LastMeetingBookedCampaign { get; private set; }

        public VocabularyKey LastMeetingBookedMedium { get; private set; }

        public VocabularyKey LastMeetingBookedSource { get; private set; }

        public VocabularyKey DealInformationHubSpotOwner { get; private set; }

        public VocabularyKey DealInformationLastContacted { get; private set; }

        public VocabularyKey DealInformationLastActivityDate { get; private set; }

        public VocabularyKey DealInformationNextActivityDate { get; private set; }

        public VocabularyKey DealInformationNumberoftimescontacted { get; private set; }

        public VocabularyKey DealInformationNumberofSalesActivities { get; private set; }

        public VocabularyKey DealInformationHubSpotCreateDate { get; private set; }

        public VocabularyKey DealInformationHubSpotTeam { get; private set; }

        public VocabularyKey DealInformationDealType { get; private set; }

        public VocabularyKey DealInformationDealDescription { get; private set; }

        public VocabularyKey DealInformationNumberofContacts { get; private set; }

        public VocabularyKey DealInformationClosedLostReason { get; private set; }

        public VocabularyKey DealInformationClosedWonReason { get; private set; }

    }
}
