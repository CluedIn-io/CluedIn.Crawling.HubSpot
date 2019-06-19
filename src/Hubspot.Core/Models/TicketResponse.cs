using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class TicketResponse : Response
    {
        public List<Ticket> Objects { get; set; }
        public bool HasMore { get; set; }
        public int? Offset { get; set; }
    }
}
