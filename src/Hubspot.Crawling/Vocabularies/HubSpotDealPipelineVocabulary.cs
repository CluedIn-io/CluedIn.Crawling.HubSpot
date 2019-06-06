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
            this.VocabularyName = "HubSpot Deal Pipeline";
            this.KeyPrefix      = "hubspot.deal.pipeline";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Process;

            this.Active       = this.Add(new VocabularyKey("Active"));
            this.DisplayOrder = this.Add(new VocabularyKey("DisplayOrder"));
            this.Label        = this.Add(new VocabularyKey("Label"));
            this.PipelineId   = this.Add(new VocabularyKey("PipelineId", VocabularyKeyVisiblity.Hidden));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Active { get; private set; }

        public VocabularyKey DisplayOrder { get; private set; }

        public VocabularyKey Label { get; private set; }

        public VocabularyKey PipelineId { get; private set; }

    }
}