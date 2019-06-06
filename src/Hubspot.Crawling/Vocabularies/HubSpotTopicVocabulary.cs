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
            this.VocabularyName = "HubSpot Topic";
            this.KeyPrefix      = "hubspot.topic";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Topic;

            this.Created   = this.Add(new VocabularyKey("Created"));
            this.DeletedAt = this.Add(new VocabularyKey("DeletedAt"));
            this.PortalId  = this.Add(new VocabularyKey("PortalId", VocabularyKeyVisiblity.Hidden));
            this.Slug      = this.Add(new VocabularyKey("Slug", VocabularyKeyVisiblity.Hidden));
            this.Updated   = this.Add(new VocabularyKey("Updated"));

            this.AddMapping(this.Created, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
            this.AddMapping(this.Updated, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.ModifiedDate);
            this.AddMapping(this.DeletedAt, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.DeletedDate);
        }

        public VocabularyKey Created { get; private set; }

        public VocabularyKey DeletedAt { get; private set; }

        public VocabularyKey PortalId { get; private set; }

        public VocabularyKey Slug { get; private set; }

        public VocabularyKey Updated { get; private set; }
    }
}
