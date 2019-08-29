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
            VocabularyName = "HubSpot Engagement";
            KeyPrefix      = "hubspot.engagement";
            KeySeparator   = ".";
            Grouping       = EntityType.Activity;

            Active    = Add(new VocabularyKey("Active"));
            TimeStamp = Add(new VocabularyKey("TimeStamp"));
            Type      = Add(new VocabularyKey("Type"));
            Duration = Add(new VocabularyKey("Duration"));
            ObjectType = Add(new VocabularyKey("ObjectType"));
            Status = Add(new VocabularyKey("Status"));
            Remidners = Add(new VocabularyKey("Reminders"));

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
