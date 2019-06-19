namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Topic
    {
        public long id { get; set; }
        public long? portalId { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public long? created { get; set; }
        public long? updated { get; set; }
        public long? deletedAt { get; set; }
    }
}
