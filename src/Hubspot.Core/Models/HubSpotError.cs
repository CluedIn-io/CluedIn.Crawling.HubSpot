using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class HubSpotError
    {
        public string status { get; set; }
        public string message { get; set; }
        public string correlationId { get; set; }
        public string requestId { get; set; }
    }
}
