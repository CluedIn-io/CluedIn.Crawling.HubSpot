// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotTableVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotTableVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot table vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotTableVocabulary : SimpleVocabulary
    {
        public HubSpotTableVocabulary()
        {
            VocabularyName = "HubSpot Table";
            KeyPrefix      = "hubspot.table";
            KeySeparator   = ".";
            Grouping       = EntityType.List;

            Id          = Add(new VocabularyKey("Id"));
            Name        = Add(new VocabularyKey("Name"));
            CreatedAt   = Add(new VocabularyKey("CreatedAt", VocabularyKeyDataType.DateTime));
            PublishedAt = Add(new VocabularyKey("PublishedAt", VocabularyKeyDataType.DateTime));
            UpdatedAt   = Add(new VocabularyKey("UpdatedAt", VocabularyKeyDataType.DateTime));
            Deleted     = Add(new VocabularyKey("Deleted", VocabularyKeyDataType.Boolean));
            UseForPages = Add(new VocabularyKey("UseForPages", VocabularyKeyDataType.Boolean));
            RowCount    = Add(new VocabularyKey("RowCount", VocabularyKeyDataType.Integer));
            ColumnCount = Add(new VocabularyKey("ColumnCount", VocabularyKeyDataType.Integer));
            CreatedBy   = Add(new VocabularyKey("CreatedBy", VocabularyKeyDataType.Integer, VocabularyKeyVisibility.Hidden));
            UpdatedBy   = Add(new VocabularyKey("UpdatedBy", VocabularyKeyDataType.Integer, VocabularyKeyVisibility.Hidden));
        }

        public VocabularyKey Id { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey CreatedAt { get; private set; }
        public VocabularyKey PublishedAt { get; private set; }
        public VocabularyKey UpdatedAt { get; private set; }
        public VocabularyKey Deleted { get; private set; }
        public VocabularyKey UseForPages { get; private set; }
        public VocabularyKey RowCount { get; private set; }
        public VocabularyKey ColumnCount { get; private set; }
        public VocabularyKey CreatedBy { get; private set; }
        public VocabularyKey UpdatedBy { get; private set; }
    }
}
