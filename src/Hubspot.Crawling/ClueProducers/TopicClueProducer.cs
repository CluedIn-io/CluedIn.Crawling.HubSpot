using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class TopicClueProducer : BaseClueProducer<Topic>
    {
        private readonly IClueFactory _factory;

        public TopicClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Topic input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Topic, input.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);

            var data = clue.Data.EntityData;

            if (input.created != null)
            {
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.created.Value);
                data.Properties[HubSpotVocabulary.Topic.Created] = DateUtilities.EpochRef.AddMilliseconds(input.created.Value).ToString("o");
            }

            if (input.deletedAt != null)
                data.Properties[HubSpotVocabulary.Topic.DeletedAt] = input.deletedAt.ToString();
            if (input.description != null)
            {
                data.Description = input.description;

            }
            if (input.name != null)
            {
                data.Name = input.name;

            }
            if (input.portalId != null)
            {
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "Hubspot");
                data.Properties[HubSpotVocabulary.Topic.PortalId] = input.portalId.ToString();
            }

            if (input.slug != null)
                data.Properties[HubSpotVocabulary.Topic.Slug] = input.slug.ToString();
            if (input.updated != null)
            {
                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updated.Value);
                data.Properties[HubSpotVocabulary.Topic.Updated] = input.updated.ToString();
            }


            return clue;
        }
    }
}
