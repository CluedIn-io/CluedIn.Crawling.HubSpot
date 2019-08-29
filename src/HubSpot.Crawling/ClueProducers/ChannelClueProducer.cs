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
    public class ChannelClueProducer : BaseClueProducer<Channel>
    {
        private readonly IClueFactory _factory;

        public ChannelClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Channel input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Channel, input.channelId, accountId);

            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.PROPERTIES_002_Unknown_VocabularyKey_Used);
            clue.ValidationRuleSuppressions.Add(Constants.Validation.Rules.METADATA_002_Uri_MustBeSet);


            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.createdAt);
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updatedAt);

            data.Properties[HubSpotVocabulary.Channel.AccountId] = input.accountGuid;
            data.Properties[HubSpotVocabulary.Channel.ChannelId] = input.channelGuid;
            data.Properties[HubSpotVocabulary.Channel.DataMap] = input.dataMap.PrintIfAvailable(JsonUtility.Serialize); 
            data.Properties[HubSpotVocabulary.Channel.Email] = input.dataMap?.email;
            data.Properties[HubSpotVocabulary.Channel.FirstName] = input.dataMap?.firstName;
            data.Properties[HubSpotVocabulary.Channel.FullName] = input.dataMap?.fullName;
            data.Properties[HubSpotVocabulary.Channel.LastName] = input.dataMap?.lastName;
            data.Properties[HubSpotVocabulary.Channel.PageCategory] = input.dataMap?.pageCategory;
            data.Properties[HubSpotVocabulary.Channel.PageId] = input.dataMap?.pageId;
            data.Properties[HubSpotVocabulary.Channel.PageName] = input.dataMap?.pageName;
            data.Properties[HubSpotVocabulary.Channel.Picture] = input.dataMap?.picture;
            data.Properties[HubSpotVocabulary.Channel.ProfileUrl] = input.dataMap?.profileUrl;
            data.Properties[HubSpotVocabulary.Channel.UserId] = input.dataMap?.userId;
            data.Properties[HubSpotVocabulary.Channel.Type] = input.type;

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "HubSpot");


            return clue;
        }
    }
}
