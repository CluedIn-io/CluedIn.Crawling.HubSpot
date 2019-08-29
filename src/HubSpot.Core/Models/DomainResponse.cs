using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class DomainResponse : Response
    {
        public int? limit { get; set; }
        public List<Domain> objects { get; set; }
        public int? offset { get; set; }
        public int? total { get; set; }
        public int? totalCount { get; set; }
    }
}