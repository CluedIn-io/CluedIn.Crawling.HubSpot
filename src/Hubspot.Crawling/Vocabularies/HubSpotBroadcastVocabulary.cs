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
            this.VocabularyName = "HubSpot Broadcast";
            this.KeyPrefix      = "hubspot.broadcast";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Announcement;

            this.AddGroup("Hubspot Broadcast Details", group =>
            {
                this.BroadcastGuid     = group.Add(new VocabularyKey("BroadcastGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisiblity.Hidden));
                this.CampaignGuid      = group.Add(new VocabularyKey("CampaignGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisiblity.Hidden));
                this.Clicks            = group.Add(new VocabularyKey("Clicks", VocabularyKeyDataType.Integer));
                this.ClientTag         = group.Add(new VocabularyKey("ClientTag", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
                this.CreatedBy         = group.Add(new VocabularyKey("CreatedBy"));
                this.FinishedAt        = group.Add(new VocabularyKey("FinishedAt", VocabularyKeyDataType.DateTime));
                this.ForeignId         = group.Add(new VocabularyKey("ForeignId", VocabularyKeyVisiblity.Hidden));
                this.GroupGuid         = group.Add(new VocabularyKey("GroupGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisiblity.Hidden));
                this.InteractionCounts = group.Add(new VocabularyKey("InteractionCounts", VocabularyKeyDataType.Json));
                this.Interactions      = group.Add(new VocabularyKey("Interactions", VocabularyKeyDataType.Json));
                this.LinkGuid          = group.Add(new VocabularyKey("LinkGuid", VocabularyKeyDataType.Guid, VocabularyKeyVisiblity.Hidden));
                this.LinkTaskQueueId   = group.Add(new VocabularyKey("LinkTaskQueueId", VocabularyKeyVisiblity.Hidden));
                this.RemoteContentId   = group.Add(new VocabularyKey("RemoteContentId", VocabularyKeyVisiblity.Hidden));
                this.RemoteContentType = group.Add(new VocabularyKey("RemoteContentType", VocabularyKeyVisiblity.Hidden));
                this.Status            = group.Add(new VocabularyKey("Status"));
                this.TaskQueued        = group.Add(new VocabularyKey("TaskQueued", VocabularyKeyVisiblity.Hidden));
                this.TriggerAt         = group.Add(new VocabularyKey("TriggerAt", VocabularyKeyDataType.DateTime));
                this.UpdatedBy         = group.Add(new VocabularyKey("UpdatedBy"));
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

