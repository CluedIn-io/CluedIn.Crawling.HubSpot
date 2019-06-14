using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class DealPipelineClueProducer : BaseClueProducer<DealPipeline>
    {
        private readonly IClueFactory _factory;

        public DealPipelineClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(DealPipeline value, Guid accountId)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, value.pipelineId, accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            data.Name = value.label;
            data.Uri = new Uri($"https://app.hubspot.com/sales-products-settings/{value.portalId}/deals/{value.pipelineId}");

            data.Properties[HubSpotVocabulary.DealPipeline.Active] = value.active.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.DealPipeline.DisplayOrder] = value.displayOrder.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.DealPipeline.PipelineId] = value.pipelineId;

            if (value.stages != null)
            {
                foreach (var stage in value.stages)
                {
                    // TODO: Do not create multiple clues in subjects
                    var stageClue = CreateStageClue(stage, accountId);
                    //this.state.Status.Statistics.Tasks.IncrementTaskCount();
                    //this.state.Status.Statistics.Tasks.IncrementQueuedCount();

                    // TODO Verify how we handle multiple clues 
                    _factory.CreateIncomingEntityReference(clue, EntityType.ProcessStage, EntityEdgeType.PartOf, stage, s => s.stageId);
                }
            }

            _factory.CreateIncomingEntityReference(clue, EntityType.Provider.Root, EntityEdgeType.Parent, value, s => "HubSpot");


            return clue;
        }

        public Clue CreateStageClue(Stage value, Guid accountId)
        {
            var clue = _factory.Create(EntityType.ProcessStage, value.stageId, accountId);
            var data = clue.Data.EntityData;

            data.Name = value.label;

            data.Properties[HubSpotVocabulary.Stage.Active] = value.active.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Stage.DisplayOrder] = value.displayOrder.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Stage.Label] = value.label;
            data.Properties[HubSpotVocabulary.Stage.Probability] = value.probability.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Stage.ClosedWon] = value.closedWon.PrintIfAvailable();

            return clue;
        }

    }
}
