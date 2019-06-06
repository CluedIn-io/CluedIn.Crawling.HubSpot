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
            this.VocabularyName = "HubSpot Meeting";
            this.KeyPrefix      = "hubspot.meeting";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Calendar.Meeting;

            this.Title       = this.Add(new VocabularyKey("Title", VocabularyKeyDataType.Text, VocabularyKeyVisiblity.HiddenInFrontendUI));
            this.Description = this.Add(new VocabularyKey("Description", VocabularyKeyVisiblity.HiddenInFrontendUI));
            this.StartTime   = this.Add(new VocabularyKey("StartTime", VocabularyKeyDataType.DateTime));
            this.EndTime     = this.Add(new VocabularyKey("EndTime", VocabularyKeyDataType.DateTime));
            this.Reminder     = this.Add(new VocabularyKey("Reminder", VocabularyKeyDataType.DateTime));

            AddMapping(this.StartTime, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Start);
            AddMapping(this.EndTime, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.End);
            AddMapping(this.Title, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Name);
            AddMapping(this.Description, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Description);
            AddMapping(this.Reminder, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.Reminder);
        }

        public VocabularyKey StartTime { get; private set; }

        public VocabularyKey EndTime { get; private set; }

        public VocabularyKey Title { get; private set; }

        public VocabularyKey Description { get; private set; }

        public VocabularyKey Reminder { get; private set; }
    }
}