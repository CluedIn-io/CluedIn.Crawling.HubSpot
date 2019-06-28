using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Browser
    {
        public string family { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public string producerUrl { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public List<object> version { get; set; }
    }
}