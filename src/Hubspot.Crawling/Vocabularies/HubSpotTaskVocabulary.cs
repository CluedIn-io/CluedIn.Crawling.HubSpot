// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotTaskVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotTaskVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot task vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotTaskVocabulary : SimpleVocabulary
    {
        public HubSpotTaskVocabulary()
        {
            this.VocabularyName = "HubSpot Task";
            this.KeyPrefix      = "hubspot.task";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Task;

            this.Status                       = this.Add(new VocabularyKey("Status"));
            this.DueDate                      = this.Add(new VocabularyKey("DueDate"));
            this.Description                  = this.Add(new VocabularyKey("Description", VocabularyKeyDataType.Html));
            this.ForObjectType                = this.Add(new VocabularyKey("ForObjectType"));
            this.Reminders                    = this.Add(new VocabularyKey("Reminders"));

            AddMapping(this.Status, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInTask.State);
            AddMapping(this.DueDate, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInTask.DueDate);
        }

        public VocabularyKey Status { get; private set; }

        public VocabularyKey ForObjectType { get; private set; }

        public VocabularyKey DueDate { get; private set; }

        public VocabularyKey Description { get; private set; }

        public VocabularyKey Reminders { get; private set; }

    }
}