using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class ProductResponse : Response
    {
        public List<Product> Objects { get; set; }
        public bool HasMore { get; set; }
        public int? Offset { get; set; }
    }

    public class Product
    {
        public string ObjectType { get; set; }
        public int? PortalId { get; set; }
        public int? ObjectId { get; set; }
        public object Properties { get; set; }
        public int? Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
