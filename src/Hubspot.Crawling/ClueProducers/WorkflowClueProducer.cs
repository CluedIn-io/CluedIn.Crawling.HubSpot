using System;
using System.Globalization;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class WorkflowClueProducer : BaseClueProducer<Workflow>
    {
        private readonly IClueFactory _factory;

        public WorkflowClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Workflow input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Process, input.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);

            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updatedAt);

            data.Properties[HubSpotVocabulary.Workflow.Actions] = input.actions.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Workflow.AllowContactToTriggerMultipleTimes] = input.allowContactToTriggerMultipleTimes.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Workflow.Enabled] = input.enabled.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Workflow.GoalListIds] = input.goalListIds.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Workflow.InsertingAt] = input.insertedAt.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v));
            data.Properties[HubSpotVocabulary.Workflow.Listening] = input.listening.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Workflow.NutureTimeRange] = input.nurtureTimeRange.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Workflow.OnlyExecOnBizDays] = input.onlyExecOnBizDays.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Workflow.RecurringSetting] = input.recurringSetting.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Workflow.SuppressionListIds] = input.suppressionListIds.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Workflow.TriggerSets] = input.triggerSets.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Workflow.Type] = input.type;
            data.Properties[HubSpotVocabulary.Workflow.UnEnrollmentSetting] = input.unenrollmentSetting.PrintIfAvailable();

            if (input.lastUpdatedBy != null)
            {
                _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.ModifiedBy, input.lastUpdatedBy);
                var createdBy = new PersonReference(new EntityCode(EntityType.Person, HubSpotNameConstants.CodeOrigin, input.lastUpdatedBy));
                data.Authors.Add(createdBy);
            }

            if (input.originalAuthorUserId != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.CreatedBy, input.originalAuthorUserId.Value.ToString(CultureInfo.InvariantCulture));

            if (input.portalId != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.Value.ToString(CultureInfo.InvariantCulture), s => "HubSpot");

            return clue;
        }
    }
}
