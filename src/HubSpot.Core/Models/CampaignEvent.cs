namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class CampaignEvent
    {
        public long? appId { get; set; }
        public string appName { get; set; }
        public Browser browser { get; set; }
        public long created { get; set; }
        public long? emailCampaignId { get; set; }
        public string hmid { get; set; }
        public string id { get; set; }
        public string ipAddress { get; set; }
        public Location location { get; set; }
        public long? portalId { get; set; }
        public string recipient { get; set; }
        public string type { get; set; }
        public string userAgent { get; set; }
    }
}
