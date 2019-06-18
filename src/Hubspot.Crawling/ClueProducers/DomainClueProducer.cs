using System;
using CluedIn.Core.Data;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Vocabularies;

namespace CluedIn.Crawling.HubSpot.ClueProducers
{
    public class DomainClueProducer : BaseClueProducer<Domain>
    {
        private readonly IClueFactory _factory;

        public DomainClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(Domain input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Note, input.id.ToString(), accountId);

            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_001_Outgoing_Edge_MustExist);
            clue.ValidationRuleSuppressions.Add(CluedIn.Core.Constants.Validation.Rules.EDGES_002_Incoming_Edge_ShouldNotExist);

            var data = clue.Data.EntityData;

            data.Name = input.domain;
            data.CreatedDate = DateUtilities.EpochRef.AddMilliseconds(input.created);
            data.ModifiedDate = DateUtilities.EpochRef.AddMilliseconds(input.updated);

            data.Properties[HubSpotVocabulary.Domain.ActualCName] = input.actual_cname;
            data.Properties[HubSpotVocabulary.Domain.ActualIp] = input.actual_ip;
            data.Properties[HubSpotVocabulary.Domain.ConsecutiveNonResolvingCount] = input.consecutive_non_resolving_count.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.Domain] = input.domain;
            data.Properties[HubSpotVocabulary.Domain.FullCategoryKey] = input.full_category_key.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.IsAnyPrimary] = input.is_any_primary.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.IsDnsCorrect] = input.is_dns_correct.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.IsInternalDomain] = input.is_internal_domain.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.IsLegacy] = input.is_legacy.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.IsLegacyDomain] = input.is_legacy_domain.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.IsResolving] = input.is_resolving.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.ManuallyMarkedAsResolving] = input.manually_marked_as_resolving.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.PortalId] = input.portal_id.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.PrimaryBlogPost] = input.primary_blog_post.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.PrimaryEmail] = input.primary_email.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.PrimaryLandingPage] = input.primary_landing_page.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.PrimaryLegacyPage] = input.primary_legacy_page.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.PrimarySitePage] = input.primary_site_page.PrintIfAvailable();
            data.Properties[HubSpotVocabulary.Domain.SecondaryToDomain] = input.secondary_to_domain;
            data.Properties[HubSpotVocabulary.Domain.Updated] = input.updated.PrintIfAvailable();

            if (input.portal_id != null)
                _factory.CreateIncomingEntityReference(clue, EntityType.Infrastructure.Site, EntityEdgeType.PartOf, input, s => s.portal_id.ToString());


            return clue;
        }
    }
}
