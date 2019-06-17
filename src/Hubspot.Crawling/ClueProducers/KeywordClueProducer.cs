using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class KeywordClueProducer : BaseClueProducer<Keyword>
    {
        private readonly IClueFactory _factory;

        public KeywordClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Keyword value, Guid accountId)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var clue = _factory.Create(EntityType.Tag, value.keyword_guid, accountId);
            
            var data = clue.Data.EntityData;

            data.Name = value.keyword;
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(value.created_at);

            data.Properties[HubSpotVocabulary.Keyword.Contacts] = value.contacts.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Keyword.Country] = value.country.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Keyword.Leads] = value.leads.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Keyword.Visits] = value.visits.PrintIfAvailable();


            return clue;
        }
    }
}
