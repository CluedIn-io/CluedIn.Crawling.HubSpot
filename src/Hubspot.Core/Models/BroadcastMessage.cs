using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class BroadcastMessage
    {
        public string broadcastGuid { get; set; }
        public long? portalId { get; set; }
        public string groupGuid { get; set; }
        public string channelGuid { get; set; }
        public int clicks { get; set; }
        public string status { get; set; }
        public string messageUrl { get; set; }
        public string foreignId { get; set; }
        public long? createdAt { get; set; }
        public long? triggerAt { get; set; }
        public long? finishedAt { get; set; }
        public string campaignGuid { get; set; }
        public string message { get; set; }
        public string linkGuid { get; set; }
        public string taskQueueId { get; set; }
        public string linkTaskQueueId { get; set; }
        public object remoteContentId { get; set; }
        public object remoteContentType { get; set; }
        public object clientTag { get; set; }
        public long? createdBy { get; set; }
        public long? updatedBy { get; set; }
        public List<object> interactions { get; set; }
        public object interactionCounts { get; set; }
    }
}
