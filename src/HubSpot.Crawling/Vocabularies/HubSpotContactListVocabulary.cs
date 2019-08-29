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
            VocabularyName = "HubSpot Contact List";
            KeyPrefix      = "hubspot.contact.list";
            KeySeparator   = ".";
            Grouping       = EntityType.List;

            Deleted        = Add(new VocabularyKey("Deleted"));
            Dynamic        = Add(new VocabularyKey("Dynamic"));
            Filters        = Add(new VocabularyKey("Filters", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            InternalListId = Add(new VocabularyKey("InternalListId", VocabularyKeyVisiblity.Hidden));
            ListId         = Add(new VocabularyKey("ListId", VocabularyKeyVisiblity.Hidden));
            MetaData       = Add(new VocabularyKey("MetaData", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));

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
