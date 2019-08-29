using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class IdentityProfile
    {
        public int vid { get; set; }

        [JsonProperty("saved-at-timestamp")]
        public long savedtimestamp { get; set; }

        [JsonProperty("deleted-changed-timestamp")]
        public int deletedtimestamp { get; set; }

        public List<Identity> identities { get; set; }
    }
}
