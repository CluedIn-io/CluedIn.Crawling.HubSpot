// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotTopicVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotTopicVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot topic vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotTopicVocabulary : SimpleVocabulary
    {
        public HubSpotTopicVocabulary()
        {
            VocabularyName = "HubSpot Topic";
            KeyPrefix      = "hubspot.topic";
            KeySeparator   = ".";
            Grouping       = EntityType.Topic;

            Created   = Add(new VocabularyKey("Created"));
            DeletedAt = Add(new VocabularyKey("DeletedAt"));
            PortalId  = Add(new VocabularyKey("PortalId", VocabularyKeyVisibility.Hidden));
            Slug      = Add(new VocabularyKey("Slug", VocabularyKeyVisibility.Hidden));
            Updated   = Add(new VocabularyKey("Updated"));

            AddMapping(Created, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
            AddMapping(Updated, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.ModifiedDate);
            AddMapping(DeletedAt, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.DeletedDate);
        }

        public VocabularyKey Created { get; private set; }

        public VocabularyKey DeletedAt { get; private set; }

        public VocabularyKey PortalId { get; private set; }

        public VocabularyKey Slug { get; private set; }

        public VocabularyKey Updated { get; private set; }
    }
}
