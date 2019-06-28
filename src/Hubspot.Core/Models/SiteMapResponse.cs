using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class SiteMapResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<SiteMap> objects { get; set; }
        public int? total_count { get; set; }
    }
}