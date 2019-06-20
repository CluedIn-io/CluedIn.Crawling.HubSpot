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
            VocabularyName = "HubSpot Keyword";
            KeyPrefix      = "hubspot.keyword";
            KeySeparator   = ".";
            Grouping       = EntityType.Tag;

            Contacts = Add(new VocabularyKey("Contacts"));
            Country  = Add(new VocabularyKey("Country"));
            Leads    = Add(new VocabularyKey("Leads"));
            Visits   = Add(new VocabularyKey("Visits"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Contacts { get; private set; }

        public VocabularyKey Country { get; private set; }

        public VocabularyKey Leads { get; private set; }

        public VocabularyKey Visits { get; private set; }
    }
}
