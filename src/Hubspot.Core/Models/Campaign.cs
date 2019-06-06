using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Counters
    {
        public int? delivered { get; set; }
        public int? open { get; set; }
        public int? processed { get; set; }
        public int? sent { get; set; }
    }
    public class Campaign
    {
        public long? appId { get; set; }
        public string appName { get; set; }
        public long? contentId { get; set; }
        public Counters counters { get; set; }
        public long? id { get; set; }
        public string name { get; set; }
        public int? numIncluded { get; set; }
        public int? numQueued { get; set; }
        public string subType { get; set; }
        public string subject { get; set; }
        public string type { get; set; }

    }


    /*
    *
    *{
    "appId": 20185,
    "appName": "AbBatch",
    "contentId": 933742756,
    "counters": {
        "delivered": 1,
        "open": 1,
        "processed": 1,
        "sent": 1
    },
    "id": 13054799,
    "name": "Test AB (Variation) ",
    "numIncluded": 1,
    "numQueued": 1,
    "subType": "VariantB",
    "subject": "test",
    "type": "AB"
}
    *
    */

}
