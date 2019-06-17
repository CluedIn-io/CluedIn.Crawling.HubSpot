using System.Collections.Generic;

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

    public class TopicResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public int? total { get; set; }
        public int? totalCount { get; set; }
        public List<Topic> objects { get; set; }
    }
}
