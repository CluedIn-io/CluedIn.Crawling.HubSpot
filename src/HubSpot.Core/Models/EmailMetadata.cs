using System.Collections.Generic;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class EmailMetadata
    {
        public List<EmailPerson> bcc { get; set; }
        public List<EmailPerson> cc { get; set; }
        public EmailPerson from { get; set; }
        public List<EmailPerson> to { get; set; }
        public string subject { get; set; }
        public string html { get; set; }
        public string body { get; set; }
        public string text { get; set; }
        public string facsimileSendId { get; set; }
        public string loggedFrom { get; set; }
        public string messageId { get; set; }
        public string sentVia { get; set; }
        public string status { get; set; }
        public string threadId { get; set; }
        public string trackerKey { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> custom { get; set; } = new Dictionary<string, object>();
    }
}