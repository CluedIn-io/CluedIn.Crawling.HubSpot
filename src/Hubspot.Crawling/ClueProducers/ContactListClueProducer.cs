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
    public class ContactListClueProducer : BaseClueProducer<ContactList>
    {
        private readonly IClueFactory _factory;

        public ContactListClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(ContactList input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, input.listId.ToString(), accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            data.Name = input.name;

            if (input.createdAt != null)
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.createdAt.Value);

            if (input.updatedAt != null)
                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updatedAt.Value);

            data.Properties[HubSpotVocabulary.ContactList.Deleted] = input.deleted.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.Dynamic] = input.dynamic.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.Filters] = input.filters.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property
            data.Properties[HubSpotVocabulary.ContactList.InternalListId] = input.internalListId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.ListId] = input.listId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.MetaData] = input.metaData.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property

            if (input.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portalId.Value.ToString(), s => "HubSpot");


            return clue;
        }
    }
}
