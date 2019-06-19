namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Ticket
    {
        public string ObjectType { get; set; }
        public int? PortalId { get; set; }
        public int? ObjectId { get; set; }
        public object Properties { get; set; }
        public int? Version { get; set; }
        public bool IsDeleted { get; set; }
        public TicketAssociations Associations { get; set; } = new TicketAssociations();
    }
}
