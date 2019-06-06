using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Deal
    {
        public long? portalId { get; set; }
        public long? dealId { get; set; }
        public Associations associations { get; set; }
        public object properties { get; set; }
        public bool isDeleted { get; set; }
        public List<object> imports { get; set; }
        public List<object> stateChanges { get; set; }
        [JsonIgnore]
        public string Currency { get; set; }
    }

    public class Associations
    {
        public List<long> associatedVids { get; set; }
        public List<long> associatedCompanyIds { get; set; }
        public List<long> associatedDealIds { get; set; }
    }

    public class DealResponse : Response
    {
        public List<Deal> deals { get; set; }
        public bool hasMore { get; set; }
        public int offset { get; set; }
    }

    /*
    *{
            "portalId": 62515,
            "dealId": 3,
            "isDeleted": false,
            "associations": {
                "associatedVids": [
                    1
                ],
                "associatedCompanyIds": [],
                "associatedDealIds": []
            },
            "properties": {
    *
    */
   
}
