// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotStageMapVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotStageMapVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot stage map vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotStageMapVocabulary : SimpleVocabulary
    {
        public HubSpotStageMapVocabulary()
        {
            this.VocabularyName = "HubSpot Stage";
            this.KeyPrefix      = "hubspot.stage";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.ProcessStage;

            this.Active       = this.Add(new VocabularyKey("Active"));
            this.DisplayOrder = this.Add(new VocabularyKey("DisplayOrder"));
            this.Label        = this.Add(new VocabularyKey("Label"));
            this.Probability  = this.Add(new VocabularyKey("Probability"));
            this.ClosedWon    = this.Add(new VocabularyKey("ClosedWon"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Active { get; private set; }

        public VocabularyKey DisplayOrder { get; private set; }

        public VocabularyKey Label { get; private set; }

        public VocabularyKey Probability { get; private set; }

        public VocabularyKey ClosedWon { get; private set; }
    }
}