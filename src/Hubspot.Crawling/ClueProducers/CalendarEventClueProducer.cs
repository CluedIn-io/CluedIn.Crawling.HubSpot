using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class CalendarEventClueProducer : BaseClueProducer<CalendarEvent>
    {
        private readonly IClueFactory _factory;

        public CalendarEventClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(CalendarEvent input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Calendar.Event, input.id, accountId);
            
            var data = clue.Data.EntityData;

            data.Name = input.name;
            data.Description = input.description;

            if (input.url != null)
                data.Uri = new Uri(input.url);

            data.Properties[HubSpotVocabulary.CalendarEvent.AvatarUrl] = input.avatarUrl.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.CalendarEvent.Category] = input.category;
            data.Properties[HubSpotVocabulary.CalendarEvent.CreatedById] = input.createdById.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.CalendarEvent.DayOfWeek] = input.eventDate.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v).DayOfWeek);
            data.Properties[HubSpotVocabulary.CalendarEvent.EventDate] = input.eventDate.PrintIfAvailable(v => DateUtilities.EpochRef.AddMilliseconds(v));
            data.Properties[HubSpotVocabulary.CalendarEvent.EventType] = input.eventType;
            data.Properties[HubSpotVocabulary.CalendarEvent.PreviewKey] = input.previewKey;
            data.Properties[HubSpotVocabulary.CalendarEvent.Recurring] = input.recurring.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.CalendarEvent.SocialDisplayName] = input.socialDisplayName.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.CalendarEvent.SocialUserName] = input.socialUsername.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.CalendarEvent.State] = input.state;
            data.Properties[HubSpotVocabulary.CalendarEvent.TopicsIds] = input.topicIds.PrintIfAvailable();

            if (input.campaignGuid != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, s => s.campaignGuid);

            if (input.categoryId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Tag, EntityEdgeType.PartOf, input, s => s.categoryId.ToString());

            if (input.contentGroupId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Tag, EntityEdgeType.PartOf, input, s => s.contentGroupId.ToString());

            if (input.contentId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.News, EntityEdgeType.PartOf, input, s => s.contentId.ToString());

            if (input.createdBy != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.CreatedBy, input, s => s.createdBy.ToString());

            if (input.ownerId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Person, EntityEdgeType.OwnedBy, input, s => s.createdBy.ToString());

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.ToString(), s => "HubSpot");


            return clue;
        }
    }
}
