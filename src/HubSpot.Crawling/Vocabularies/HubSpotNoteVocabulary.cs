// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotNoteVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotNoteVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot note vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotNoteVocabulary : SimpleVocabulary
    {
        public HubSpotNoteVocabulary()
        {
            VocabularyName = "HubSpot Note";
            KeyPrefix      = "hubspot.note";
            KeySeparator   = ".";
            Grouping       = EntityType.Note;

            Body = Add(new VocabularyKey("Note"));

        }

        public VocabularyKey Body { get; private set; }
    
    }
}
