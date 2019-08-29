// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotRowVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotRowVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot row vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotRowVocabulary : SimpleVocabulary
    {
        public HubSpotRowVocabulary()
        {
            VocabularyName = "HubSpot Row";
            KeyPrefix      = "hubspot.row";
            KeySeparator   = ".";
            Grouping       = EntityType.List.Item;

            Id          = Add(new VocabularyKey("Id"));
            Name        = Add(new VocabularyKey("Name"));
            CreatedAt   = Add(new VocabularyKey("CreatedAt", VocabularyKeyDataType.DateTime));
            Path        = Add(new VocabularyKey("Path"));
        }

        public VocabularyKey Id { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey CreatedAt { get; private set; }
        public VocabularyKey Path { get; private set; }
    }
}
