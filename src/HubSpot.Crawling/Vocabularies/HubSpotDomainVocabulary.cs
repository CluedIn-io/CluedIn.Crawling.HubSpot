// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotDomainVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotDomainVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot domain vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotDomainVocabulary : SimpleVocabulary
    {
        public HubSpotDomainVocabulary()
        {
            VocabularyName = "HubSpot Domain";
            KeyPrefix      = "hubspot.domain";
            KeySeparator   = ".";
            Grouping       = EntityType.Note;

            ActualCName                  = Add(new VocabularyKey("ActualCName"));
            ActualIp = Add(new VocabularyKey("ActualIp", VocabularyKeyVisibility.Hidden));
            ConsecutiveNonResolvingCount = Add(new VocabularyKey("ConsecutiveNonResolvingCount"));
            Domain                       = Add(new VocabularyKey("Domain"));
            FullCategoryKey              = Add(new VocabularyKey("FullCategoryKey"));
            IsAnyPrimary = Add(new VocabularyKey("IsAnyPrimary", VocabularyKeyVisibility.Hidden));
            IsDnsCorrect = Add(new VocabularyKey("IsDnsCorrect", VocabularyKeyVisibility.Hidden));
            IsInternalDomain             = Add(new VocabularyKey("IsInternalDomain"));
            IsLegacy = Add(new VocabularyKey("IsLegacy", VocabularyKeyVisibility.Hidden));
            IsLegacyDomain = Add(new VocabularyKey("IsLegacyDomain", VocabularyKeyVisibility.Hidden));
            IsResolving = Add(new VocabularyKey("IsResolving", VocabularyKeyVisibility.Hidden));
            ManuallyMarkedAsResolving = Add(new VocabularyKey("ManuallyMarkedAsResolving", VocabularyKeyVisibility.Hidden));
            PortalId                     = Add(new VocabularyKey("PortalId", VocabularyKeyVisibility.Hidden));
            PrimaryBlogPost              = Add(new VocabularyKey("PrimaryBlogPost"));
            PrimaryEmail                 = Add(new VocabularyKey("PrimaryEmail"));
            PrimaryLandingPage           = Add(new VocabularyKey("PrimaryLandingPage"));
            PrimaryLegacyPage            = Add(new VocabularyKey("PrimaryLegacyPage"));
            SecondaryToDomain            = Add(new VocabularyKey("SecondaryToDomain"));
            Updated                      = Add(new VocabularyKey("Updated"));
            PrimarySitePage              = Add(new VocabularyKey("PrimarySitePage"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey ActualCName { get; private set; }
        public VocabularyKey ActualIp { get; private set; }
        public VocabularyKey ConsecutiveNonResolvingCount { get; private set; }
        public VocabularyKey Domain { get; private set; }
        public VocabularyKey FullCategoryKey { get; private set; }
        public VocabularyKey IsAnyPrimary { get; private set; }
        public VocabularyKey IsDnsCorrect { get; private set; }
        public VocabularyKey IsInternalDomain { get; private set; }
        public VocabularyKey IsLegacy { get; private set; }
        public VocabularyKey IsLegacyDomain { get; private set; }
        public VocabularyKey IsResolving { get; private set; }
        public VocabularyKey ManuallyMarkedAsResolving { get; private set; }
        public VocabularyKey PortalId { get; private set; }
        public VocabularyKey PrimaryBlogPost { get; private set; }
        public VocabularyKey PrimaryEmail { get; private set; }
        public VocabularyKey PrimaryLandingPage { get; private set; }
        public VocabularyKey PrimaryLegacyPage { get; private set; }
        public VocabularyKey SecondaryToDomain { get; private set; }
        public VocabularyKey Updated { get; private set; }
        public VocabularyKey PrimarySitePage { get; private set; }
    }
}
