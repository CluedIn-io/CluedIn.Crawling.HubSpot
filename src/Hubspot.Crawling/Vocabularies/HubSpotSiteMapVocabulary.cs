// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotSiteMapVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotSiteMapVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot site map vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotSiteMapVocabulary : SimpleVocabulary
    {
        public HubSpotSiteMapVocabulary()
        {
            VocabularyName = "HubSpot Sitemap";
            KeyPrefix      = "hubspot.sitemap";
            KeySeparator   = ".";
            Grouping       = EntityType.News;

            Created   = Add(new VocabularyKey("Created"));
            DeletedAt = Add(new VocabularyKey("DeletedAt", VocabularyKeyDataType.DateTime));
            PagesTree = Add(new VocabularyKey("PagesTree", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            PortalId  = Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            Updated   = Add(new VocabularyKey("Updated"));

            // TODO: map keys to CluedIn vocabulary
            AddMapping(Created, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
            AddMapping(Updated, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.ModifiedDate);
        }

        public VocabularyKey Created { get; private set; }

        public VocabularyKey DeletedAt { get; private set; }

        public VocabularyKey PagesTree { get; private set; }

        public VocabularyKey PortalId { get; private set; }

        public VocabularyKey Updated { get; private set; }
    }
}
