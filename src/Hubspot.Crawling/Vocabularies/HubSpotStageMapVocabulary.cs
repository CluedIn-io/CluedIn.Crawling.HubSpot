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
            VocabularyName = "HubSpot Stage";
            KeyPrefix      = "hubspot.stage";
            KeySeparator   = ".";
            Grouping       = EntityType.ProcessStage;

            Active       = Add(new VocabularyKey("Active"));
            DisplayOrder = Add(new VocabularyKey("DisplayOrder"));
            Label        = Add(new VocabularyKey("Label"));
            Probability  = Add(new VocabularyKey("Probability"));
            ClosedWon    = Add(new VocabularyKey("ClosedWon"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Active { get; private set; }

        public VocabularyKey DisplayOrder { get; private set; }

        public VocabularyKey Label { get; private set; }

        public VocabularyKey Probability { get; private set; }

        public VocabularyKey ClosedWon { get; private set; }
    }
}
