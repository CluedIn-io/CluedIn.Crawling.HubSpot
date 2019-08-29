namespace CluedIn.Crawling.HubSpot.Core.Models
{
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