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
            VocabularyName = "HubSpot Task";
            KeyPrefix      = "hubspot.task";
            KeySeparator   = ".";
            Grouping       = EntityType.Task;

            Status                       = Add(new VocabularyKey("Status"));
            DueDate                      = Add(new VocabularyKey("DueDate"));
            Description                  = Add(new VocabularyKey("Description", VocabularyKeyDataType.Html));
            ForObjectType                = Add(new VocabularyKey("ForObjectType"));
            Reminders                    = Add(new VocabularyKey("Reminders"));

            AddMapping(Status, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInTask.State);
            AddMapping(DueDate, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInTask.DueDate);
        }

        public VocabularyKey Status { get; private set; }

        public VocabularyKey ForObjectType { get; private set; }

        public VocabularyKey DueDate { get; private set; }

        public VocabularyKey Description { get; private set; }

        public VocabularyKey Reminders { get; private set; }

    }
}
