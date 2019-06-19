using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Contact
    {
        [JsonProperty("addedAt")]
        public long? AddedAt { get; set; }

        [JsonProperty("vid")]
        public int? Vid { get; set; }

        [JsonProperty("canonical-vid")]
        public int? CanonicalVid { get; set; }

        [JsonProperty("merged-vid")]
        public List<int> MergedVids { get; set; }

        [JsonProperty("portal-id")]
        public long? PortalId { get; set; }

        [JsonProperty("is-contact")]
        public bool? IsContact { get; set; }

        [JsonProperty("profile-token")]
        public string ProfileToken { get; set; }

        [JsonProperty("profile-url")]
        public string ProfileUrl { get; set; }

        [JsonProperty("properties")]
        public object Properties { get; set; }

        [JsonProperty("form-submissions")]
        public List<object> FormSubmissions { get; set; }

        [JsonProperty("identity-profiles")]
        public List<object> IdentityProfiles { get; set; }

        [JsonProperty("merge-audits")]
        public List<object> MergeAudits { get; set; }

        [NotMapped]
        public string CompanyId { get; set; }

        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }

        [JsonIgnore]
        public string Photo { get; set; }
    }
}
