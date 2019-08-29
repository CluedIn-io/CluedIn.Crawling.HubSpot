using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class EngagementAssociations
    {
        public List<long> companyIds { get; set; }
        public List<object> contactIds { get; set; }
        public List<object> dealIds { get; set; }
        public List<object> ownerIds { get; set; }
        public List<object> workflowIds { get; set; }
        public List<object> ticketIds { get; set; }
    }
}
