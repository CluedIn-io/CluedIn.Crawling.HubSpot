using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
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
}