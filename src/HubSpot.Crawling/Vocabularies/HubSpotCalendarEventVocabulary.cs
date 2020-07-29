// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotCalendarEventVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotCalendarEventVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot calendar event vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotCalendarEventVocabulary : SimpleVocabulary
    {
        public HubSpotCalendarEventVocabulary()
        {
            VocabularyName = "HubSpot Calendar Event";
            KeyPrefix      = "hubspot.calendar.event";
            KeySeparator   = ".";
            Grouping       = EntityType.Calendar.Event;

            AddGroup("HubSpot Calendar Event Details", group =>
            {
                AvatarUrl         = group.Add(new VocabularyKey("AvatarUrl", VocabularyKeyDataType.Uri));
                Category          = group.Add(new VocabularyKey("Category"));
                CreatedById       = group.Add(new VocabularyKey("CreatedById", VocabularyKeyVisibility.Hidden));
                EventDate         = group.Add(new VocabularyKey("EventDate", VocabularyKeyDataType.DateTime));
                EventType         = group.Add(new VocabularyKey("EventType"));
                PreviewKey        = group.Add(new VocabularyKey("PreviewKey"));
                Recurring         = group.Add(new VocabularyKey("Recurring"));
                SocialDisplayName = group.Add(new VocabularyKey("SocialDisplayName"));
                SocialUserName    = group.Add(new VocabularyKey("SocialUserName"));
                State             = group.Add(new VocabularyKey("State"));
                TopicsIds         = group.Add(new VocabularyKey("TopicsIds", VocabularyKeyVisibility.Hidden));
            });

            DayOfWeek = Add(new VocabularyKey("DayOfWeek", VocabularyKeyVisibility.Hidden));
            AddMapping(DayOfWeek, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.DayOfWeek);
            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey AvatarUrl { get; private set; }

        public VocabularyKey Category { get; private set; }

        public VocabularyKey CreatedById { get; private set; }

        public VocabularyKey DayOfWeek { get; protected set; }

        public VocabularyKey EventDate { get; private set; }

        public VocabularyKey EventType { get; private set; }

        public VocabularyKey PreviewKey { get; private set; }

        public VocabularyKey Recurring { get; private set; }

        public VocabularyKey SocialDisplayName { get; private set; }

        public VocabularyKey SocialUserName { get; private set; }

        public VocabularyKey State { get; private set; }

        public VocabularyKey TopicsIds { get; private set; }
    }
}
