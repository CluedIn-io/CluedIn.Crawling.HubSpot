using System;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class UrlMappingClueProducer : BaseClueProducer<UrlMapping>
    {
        private readonly IClueFactory _factory;

        public UrlMappingClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(UrlMapping value, Guid accountId)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var clue = _factory.Create(EntityType.Note, value.id.ToString(), accountId);
            
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Name = value.name;

            data.Properties[HubSpotVocabulary.UrlMapping.ContentGroupId] = value.contentGroupId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.Created] = value.created.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.DeletedAt] = value.deletedAt.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.Destination] = value.destination;
            data.Properties[HubSpotVocabulary.UrlMapping.IsMatchFullUrl] = value.isMatchFullUrl.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.IsMatchQueryString] = value.isMatchQueryString.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.IsOnlyAfterNotFound] = value.isOnlyAfterNotFound.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.IsPattern] = value.isPattern.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.IsRegex] = value.isRegex.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.PortalId] = value.portalId.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.Precedence] = value.precedence.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.RedirectStyle] = value.redirectStyle.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.UrlMapping.RoutePrefix] = value.routePrefix;
            data.Properties[HubSpotVocabulary.UrlMapping.Updated] = value.updated.PrintIfAvailable();

            return clue;
        }
    }
}
