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
            this.VocabularyName = "HubSpot Sitemap";
            this.KeyPrefix      = "hubspot.sitemap";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.News;

            this.Created   = this.Add(new VocabularyKey("Created"));
            this.DeletedAt = this.Add(new VocabularyKey("DeletedAt", VocabularyKeyDataType.DateTime));
            this.PagesTree = this.Add(new VocabularyKey("PagesTree", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.PortalId  = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.Updated   = this.Add(new VocabularyKey("Updated"));

            // TODO: map keys to CluedIn vocabulary
            this.AddMapping(this.Created, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
            this.AddMapping(this.Updated, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.ModifiedDate);
        }

        public VocabularyKey Created { get; private set; }

        public VocabularyKey DeletedAt { get; private set; }

        public VocabularyKey PagesTree { get; private set; }

        public VocabularyKey PortalId { get; private set; }

        public VocabularyKey Updated { get; private set; }
    }
}
