using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class EngagementActivityClueProducer : BaseClueProducer<EngagementResult>
    {
        private readonly IClueFactory _factory;

        public EngagementActivityClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(EngagementResult input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Activity, input.engagement.id.ToString(), accountId);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_001_Name_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_003_Author_Name_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_004_Invalid_EntityType);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_005_PreviewImage_RawData_MustBeSet);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_006_Created_Modified_Date_InFuture);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_007_Created_Modified_Date_InPast);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_008_Created_Modified_Date_UnixEpoch);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_009_Created_Modified_Date_MinDate);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_010_Created_Modified_Date_MaxDate);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_001_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_002_Unknown_VocabularyKey_Used);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_003_Value_ShouldNotBeQuoted);

            var data = clue.Data.EntityData;

            if (input.associations != null)
            {
                if (input.associations.companyIds != null)
                    foreach (var association in input.associations.companyIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.PartOf, input, a => association.ToString());

                if (input.associations.contactIds != null)
                    foreach (var association in input.associations.contactIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.PartOf, input, a => association.ToString());

                if (input.associations.dealIds != null)
                    foreach (var association in input.associations.dealIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Sales.Deal, EntityEdgeType.PartOf, input, a => association.ToString());

                if (input.associations.workflowIds != null)
                    foreach (var association in input.associations.workflowIds)
                        _factory.CreateIncomingEntityReference(clue, EntityType.Process, EntityEdgeType.PartOf, input, a => association.ToString());
            }

            if (input.scheduledTasks != null)
                foreach (var task in input.scheduledTasks)
                {
                    var r = JsonUtility.Deserialize<Dictionary<string, object>>(JsonUtility.Serialize(task));
                    foreach (var p in r)
                    {
                        if (p.Key == "uuid")
                            _factory.CreateIncomingEntityReference(clue, EntityType.Task, EntityEdgeType.PartOf, input, a => p.Value.ToString());
                    }
                }


            if (input.attachments != null)
                foreach (var association in input.attachments)
                //Download 
                {
                }

            if (input.engagement != null)
            {
                if (input.engagement.createdAt != null)
                {
                    if (long.TryParse(input.engagement.createdAt.ToString(), out long date))
                        data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                }

                if (input.engagement.lastUpdated != null)
                {
                    if (long.TryParse(input.engagement.lastUpdated.ToString(), out long date))
                        data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(date);
                }

                if (input.engagement.createdBy != null)
                    data.Authors.Add(new PersonReference(new EntityCode(EntityType.Person, HubSpotNameConstants.CodeOrigin, input.engagement.createdBy.Value)));

                if (input.engagement.modifiedBy != null)
                    data.LastChangedBy = new PersonReference(new EntityCode(EntityType.Person, HubSpotNameConstants.CodeOrigin, input.engagement.modifiedBy.Value));

                if (input.engagement.active != null)
                    data.Properties[HubSpotVocabulary.Engagement.Active] = input.engagement.active.ToString();
                if (input.engagement.type != null)
                    data.Properties[HubSpotVocabulary.Engagement.Type] = input.engagement.type.ToString();
                if (input.engagement.timestamp != null)
                    if (long.TryParse(input.engagement.timestamp.ToString(), out long timestamp))
                        data.Properties[HubSpotVocabulary.Engagement.TimeStamp] = DateUtilities.EpochRef.AddMilliseconds(timestamp).ToString();

                if (input.engagement.ownerId != null)
                    _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.PartOf, input, a => a.engagement.ownerId.ToString());

                if (input.engagement.portalId != null)
                    _factory.CreateIncomingEntityReference(clue, EntityType.Organization, EntityEdgeType.PartOf, input, a => a.engagement.portalId.ToString());
            }

            if (input.metadata != null)
            {
                var metadata = JsonUtility.Deserialize<EngagementMetadata>(JsonUtility.Serialize(input.metadata));

                if (metadata.body != null)
                {
                    data.Name = Regex.Replace(metadata.body, "<.*?>", String.Empty);
                    data.Description = Regex.Replace(metadata.body, "<.*?>", String.Empty) ?? metadata.status;
                }
                if (metadata.durationMilliseconds != null)
                    data.Properties[HubSpotVocabulary.Engagement.Duration] = metadata.durationMilliseconds.ToString();
                if (metadata.forObjectType != null)
                    data.Properties[HubSpotVocabulary.Engagement.ObjectType] = metadata.forObjectType;
                if (metadata.status != null)
                    data.Properties[HubSpotVocabulary.Engagement.Status] = metadata.status;
            }
            if (data.Name == null)
                data.Name = input.engagement.type + " at " + data.CreatedDate.Value.ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);

            return clue;
        }
    }

}
