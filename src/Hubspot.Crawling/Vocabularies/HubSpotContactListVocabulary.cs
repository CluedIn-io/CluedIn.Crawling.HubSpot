// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotContactListVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotContactListVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot contact list vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotContactListVocabulary : SimpleVocabulary
    {
        public HubSpotContactListVocabulary()
        {
            this.VocabularyName = "HubSpot Contact List";
            this.KeyPrefix      = "hubspot.contact.list";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.List;

            this.Deleted        = this.Add(new VocabularyKey("Deleted"));
            this.Dynamic        = this.Add(new VocabularyKey("Dynamic"));
            this.Filters        = this.Add(new VocabularyKey("Filters", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.InternalListId = this.Add(new VocabularyKey("InternalListId", VocabularyKeyVisiblity.Hidden));
            this.ListId         = this.Add(new VocabularyKey("ListId", VocabularyKeyVisiblity.Hidden));
            this.MetaData       = this.Add(new VocabularyKey("MetaData", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Deleted { get; private set; }

        public VocabularyKey Dynamic { get; private set; }

        public VocabularyKey Filters { get; private set; }

        public VocabularyKey InternalListId { get; private set; }

        public VocabularyKey ListId { get; private set; }

        public VocabularyKey MetaData { get; private set; }
    }
}
