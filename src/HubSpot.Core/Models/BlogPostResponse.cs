using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class BlogPostResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        [JsonProperty("total_count")]
        public int? totalCount { get; set; }
        public List<BlogPost> objects { get; set; }
    }
}