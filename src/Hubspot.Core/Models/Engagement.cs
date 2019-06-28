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

    //public class Associations
    //{
    //    public List<int> contactIds { get; set; }
    //    public List<object> companyIds { get; set; }
    //    public List<object> dealIds { get; set; }
    //    public List<object> ownerIds { get; set; }
    //}

    public class Metadata  // TODO there is a MetaData class already in this namespace, bug ?
    {
        public string body { get; set; }
    }
}
