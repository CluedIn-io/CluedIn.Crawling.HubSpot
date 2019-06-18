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
    public class FormClueProducer : BaseClueProducer<Form>
    {
        private readonly IClueFactory _factory;

        public FormClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Form input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Form, input.guid, accountId);

            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.createdAt);
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updatedAt);

            data.Properties[HubSpotVocabulary.Form.Action] = input.action;
            data.Properties[HubSpotVocabulary.Form.CssClass] = input.cssClass;
            data.Properties[HubSpotVocabulary.Form.Deletable] = input.deletable.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Form.FollowUpId] = input.followUpId;
            data.Properties[HubSpotVocabulary.Form.FormFieldGroups] = input.formFieldGroups.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Form.Guid] = input.guid;
            data.Properties[HubSpotVocabulary.Form.IgnoreCurrentValues] = input.ignoreCurrentValues.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Form.LeadNuturingCampaignId] = input.leadNurturingCampaignId;
            data.Properties[HubSpotVocabulary.Form.MetaData] = input.metaData.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.Form.Method] = input.method;
            data.Properties[HubSpotVocabulary.Form.MigratedFrom] = input.migratedFrom;
            data.Properties[HubSpotVocabulary.Form.NotifyRecipients] = input.notifyRecipients;
            data.Properties[HubSpotVocabulary.Form.PerformableHtml] = input.performableHtml;
            data.Properties[HubSpotVocabulary.Form.Redirect] = input.redirect;
            data.Properties[HubSpotVocabulary.Form.SubmitText] = input.submitText;

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "HubSpot");


            return clue;
        }
    }
}
