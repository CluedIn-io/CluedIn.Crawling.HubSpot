namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Channel
    {
        public string channelGuid { get; set; }
        public long? portalId { get; set; }
        public string channelId { get; set; }
        public string accountGuid { get; set; }
        public string name { get; set; }
        public long createdAt { get; set; }
        public long updatedAt { get; set; }
        public string type { get; set; }
        public DataMap dataMap { get; set; }
    }
}
