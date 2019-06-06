using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class SmtpToken
    {
        public long? portalId { get; set; }
        public string userName { get; set; }
        public long? emailCampaignId { get; set; }
        public long createdAt { get; set; }
        public bool deleted { get; set; }
        public string createdBy { get; set; }
        public int appId { get; set; }
        public string campaignName { get; set; }
    }
}
