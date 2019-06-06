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
            this.VocabularyName = "HubSpot Form";
            this.KeyPrefix      = "hubspot.form";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Form;

            this.Action                 = this.Add(new VocabularyKey("Action"));
            this.CssClass               = this.Add(new VocabularyKey("CssClass"));
            this.Deletable              = this.Add(new VocabularyKey("Deletable"));
            this.FollowUpId             = this.Add(new VocabularyKey("FollowUpId", VocabularyKeyVisiblity.Hidden));
            this.FormFieldGroups        = this.Add(new VocabularyKey("FormFieldGroups", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.Guid                   = this.Add(new VocabularyKey("Guid", VocabularyKeyVisiblity.Hidden));
            this.IgnoreCurrentValues    = this.Add(new VocabularyKey("IgnoreCurrentValues"));
            this.LeadNuturingCampaignId = this.Add(new VocabularyKey("LeadNuturingCampaignId"));
            this.MetaData               = this.Add(new VocabularyKey("MetaData", VocabularyKeyDataType.Json, VocabularyKeyVisiblity.Hidden));
            this.Method                 = this.Add(new VocabularyKey("Method"));
            this.MigratedFrom           = this.Add(new VocabularyKey("MigratedFrom"));
            this.NotifyRecipients       = this.Add(new VocabularyKey("NotifyRecipients"));
            this.PerformableHtml        = this.Add(new VocabularyKey("PerformableHtml"));
            this.Redirect               = this.Add(new VocabularyKey("Redirect"));
            this.SubmitText             = this.Add(new VocabularyKey("SubmitText"));

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