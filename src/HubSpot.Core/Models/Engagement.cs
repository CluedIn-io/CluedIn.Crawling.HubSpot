namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Engagement
    {
        public long id { get; set; }
        public long? portalId { get; set; }
        public bool active { get; set; }
        public long createdAt { get; set; }
        public long lastUpdated { get; set; }
        public long? ownerId { get; set; }
        public string type { get; set; }
        public long timestamp { get; set; }
    }

    public class Metadata  
    {
        public string body { get; set; }
    }
}
