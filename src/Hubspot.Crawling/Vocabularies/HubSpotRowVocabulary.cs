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
            this.VocabularyName = "HubSpot Row";
            this.KeyPrefix      = "hubspot.row";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.List.Item;

            this.Id          = this.Add(new VocabularyKey("Id"));
            this.Name        = this.Add(new VocabularyKey("Name"));
            this.CreatedAt   = this.Add(new VocabularyKey("CreatedAt", VocabularyKeyDataType.DateTime));
            this.Path        = this.Add(new VocabularyKey("Path"));
        }

        public VocabularyKey Id { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey CreatedAt { get; private set; }
        public VocabularyKey Path { get; private set; }
    }
}