using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class LineItemResponse : Response
    {
        public List<LineItem> Objects { get; set; }
        public bool HasMore { get; set; }
        public int? Offset { get; set; }
    }
}
