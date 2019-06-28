using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Associations
    {
        public List<long> associatedVids { get; set; }
        public List<long> associatedCompanyIds { get; set; }
        public List<long> associatedDealIds { get; set; }
    }
}