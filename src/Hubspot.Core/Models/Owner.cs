using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Owner
    {
        public long? portalId { get; set; }
        public long? ownerId { get; set; }
        public string type { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public long createdAt { get; set; }
        public long updatedAt { get; set; }
        public List<RemoteList> remoteList { get; set; }
    }
}
