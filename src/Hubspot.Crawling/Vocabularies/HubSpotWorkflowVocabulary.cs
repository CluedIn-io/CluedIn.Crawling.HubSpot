// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotWorkflowVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotWorkflowVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot workflow vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotWorkflowVocabulary : SimpleVocabulary
    {
        public HubSpotWorkflowVocabulary()
        {
            VocabularyName = "HubSpot Workflow";
            KeyPrefix      = "hubspot.workflow";
            KeySeparator   = ".";
            Grouping       = EntityType.Process;

            Actions                            = Add(new VocabularyKey("Actions", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            AllowContactToTriggerMultipleTimes = Add(new VocabularyKey("AllowContactToTriggerMultipleTimes"));
            Enabled                            = Add(new VocabularyKey("Enabled"));
            GoalListIds                        = Add(new VocabularyKey("GoalListIds", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            InsertingAt                        = Add(new VocabularyKey("InsertingAt"));
            Listening                          = Add(new VocabularyKey("Listening"));
            NutureTimeRange                    = Add(new VocabularyKey("NutureTimeRange", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            OnlyExecOnBizDays                  = Add(new VocabularyKey("OnlyExecOnBizDays"));
            RecurringSetting                   = Add(new VocabularyKey("RecurringSetting", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            SuppressionListIds                 = Add(new VocabularyKey("SuppressionListIds", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            TriggerSets                        = Add(new VocabularyKey("TriggerSets", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            Type                               = Add(new VocabularyKey("Type"));
            UnEnrollmentSetting                = Add(new VocabularyKey("UnEnrollmentSetting"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Actions { get; private set; }

        public VocabularyKey AllowContactToTriggerMultipleTimes { get; private set; }

        public VocabularyKey Enabled { get; private set; }

        public VocabularyKey GoalListIds { get; private set; }

        public VocabularyKey InsertingAt { get; private set; }

        public VocabularyKey Listening { get; private set; }

        public VocabularyKey NutureTimeRange { get; private set; }

        public VocabularyKey OnlyExecOnBizDays { get; private set; }

        public VocabularyKey RecurringSetting { get; private set; }

        public VocabularyKey SuppressionListIds { get; private set; }

        public VocabularyKey TriggerSets { get; private set; }

        public VocabularyKey Type { get; private set; }

        public VocabularyKey UnEnrollmentSetting { get; private set; }
    }
}
