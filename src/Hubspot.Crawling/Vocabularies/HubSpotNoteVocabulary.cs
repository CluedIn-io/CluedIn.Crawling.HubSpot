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
            this.VocabularyName = "HubSpot Note";
            this.KeyPrefix      = "hubspot.note";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Note;

            this.Body = this.Add(new VocabularyKey("Note"));

        }

        public VocabularyKey Body { get; private set; }
    
    }
}