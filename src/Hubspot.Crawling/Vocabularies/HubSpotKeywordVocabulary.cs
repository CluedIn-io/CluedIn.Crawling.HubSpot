// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotKeywordVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotKeywordVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot keyword vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotKeywordVocabulary : SimpleVocabulary
    {
        public HubSpotKeywordVocabulary()
        {
            this.VocabularyName = "HubSpot Keyword";
            this.KeyPrefix      = "hubspot.keyword";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Tag;

            this.Contacts = this.Add(new VocabularyKey("Contacts"));
            this.Country  = this.Add(new VocabularyKey("Country"));
            this.Leads    = this.Add(new VocabularyKey("Leads"));
            this.Visits   = this.Add(new VocabularyKey("Visits"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Contacts { get; private set; }

        public VocabularyKey Country { get; private set; }

        public VocabularyKey Leads { get; private set; }

        public VocabularyKey Visits { get; private set; }
    }
}