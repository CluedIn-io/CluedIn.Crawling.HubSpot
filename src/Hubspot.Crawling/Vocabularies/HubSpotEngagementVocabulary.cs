// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotEngagementVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotEngagementVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot engagement vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotEngagementVocabulary : SimpleVocabulary
    {
        public HubSpotEngagementVocabulary()
        {
            this.VocabularyName = "HubSpot Engagement";
            this.KeyPrefix      = "hubspot.engagement";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Activity;

            this.Active    = this.Add(new VocabularyKey("Active"));
            this.TimeStamp = this.Add(new VocabularyKey("TimeStamp"));
            this.Type      = this.Add(new VocabularyKey("Type"));
            this.Duration = this.Add(new VocabularyKey("Duration"));
            this.ObjectType = this.Add(new VocabularyKey("ObjectType"));
            this.Status = this.Add(new VocabularyKey("Status"));
            this.Remidners = this.Add(new VocabularyKey("Reminders"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Active { get; private set; }

        public VocabularyKey TimeStamp { get; private set; }

        public VocabularyKey Type { get; private set; }
        public VocabularyKey Duration { get; internal set; }
        public VocabularyKey ObjectType { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey Remidners { get; internal set; }
    }
}