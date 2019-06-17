using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class EngagementClueProducer : BaseClueProducer<EngagementObject>
    {
        private readonly IClueFactory _factory;

        public EngagementClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(EngagementObject input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Activity, input.engagement.id.ToString(), accountId);
            
            var data = clue.Data.EntityData;

            data.Name = input.metadata.body ?? $"{input.engagement.type ?? "Engagement " + input.engagement.id}";
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.engagement.createdAt);
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.engagement.lastUpdated);

            data.Properties[HubSpotVocabulary.Engagement.Active] = input.engagement.active.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Engagement.TimeStamp] = input.engagement.timestamp.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v));
            data.Properties[HubSpotVocabulary.Engagement.Type] = input.engagement.type;

            if (input.engagement.ownerId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.Owns, input, s => s.engagement.ownerId.Value.ToString());

            if (input.engagement.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.engagement.portalId.ToString(), s => "Hubspot");


            return clue;
        }
    }
}
