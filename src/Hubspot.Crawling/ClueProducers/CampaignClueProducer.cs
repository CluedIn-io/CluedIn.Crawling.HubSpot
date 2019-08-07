using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class CampaignClueProducer : BaseClueProducer<Campaign>
    {
        private readonly IClueFactory _factory;

        public CampaignClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Campaign input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Marketing.Campaign, input.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            if (input.name != null)
                data.Name = input.name;
            if (input.subject != null)
                data.Description = input.subject;

            if (input.appId != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Application, EntityEdgeType.PartOf, input, input.appId.Value.ToString());

            if (input.appName != null)
                data.Properties[HubSpotVocabulary.Campaign.AppName] = input.appName;
            if (input.contentId != null)
                data.Properties[HubSpotVocabulary.Campaign.ContentId] = input.contentId.ToString();
            if (input.counters != null)
            {
                data.Properties[HubSpotVocabulary.Campaign.Counters] = JsonUtility.Serialize(input.counters);
                if (input.counters.delivered != null)
                    data.Properties[HubSpotVocabulary.Campaign.Delivered] = input.counters.delivered.Value.ToString();
                if (input.counters.open != null)
                    data.Properties[HubSpotVocabulary.Campaign.Open] = input.counters.open.Value.ToString();
                if (input.counters.processed != null)
                    data.Properties[HubSpotVocabulary.Campaign.Processed] = input.counters.processed.Value.ToString();
                if (input.counters.sent != null)
                    data.Properties[HubSpotVocabulary.Campaign.Sent] = input.counters.sent.Value.ToString();
            }

            if (input.numIncluded != null)
                data.Properties[HubSpotVocabulary.Campaign.NumIncluded] = input.numIncluded.ToString();
            if (input.numQueued != null)
                data.Properties[HubSpotVocabulary.Campaign.NumQueued] = input.numQueued.ToString();
            if (input.subType != null)
                data.Properties[HubSpotVocabulary.Campaign.SubType] = input.subType;
            if (input.type != null)
                data.Properties[HubSpotVocabulary.Campaign.Type] = input.type;


            return clue;
        }
    }
}
