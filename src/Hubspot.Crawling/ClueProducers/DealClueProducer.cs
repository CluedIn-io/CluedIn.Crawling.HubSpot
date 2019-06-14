using System;
using System.Globalization;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class DealClueProducer : BaseClueProducer<Deal>
    {
        private readonly IClueFactory _factory;

        public DealClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Deal value, Guid accountId)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, value.dealId.ToString(), accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            data.Name = value.dealId.Value.ToString(CultureInfo.InvariantCulture);

            if (value.associations != null)
            {
                if (value.associations != null)
                    foreach (var company in value.associations.associatedCompanyIds)
                    {
                        _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.PartOf, value, s => company.ToString());
                    }
                if (value.associations.associatedCompanyIds != null)
                    foreach (var company in value.associations.associatedCompanyIds)
                    {
                        _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.PartOf, value, s => company.ToString());
                    }

                if (value.associations.associatedDealIds != null)
                    foreach (var company in value.associations.associatedDealIds)
                    {
                        _factory.CreateIncomingEntityReference(clue, EntityType.Sales.Deal, EntityEdgeType.PartOf, value, s => company.ToString());
                    }

                if (value.associations.associatedVids != null)
                    foreach (var company in value.associations.associatedVids)
                    {
                        _factory.CreateIncomingEntityReference(clue, EntityType.Activity, EntityEdgeType.PartOf, value, s => company.ToString());
                    }
            }
            if (value.portalId != null)
            {
                if (data.OutgoingEdges.Count == 0)
                {
                    _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, value, s => s.portalId.ToString(), s => "Hubspot");
                }
                string url = string.Format("https://app.hubspot.com/sales/{0}/deal/{1}/", value.portalId, value.dealId);
                data.Uri = new Uri(url);
            }

            if (value.properties != null)
            {
                JObject allProperties = JObject.Parse(JsonUtility.Serialize(value.properties));

                foreach (var property in allProperties)
                {
                    string val = null;
                    DateTimeOffset? date = null;
                    if (property.Value["value"] != null && property.Value["value"].ToString() != null)
                    {
                        val = property.Value["value"].ToString();
                        if (long.TryParse(val, out long epoch))
                        {
                            try
                            {
                                date = DateUtilities.EpochRef.AddMilliseconds(epoch);
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (val == null)
                    {
                        continue;
                    }

                    if (property.Key == "hs_analytics_source")
                    {
                        data.Properties[HubSpotVocabulary.Deal.AnalyticsInformationOriginalSourceType] = val;
                    }
                    else if (property.Key == "hs_analytics_source_data_1")
                    {
                        data.Properties[HubSpotVocabulary.Deal.AnalyticsInformationOriginalSourceData1] = val;
                    }
                    else if (property.Key == "hs_analytics_source_data_2")
                    {
                        data.Properties[HubSpotVocabulary.Deal.AnalyticsInformationOriginalSourceData2] = val;
                    }
                    else if (property.Key == "hs_lastmodifieddate")
                    {
                        if (date.HasValue)
                            data.ModifiedDate = date.Value;
                    }
                    else if (property.Key == "hubspot_owner_assigneddate")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.DealInformationOwnerAssignedDate] = date.Value.ToString();
                    }
                    else if (property.Key == "dealname")
                    {
                        data.Name = val;
                    }
                    else if (property.Key == "amount")
                    {
                        if (value.Currency != null && !string.IsNullOrEmpty(val))
                            val = val + value.Currency;
                        data.Properties[HubSpotVocabulary.Deal.DealInformationAmount] = val;
                    }
                    else if (property.Key == "dealstage")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationDealStage] = val;
                    }
                    else if (property.Key == "pipeline")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationPipeline] = val;
                    }
                    else if (property.Key == "closedate")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.DealInformationCloseDate] = date.Value.ToString();
                    }
                    else if (property.Key == "createdate")
                    {
                        if (date.HasValue)
                            data.CreatedDate = date.Value;
                    }
                    else if (property.Key == "engagements_last_meeting_booked")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.LastMeetingBooked] = date.Value.ToString();
                    }
                    else if (property.Key == "engagements_last_meeting_booked_campaign")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.LastMeetingBookedCampaign] = date.Value.ToString();
                    }
                    else if (property.Key == "engagements_last_meeting_booked_medium")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.LastMeetingBookedMedium] = date.Value.ToString();
                    }
                    else if (property.Key == "engagements_last_meeting_booked_source")
                    {
                        data.Properties[HubSpotVocabulary.Deal.LastMeetingBookedSource] = val;
                    }
                    else if (property.Key == "hubspot_owner_id")
                    {
                        _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, value, s => val.ToString());
                    }
                    else if (property.Key == "notes_last_contacted")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.DealInformationLastContacted] = date.Value.ToString();
                    }
                    else if (property.Key == "notes_last_updated")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.DealInformationLastActivityDate] = date.Value.ToString();
                    }
                    else if (property.Key == "notes_next_activity_date")
                    {
                        if (date.HasValue)
                            data.Properties[HubSpotVocabulary.Deal.DealInformationNextActivityDate] = date.Value.ToString();
                    }
                    else if (property.Key == "num_contacted_notes")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationNumberoftimescontacted] = val;
                    }
                    else if (property.Key == "num_notes")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationNumberofSalesActivities] = val;
                    }
                    else if (property.Key == "hs_createdate")
                    {
                        if (date.HasValue)
                            data.CreatedDate = date.Value;
                    }
                    else if (property.Key == "hubspot_team_id")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationHubSpotTeam] = val;
                    }
                    else if (property.Key == "dealtype")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationDealType] = val;
                    }
                    else if (property.Key == "description")
                    {
                        data.Description = val;
                    }
                    else if (property.Key == "num_associated_contacts")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationNumberofContacts] = val;
                    }
                    else if (property.Key == "closed_lost_reason")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationClosedLostReason] = val;
                    }
                    else if (property.Key == "closed_won_reason")
                    {
                        data.Properties[HubSpotVocabulary.Deal.DealInformationClosedWonReason] = val;
                    }
                    else
                    {
                        data.Properties[string.Format("hubspot.deal.custom-{0}", property.Key)] = val;
                    }

                }

            }

            return clue;
        }
    }
}
