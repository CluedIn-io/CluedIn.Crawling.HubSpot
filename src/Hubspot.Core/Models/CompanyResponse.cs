using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class CompanyResponse : Response
    {
        [JsonProperty("companies")]
        public List<Company> results { get; set; }

        [JsonProperty("has-more")]
        public bool hasMore { get; set; }

        [JsonProperty("offset")]
        public int? offset { get; set; }

        [JsonProperty("total")]
        public int? total { get; set; }
    }
}