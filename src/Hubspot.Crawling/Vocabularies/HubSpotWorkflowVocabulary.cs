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
            this.VocabularyName = "HubSpot Workflow";
            this.KeyPrefix      = "hubspot.workflow";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Process;

            this.Actions                            = this.Add(new VocabularyKey("Actions", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.AllowContactToTriggerMultipleTimes = this.Add(new VocabularyKey("AllowContactToTriggerMultipleTimes"));
            this.Enabled                            = this.Add(new VocabularyKey("Enabled"));
            this.GoalListIds                        = this.Add(new VocabularyKey("GoalListIds", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.InsertingAt                        = this.Add(new VocabularyKey("InsertingAt"));
            this.Listening                          = this.Add(new VocabularyKey("Listening"));
            this.NutureTimeRange                    = this.Add(new VocabularyKey("NutureTimeRange", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.OnlyExecOnBizDays                  = this.Add(new VocabularyKey("OnlyExecOnBizDays"));
            this.RecurringSetting                   = this.Add(new VocabularyKey("RecurringSetting", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.SuppressionListIds                 = this.Add(new VocabularyKey("SuppressionListIds", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.TriggerSets                        = this.Add(new VocabularyKey("TriggerSets", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.Type                               = this.Add(new VocabularyKey("Type"));
            this.UnEnrollmentSetting                = this.Add(new VocabularyKey("UnEnrollmentSetting"));

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