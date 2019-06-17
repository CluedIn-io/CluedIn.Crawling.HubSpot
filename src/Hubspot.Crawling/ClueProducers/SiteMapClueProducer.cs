using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class SiteMapClueProducer : BaseClueProducer<SiteMap>
    {
        private readonly IClueFactory _factory;

        public SiteMapClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(SiteMap input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.News, input.id.ToString(), accountId);
            
            var data = clue.Data.EntityData;

            data.Name = input.name;

            data.Properties[HubSpotVocabulary.SiteMap.Created] = input.created.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SiteMap.DeletedAt] = input.deleted_at.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SiteMap.PagesTree] = input.pages_tree.PrintIfAvailable(JsonUtility.Serialize);
            data.Properties[HubSpotVocabulary.SiteMap.PortalId] = input.portal_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.SiteMap.Updated] = input.updated.PrintIfAvailable();


            return clue;
        }
    }
}
