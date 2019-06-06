using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class CompanyResponse : Response
    {
        [JsonProperty("companies")]
        public List<Company> results { get; set; }

        [JsonProperty("has-more")]
        public bool hasMore { get; set; }

        [JsonProperty("offset")]
        public int? offset { get; set; }

        [JsonProperty("total")]
        public int? total { get; set; }
    }

    public class Settings
    {
        public int portalId { get; set; }
        public string timeZone { get; set; }
        public string currency { get; set; }
        public long utcOffsetMilliseconds { get; set; }
        public string utcOffset { get; set; }
    }
}
