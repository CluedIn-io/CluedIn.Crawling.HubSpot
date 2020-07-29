using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class ContactListResponse : Response
    {
        public List<ContactList> contacts { get; set; }

        [JsonProperty("has-more")]
        public bool hasMore { get; set; }

        [JsonProperty("offset")]
        public int? offset { get; set; }

        public List<ContactList> lists { get; set; }
 
    }
}