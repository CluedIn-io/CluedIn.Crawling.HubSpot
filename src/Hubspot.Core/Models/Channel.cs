namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class DataMap
    {
        public string picture { get; set; }
        public string lastName { get; set; }
        public string pageName { get; set; }
        public string profileUrl { get; set; }
        public string email { get; set; }
        public string pageId { get; set; }
        public string userId { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string pageCategory { get; set; }
    }

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
