// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotMeetingVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotMeetingVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot meeting vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotMeetingVocabulary : SimpleVocabulary
    {
        public HubSpotMeetingVocabulary()
        {
            VocabularyName = "HubSpot Meeting";
            KeyPrefix      = "hubspot.meeting";
            KeySeparator   = ".";
            Grouping       = EntityType.Calendar.Meeting;

            Title       = Add(new VocabularyKey("Title", VocabularyKeyDataType.Text, VocabularyKeyVisibility.HiddenInFrontendUI));
            Description = Add(new VocabularyKey("Description", VocabularyKeyVisibility.HiddenInFrontendUI));
            StartTime   = Add(new VocabularyKey("StartTime", VocabularyKeyDataType.DateTime));
            EndTime     = Add(new VocabularyKey("EndTime", VocabularyKeyDataType.DateTime));
            Reminder     = Add(new VocabularyKey("Reminder", VocabularyKeyDataType.DateTime));

            AddMapping(StartTime, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Start);
            AddMapping(EndTime, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.End);
            AddMapping(Title, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Name);
            AddMapping(Description, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Description);
            AddMapping(Reminder, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Reminder);
        }

        public VocabularyKey StartTime { get; private set; }

        public VocabularyKey EndTime { get; private set; }

        public VocabularyKey Title { get; private set; }

        public VocabularyKey Description { get; private set; }

        public VocabularyKey Reminder { get; private set; }
    }
}
