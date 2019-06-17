using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class UrlMapping
    {
        public long id { get; set; }
        public long? portalId { get; set; }
        public long created { get; set; }
        public long updated { get; set; }
        public string routePrefix { get; set; }
        public string destination { get; set; }
        public int redirectStyle { get; set; }
        public object contentGroupId { get; set; }
        public bool isOnlyAfterNotFound { get; set; }
        public bool isRegex { get; set; }
        public bool isMatchFullUrl { get; set; }
        public bool isMatchQueryString { get; set; }
        public bool isPattern { get; set; }
        public string name { get; set; }
        public int precedence { get; set; }
        public int deletedAt { get; set; }
    }

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
