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
            this.VocabularyName = "HubSpot Table";
            this.KeyPrefix      = "hubspot.table";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.List;

            this.Id          = this.Add(new VocabularyKey("Id"));
            this.Name        = this.Add(new VocabularyKey("Name"));
            this.CreatedAt   = this.Add(new VocabularyKey("CreatedAt", VocabularyKeyDataType.DateTime));
            this.PublishedAt = this.Add(new VocabularyKey("PublishedAt", VocabularyKeyDataType.DateTime));
            this.UpdatedAt   = this.Add(new VocabularyKey("UpdatedAt", VocabularyKeyDataType.DateTime));
            this.Deleted     = this.Add(new VocabularyKey("Deleted", VocabularyKeyDataType.Boolean));
            this.UseForPages = this.Add(new VocabularyKey("UseForPages", VocabularyKeyDataType.Boolean));
            this.RowCount    = this.Add(new VocabularyKey("RowCount", VocabularyKeyDataType.Integer));
            this.ColumnCount = this.Add(new VocabularyKey("ColumnCount", VocabularyKeyDataType.Integer));
            this.CreatedBy   = this.Add(new VocabularyKey("CreatedBy", VocabularyKeyDataType.Integer, VocabularyKeyVisiblity.Hidden));
            this.UpdatedBy   = this.Add(new VocabularyKey("UpdatedBy", VocabularyKeyDataType.Integer, VocabularyKeyVisiblity.Hidden));
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