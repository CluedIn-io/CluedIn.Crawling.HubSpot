using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Company
    {
        public long? portalId { get; set; }
        public long? companyId { get; set; }
        public bool isDeleted { get; set; }
        public object properties { get; set; }
        [JsonIgnore]
        public string Currency { get; set; }
    }

    //Content = "{\"has-more\":false,\"offset\":694367888,\"companies\":[{\"portalId\":4275951,\"companyId\":686724251,\"isDeleted\":false,\"properties\":{},\"additionalDomains\":[],\"stateChanges\":[],\"mergeAudits\":[]},{\"portalId\":4275951,\"companyId\":686724958,\"isD...
}
