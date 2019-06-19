using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class CampaignEventProducer : BaseClueProducer<CampaignEvent>
    {
        private readonly IClueFactory _factory;

        public CampaignEventProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(CampaignEvent input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Calendar.Event, input.id, accountId);
            
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            // TODO: No name have been specified
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.created);

            data.Properties[HubSpotVocabulary.CampaignEvent.AppName] = input.appName;
            data.Properties[HubSpotVocabulary.CampaignEvent.Browser] = input.browser.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property;
            data.Properties[HubSpotVocabulary.CampaignEvent.EmailCampaignId] = input.emailCampaignId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.CampaignEvent.Hmid] = input.hmid;
            data.Properties[HubSpotVocabulary.CampaignEvent.IpAddress] = input.ipAddress;
            data.Properties[HubSpotVocabulary.CampaignEvent.Location] = input.location.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property;
            data.Properties[HubSpotVocabulary.CampaignEvent.City] = input.location?.city;
            data.Properties[HubSpotVocabulary.CampaignEvent.Country] = input.location?.country;
            data.Properties[HubSpotVocabulary.CampaignEvent.State] = input.location?.state;
            data.Properties[HubSpotVocabulary.CampaignEvent.Recipient] = input.recipient;
            data.Properties[HubSpotVocabulary.CampaignEvent.Type] = input.type;
            data.Properties[HubSpotVocabulary.CampaignEvent.UserAgent] = input.userAgent;

            if (input.appId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Application, EntityEdgeType.PartOf, input, s => s.appId.Value.ToString());

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "HubSpot");

            return clue;
        }
    }
}
