// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HubSpotFormVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the HubSpotFormVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.HubSpot.Vocabularies
{
    /// <summary>The hub spot form vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class HubSpotFormVocabulary : SimpleVocabulary
    {
        public HubSpotFormVocabulary()
        {
            VocabularyName = "HubSpot Form";
            KeyPrefix      = "hubspot.form";
            KeySeparator   = ".";
            Grouping       = EntityType.Form;

            Action                 = Add(new VocabularyKey("Action"));
            CssClass               = Add(new VocabularyKey("CssClass"));
            Deletable              = Add(new VocabularyKey("Deletable"));
            FollowUpId             = Add(new VocabularyKey("FollowUpId", VocabularyKeyVisiblity.Hidden));
            FormFieldGroups        = Add(new VocabularyKey("FormFieldGroups", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            Guid                   = Add(new VocabularyKey("Guid", VocabularyKeyVisiblity.Hidden));
            IgnoreCurrentValues    = Add(new VocabularyKey("IgnoreCurrentValues"));
            LeadNuturingCampaignId = Add(new VocabularyKey("LeadNuturingCampaignId"));
            MetaData               = Add(new VocabularyKey("MetaData", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            Method                 = Add(new VocabularyKey("Method"));
            MigratedFrom           = Add(new VocabularyKey("MigratedFrom"));
            NotifyRecipients       = Add(new VocabularyKey("NotifyRecipients"));
            PerformableHtml        = Add(new VocabularyKey("PerformableHtml"));
            Redirect               = Add(new VocabularyKey("Redirect"));
            SubmitText             = Add(new VocabularyKey("SubmitText"));

            // TODO: map keys to CluedIn vocabulary
        }

        public VocabularyKey Action { get; private set; }
        public VocabularyKey CssClass { get; private set; }
        public VocabularyKey Deletable { get; private set; }
        public VocabularyKey FollowUpId { get; private set; }
        public VocabularyKey FormFieldGroups { get; private set; }
        public VocabularyKey Guid { get; private set; }
        public VocabularyKey IgnoreCurrentValues { get; private set; }
        public VocabularyKey LeadNuturingCampaignId { get; private set; }
        public VocabularyKey MetaData { get; private set; }
        public VocabularyKey Method { get; private set; }
        public VocabularyKey MigratedFrom { get; private set; }
        public VocabularyKey NotifyRecipients { get; private set; }
        public VocabularyKey PerformableHtml { get; private set; }
        public VocabularyKey Redirect { get; private set; }
        public VocabularyKey SubmitText { get; private set; }
    }
}
