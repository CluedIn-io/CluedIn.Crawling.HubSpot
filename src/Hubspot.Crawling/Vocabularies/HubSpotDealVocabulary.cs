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
            this.VocabularyName = "HubSpot Deal";
            this.KeyPrefix      = "hubspot.deal";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Sales.Deal;

            this.Associations                           = this.Add(new VocabularyKey("Associations", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.LastMeetingBooked                      = this.Add(new VocabularyKey("LastMeetingBooked", VocabularyKeyDataType.Text));
            this.LastMeetingBookedCampaign              = this.Add(new VocabularyKey("LastMeetingBookedCampaign", VocabularyKeyDataType.Text));
            this.LastMeetingBookedMedium                = this.Add(new VocabularyKey("LastMeetingBookedMedium", VocabularyKeyDataType.Text));
            this.LastMeetingBookedSource                = this.Add(new VocabularyKey("LastMeetingBookedSource", VocabularyKeyDataType.Text));
            this.DealInformationHubSpotOwner            = this.Add(new VocabularyKey("HubSpotOwner", VocabularyKeyDataType.Text));
            this.DealInformationLastContacted           = this.Add(new VocabularyKey("LastContacted", VocabularyKeyDataType.DateTime));
            this.DealInformationLastActivityDate        = this.Add(new VocabularyKey("LastActivityDate", VocabularyKeyDataType.DateTime));
            this.DealInformationNextActivityDate        = this.Add(new VocabularyKey("NextActivityDate", VocabularyKeyDataType.DateTime));
            this.DealInformationNumberoftimescontacted  = this.Add(new VocabularyKey("NumberOfTimesContacted", VocabularyKeyDataType.Integer));
            this.DealInformationNumberofSalesActivities = this.Add(new VocabularyKey("NumberOfSalesActivities", VocabularyKeyDataType.Integer));
            this.DealInformationHubSpotCreateDate       = this.Add(new VocabularyKey("HubSpotCreateDate", VocabularyKeyDataType.DateTime));
            this.DealInformationHubSpotTeam             = this.Add(new VocabularyKey("HubSpotTeam", VocabularyKeyDataType.Text));
            this.DealInformationDealType                = this.Add(new VocabularyKey("DealType", VocabularyKeyDataType.Text));
            this.DealInformationDealDescription         = this.Add(new VocabularyKey("DealDescription", VocabularyKeyDataType.Text));
            this.DealInformationNumberofContacts        = this.Add(new VocabularyKey("NumberofContacts", VocabularyKeyDataType.Integer));
            this.DealInformationClosedLostReason        = this.Add(new VocabularyKey("ClosedLostReason", VocabularyKeyDataType.Text));
            this.DealInformationClosedWonReason         = this.Add(new VocabularyKey("ClosedWonReason", VocabularyKeyDataType.Text));
            this.DealInformationLastModifiedDate        = this.Add(new VocabularyKey("LastModifiedDate", VocabularyKeyDataType.DateTime));
            this.DealInformationOwnerAssignedDate       = this.Add(new VocabularyKey("OwnerAssignedDate", VocabularyKeyDataType.Text));
            this.DealInformationDealName                = this.Add(new VocabularyKey("DealName", VocabularyKeyDataType.Text));
            this.DealInformationAmount                  = this.Add(new VocabularyKey("Amount", VocabularyKeyDataType.Text));
            this.DealInformationDealStage               = this.Add(new VocabularyKey("DealStage", VocabularyKeyDataType.Text));
            this.DealInformationPipeline                = this.Add(new VocabularyKey("Pipeline", VocabularyKeyDataType.Text));
            this.DealInformationCloseDate               = this.Add(new VocabularyKey("CloseDate", VocabularyKeyDataType.DateTime));
            this.DealInformationCreateDate              = this.Add(new VocabularyKey("CreateDate", VocabularyKeyDataType.DateTime));

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