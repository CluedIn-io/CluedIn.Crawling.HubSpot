using Newtonsoft.Json;
using System.Collections.Generic;

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
