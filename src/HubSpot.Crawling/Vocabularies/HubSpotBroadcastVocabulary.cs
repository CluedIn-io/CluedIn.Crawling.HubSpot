// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotBroadcastVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotBroadcastVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot broadcast vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotBroadcastVocabulary : SimpleVocabulary
    {
        public HubSpotBroadcastVocabulary()
        {
            VocabularyName = "HubSpot Broadcast";
            KeyPrefix      = "hubspot.broadcast";
            KeySeparator   = ".";
            Grouping       = EntityType.Announcement;

            AddGroup("Hubspot Broadcast Details", group =>
            {
                BroadcastGuid     = group.Add(new VocabularyKey("BroadcastGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisibility.Hidden));
                CampaignGuid      = group.Add(new VocabularyKey("CampaignGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisibility.Hidden));
                Clicks            = group.Add(new VocabularyKey("Clicks", VocabularyKeyDataType.Integer));
                ClientTag         = group.Add(new VocabularyKey("ClientTag", VocabularyKeyDataType.Json, VocabularyKeyVisibility.Hidden));
                CreatedBy         = group.Add(new VocabularyKey("CreatedBy"));
                FinishedAt        = group.Add(new VocabularyKey("FinishedAt", VocabularyKeyDataType.DateTime));
                ForeignId         = group.Add(new VocabularyKey("ForeignId", VocabularyKeyVisibility.Hidden));
                GroupGuid         = group.Add(new VocabularyKey("GroupGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisibility.Hidden));
                InteractionCounts = group.Add(new VocabularyKey("InteractionCounts", VocabularyKeyDataType.Json));
                Interactions      = group.Add(new VocabularyKey("Interactions", VocabularyKeyDataType.Json));
                LinkGuid          = group.Add(new VocabularyKey("LinkGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisibility.Hidden));
                LinkTaskQueueId   = group.Add(new VocabularyKey("LinkTaskQueueId", VocabularyKeyVisibility.Hidden));
                RemoteContentId   = group.Add(new VocabularyKey("RemoteContentId", VocabularyKeyVisibility.Hidden));
                RemoteContentType = group.Add(new VocabularyKey("RemoteContentType", VocabularyKeyVisibility.Hidden));
                Status            = group.Add(new VocabularyKey("Status"));
                TaskQueued        = group.Add(new VocabularyKey("TaskQueued", VocabularyKeyVisibility.Hidden));
                TriggerAt         = group.Add(new VocabularyKey("TriggerAt", VocabularyKeyDataType.DateTime));
                UpdatedBy         = group.Add(new VocabularyKey("UpdatedBy"));
            });

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey BroadcastGuid { get; private set; }
        public VocabularyKey CampaignGuid { get; private set; }
        public VocabularyKey Clicks { get; private set; }
        public VocabularyKey ClientTag { get; private set; }
        public VocabularyKey CreatedBy { get; private set; }
        public VocabularyKey FinishedAt { get; private set; }
        public VocabularyKey ForeignId { get; private set; }
        public VocabularyKey GroupGuid { get; private set; }
        public VocabularyKey InteractionCounts { get; private set; }
        public VocabularyKey Interactions { get; private set; }
        public VocabularyKey LinkGuid { get; private set; }
        public VocabularyKey LinkTaskQueueId { get; private set; }
        public VocabularyKey RemoteContentId { get; private set; }
        public VocabularyKey RemoteContentType { get; private set; }
        public VocabularyKey Status { get; private set; }
        public VocabularyKey TaskQueued { get; private set; }
        public VocabularyKey TriggerAt { get; private set; }
        public VocabularyKey UpdatedBy { get; private set; }
    }
}

