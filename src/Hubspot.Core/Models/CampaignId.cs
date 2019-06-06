using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class CampaignId
    {
        public long? id { get; set; }
        public long? appId { get; set; }
        public string appName { get; set; }

    }

    /*
    * "id": 16275278,
            "appId": 113,
            "appName": "Batch"
    */

}
