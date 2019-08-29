using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class UrlMappingResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<UrlMapping> objects { get; set; }
        public int? total_count { get; set; }
        public int? total { get; set; }
        public object message { get; set; }
    }
}