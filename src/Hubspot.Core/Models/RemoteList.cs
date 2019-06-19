namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class RemoteList
    {
        public long? portalId { get; set; }
        public long? ownerId { get; set; }
        public string remoteId { get; set; }
        public string remoteType { get; set; }
        public bool active { get; set; }
    }
}