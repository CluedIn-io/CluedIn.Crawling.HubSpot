using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class TopicResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public int? total { get; set; }
        public int? totalCount { get; set; }
        public List<Topic> objects { get; set; }
    }
}