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
            this.VocabularyName = "HubSpot Calendar Event";
            this.KeyPrefix      = "hubspot.calendar.event";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Calendar.Event;

            this.AddGroup("Hubspot Calendar Event Details", group =>
            {
                this.AvatarUrl         = group.Add(new VocabularyKey("AvatarUrl", VocabularyKeyDataType.Uri));
                this.Category          = group.Add(new VocabularyKey("Category"));
                this.CreatedById       = group.Add(new VocabularyKey("CreatedById", VocabularyKeyVisiblity.Hidden));
                this.EventDate         = group.Add(new VocabularyKey("EventDate", VocabularyKeyDataType.DateTime));
                this.EventType         = group.Add(new VocabularyKey("EventType"));
                this.PreviewKey        = group.Add(new VocabularyKey("PreviewKey"));
                this.Recurring         = group.Add(new VocabularyKey("Recurring"));
                this.SocialDisplayName = group.Add(new VocabularyKey("SocialDisplayName"));
                this.SocialUserName    = group.Add(new VocabularyKey("SocialUserName"));
                this.State             = group.Add(new VocabularyKey("State"));
                this.TopicsIds         = group.Add(new VocabularyKey("TopicsIds", VocabularyKeyVisiblity.Hidden));
            });

            this.DayOfWeek = this.Add(new VocabularyKey("DayOfWeek", VocabularyKeyVisiblity.Hidden));
            this.AddMapping(this.DayOfWeek, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInEvent.DayOfWeek);
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