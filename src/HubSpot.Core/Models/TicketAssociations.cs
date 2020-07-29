using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class TicketAssociations
    {
        public List<long> Contacts { get; set; } = new List<long>();
        public List<long> Engagements { get; set; } = new List<long>();
        public List<long> Companies { get; set; } = new List<long>();
    }
}
