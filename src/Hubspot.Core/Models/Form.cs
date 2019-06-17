using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Validation
    {
        public string name { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public bool useDefaultBlockList { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public string fieldType { get; set; }
        public string description { get; set; }
        public string groupName { get; set; }
        public int displayOrder { get; set; }
        public bool required { get; set; }
        public List<object> selectedOptions { get; set; }
        public List<object> options { get; set; }
        public Validation validation { get; set; }
        public bool enabled { get; set; }
        public bool hidden { get; set; }
        public string defaultValue { get; set; }
        public bool isSmartField { get; set; }
        public string unselectedLabel { get; set; }
        public string placeholder { get; set; }
    }

    public class FormFieldGroup
    {
        public List<Field> fields { get; set; }
        public bool @default { get; set; }
        public bool isSmartGroup { get; set; }
    }

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
