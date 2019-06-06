using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class TicketResponse : Response
    {
        public List<Ticket> Objects { get; set; }
        public bool HasMore { get; set; }
        public int? Offset { get; set; }
    }

    public class Ticket
    {
        public string ObjectType { get; set; }
        public int? PortalId { get; set; }
        public int? ObjectId { get; set; }
        public object Properties { get; set; }
        public int? Version { get; set; }
        public bool IsDeleted { get; set; }
        public TicketAssociations Associations { get; set; }
    }

    public class TicketAssociations
    {
        public List<long> Contacts { get; set; }
        public List<long> Engagements { get; set; }
        public List<long> Companies { get; set; }
    }
}
