// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotDealPipelineVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotDealPipelineVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot deal pipeline vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotDealPipelineVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubSpotDealPipelineVocabulary"/> class.
        /// </summary>
        public HubSpotDealPipelineVocabulary()
        {
            VocabularyName = "HubSpot Deal Pipeline";
            KeyPrefix      = "hubspot.deal.pipeline";
            KeySeparator   = ".";
            Grouping       = EntityType.Process;

            Active       = Add(new VocabularyKey("Active"));
            DisplayOrder = Add(new VocabularyKey("DisplayOrder"));
            Label        = Add(new VocabularyKey("Label"));
            PipelineId   = Add(new VocabularyKey("PipelineId", VocabularyKeyVisibility.Hidden));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Active { get; private set; }

        public VocabularyKey DisplayOrder { get; private set; }

        public VocabularyKey Label { get; private set; }

        public VocabularyKey PipelineId { get; private set; }

    }
}
