using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Identities
    {
        public string type { get; set; }
        public List<Value> values { get; set; }
    }
}