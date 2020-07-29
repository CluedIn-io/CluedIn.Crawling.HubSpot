using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class ContactResponse : Response
    {
        public List<Contact> contacts { get; set; }

        [JsonProperty("has-more")]
        public bool? hasMore { get; set; }

        [JsonProperty("vid-offset")]
        public int? vidOffset { get; set; }
    }
}