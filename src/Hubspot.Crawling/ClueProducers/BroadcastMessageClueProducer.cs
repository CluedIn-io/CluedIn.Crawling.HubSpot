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
    public class BroadcastMessageClueProducer : BaseClueProducer<BroadcastMessage>
    {
        private readonly IClueFactory _factory;

        public BroadcastMessageClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(BroadcastMessage input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, input.broadcastGuid, accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            data.Name = input.message;

            if (input.createdAt != null)
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.createdAt.Value);

            if (input.messageUrl != null && Uri.TryCreate(input.messageUrl, UriKind.Absolute, out var uri))
                data.Uri = uri;

            data.Properties[HubSpotVocabulary.Broadcast.BroadcastGuid] = input.broadcastGuid;
            data.Properties[HubSpotVocabulary.Broadcast.CampaignGuid] = input.campaignGuid;
            data.Properties[HubSpotVocabulary.Broadcast.Clicks] = input.clicks.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Broadcast.ClientTag] = input.clientTag.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property;
            data.Properties[HubSpotVocabulary.Broadcast.CreatedBy] = input.createdBy.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Broadcast.FinishedAt] = input.finishedAt.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v.Value));
            data.Properties[HubSpotVocabulary.Broadcast.ForeignId] = input.foreignId;
            data.Properties[HubSpotVocabulary.Broadcast.GroupGuid] = input.groupGuid;
            data.Properties[HubSpotVocabulary.Broadcast.InteractionCounts] = input.interactionCounts.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property
            data.Properties[HubSpotVocabulary.Broadcast.Interactions] = input.interactions.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property
            data.Properties[HubSpotVocabulary.Broadcast.LinkGuid] = input.linkGuid;
            data.Properties[HubSpotVocabulary.Broadcast.LinkTaskQueueId] = input.linkTaskQueueId;
            data.Properties[HubSpotVocabulary.Broadcast.RemoteContentId] = input.remoteContentId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Broadcast.RemoteContentType] = input.remoteContentType.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Broadcast.Status] = input.status;
            data.Properties[HubSpotVocabulary.Broadcast.TaskQueued] = input.taskQueueId;
            data.Properties[HubSpotVocabulary.Broadcast.TriggerAt] = input.triggerAt.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v.Value));
            data.Properties[HubSpotVocabulary.Broadcast.UpdatedBy] = input.updatedBy.PrintIfAvailable();

            if (input.channelGuid != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Channel, EntityEdgeType.PartOf, input, s => s.channelGuid);

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "HubSpot");

            return clue;
        }
    }
}
