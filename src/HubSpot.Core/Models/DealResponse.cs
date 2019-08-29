using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class DealResponse : Response
    {
        public List<Deal> deals { get; set; }
        public bool hasMore { get; set; }
        public int offset { get; set; }
    }
}