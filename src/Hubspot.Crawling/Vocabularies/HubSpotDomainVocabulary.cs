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
            this.VocabularyName = "HubSpot Domain";
            this.KeyPrefix      = "hubspot.domain";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Note;

            this.ActualCName                  = this.Add(new VocabularyKey("ActualCName"));
            this.ActualIp = this.Add(new VocabularyKey("ActualIp", VocabularyKeyVisiblity.Hidden));
            this.ConsecutiveNonResolvingCount = this.Add(new VocabularyKey("ConsecutiveNonResolvingCount"));
            this.Domain                       = this.Add(new VocabularyKey("Domain"));
            this.FullCategoryKey              = this.Add(new VocabularyKey("FullCategoryKey"));
            this.IsAnyPrimary = this.Add(new VocabularyKey("IsAnyPrimary", VocabularyKeyVisiblity.Hidden));
            this.IsDnsCorrect = this.Add(new VocabularyKey("IsDnsCorrect", VocabularyKeyVisiblity.Hidden));
            this.IsInternalDomain             = this.Add(new VocabularyKey("IsInternalDomain"));
            this.IsLegacy = this.Add(new VocabularyKey("IsLegacy", VocabularyKeyVisiblity.Hidden));
            this.IsLegacyDomain = this.Add(new VocabularyKey("IsLegacyDomain", VocabularyKeyVisiblity.Hidden));
            this.IsResolving = this.Add(new VocabularyKey("IsResolving", VocabularyKeyVisiblity.Hidden));
            this.ManuallyMarkedAsResolving = this.Add(new VocabularyKey("ManuallyMarkedAsResolving", VocabularyKeyVisiblity.Hidden));
            this.PortalId                     = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.PrimaryBlogPost              = this.Add(new VocabularyKey("PrimaryBlogPost"));
            this.PrimaryEmail                 = this.Add(new VocabularyKey("PrimaryEmail"));
            this.PrimaryLandingPage           = this.Add(new VocabularyKey("PrimaryLandingPage"));
            this.PrimaryLegacyPage            = this.Add(new VocabularyKey("PrimaryLegacyPage"));
            this.SecondaryToDomain            = this.Add(new VocabularyKey("SecondaryToDomain"));
            this.Updated                      = this.Add(new VocabularyKey("Updated"));
            this.PrimarySitePage              = this.Add(new VocabularyKey("PrimarySitePage"));

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