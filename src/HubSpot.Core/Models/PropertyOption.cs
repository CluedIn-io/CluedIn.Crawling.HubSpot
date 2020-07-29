namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class PropertyOption
    {
        public bool hidden { get; set; }
        public string description { get; set; }
        public int displayOrder { get; set; }
        public string label { get; set; }
        public object doubleData { get; set; }
        public object readOnly { get; set; }
        public string value { get; set; }
        //public bool? hubspotDefined { get; set; }
    }
}