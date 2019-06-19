using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class ProductResponse : Response
    {
        public List<Product> Objects { get; set; }
        public bool HasMore { get; set; }
        public int? Offset { get; set; }
    }
}
