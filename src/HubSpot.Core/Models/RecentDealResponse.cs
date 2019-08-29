using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class RecentDealResponse : Response
    {
        public List<Deal> results { get; set; }
        public bool hasMore { get; set; }
        public int offset { get; set; }
    }
}