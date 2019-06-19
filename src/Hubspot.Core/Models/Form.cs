using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Form
    {
        public long? portalId { get; set; }
        public string guid { get; set; }
        public string name { get; set; }
        public string action { get; set; }
        public string method { get; set; }
        public string cssClass { get; set; }
        public string redirect { get; set; }
        public string submitText { get; set; }
        public string followUpId { get; set; }
        public string notifyRecipients { get; set; }
        public string leadNurturingCampaignId { get; set; }
        public List<FormFieldGroup> formFieldGroups { get; set; }
        public long createdAt { get; set; }
        public long updatedAt { get; set; }
        public string performableHtml { get; set; }
        public string migratedFrom { get; set; }
        public bool ignoreCurrentValues { get; set; }
        public List<object> metaData { get; set; }
        public bool deletable { get; set; }
    }
}
