using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class EngagementResponse : Response
    {
        public bool hasMore { get; set; }
        public long? offset { get; set; }
        public List<EngagementResult> results { get; set; }
    }
}

