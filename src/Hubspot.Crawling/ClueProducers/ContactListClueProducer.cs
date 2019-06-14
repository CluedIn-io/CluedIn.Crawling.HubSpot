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

        protected override Clue MakeClueImpl(ContactList value, Guid accountId)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.News, value.listId.ToString(), accountId);

            // TODO: Populate clue data
            var data = clue.Data.EntityData;

            data.Name = value.name;

            if (value.createdAt != null)
                data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(value.createdAt.Value);

            if (value.updatedAt != null)
                data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(value.updatedAt.Value);

            data.Properties[HubSpotVocabulary.ContactList.Deleted] = value.deleted.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.Dynamic] = value.dynamic.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.Filters] = value.filters.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property
            data.Properties[HubSpotVocabulary.ContactList.InternalListId] = value.internalListId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.ListId] = value.listId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.ContactList.MetaData] = value.metaData.PrintIfAvailable(JsonUtility.Serialize); // TODO: Json serialized to property

            if (value.portalId != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, value, s => s.portalId.Value.ToString(), s => "HubSpot");


            return clue;
        }
    }
}
