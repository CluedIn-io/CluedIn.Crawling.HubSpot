using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class SmtpTokenClueProducer : BaseClueProducer<SmtpToken>
    {
        private readonly IClueFactory _factory;

        public SmtpTokenClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(SmtpToken input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Activity, input.emailCampaignId.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Properties[HubSpotVocabulary.SmtpToken.AppId] = input.appId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SmtpToken.CampaignName] = input.campaignName;
            data.Properties[HubSpotVocabulary.SmtpToken.CreatedAt] = input.createdAt.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SmtpToken.CreatedBy] = input.createdBy;
            data.Properties[HubSpotVocabulary.SmtpToken.Deleted] = input.deleted.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SmtpToken.EmailCampaignId] = input.emailCampaignId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SmtpToken.PortalId] = input.portalId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SmtpToken.UserName] = input.userName;


            return clue;
        }
    }
}
