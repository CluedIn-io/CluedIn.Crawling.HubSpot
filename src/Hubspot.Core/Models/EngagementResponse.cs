using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class EngagementResponse : Response
    {
        public bool hasMore { get; set; }
        public long? offset { get; set; }
        public List<EngagementResult> results { get; set; }
    }

    public class EngagementAssociations
    {
        public List<int> companyIds { get; set; }
        public List<object> contactIds { get; set; }
        public List<object> dealIds { get; set; }
        public List<object> ownerIds { get; set; }
        public List<object> workflowIds { get; set; }
        public List<object> ticketIds { get; set; }
    }

    public class EngagementEngagement
    {
        public bool? active { get; set; }
        public object createdAt { get; set; }
        public int? createdBy { get; set; }
        public long? id { get; set; }
        public object lastUpdated { get; set; }
        public int? modifiedBy { get; set; }
        public int? ownerId { get; set; }
        public int? portalId { get; set; }
        public object timestamp { get; set; }
        public string type { get; set; }
    }

    public class EngagementMetadata
    {
        public string body { get; set; }
        public string forObjectType { get; set; }
        public List<object> reminders { get; set; }
        public string status { get; set; }
        public int? durationMilliseconds { get; set; }
        public string subject { get; set; }
        public string taskType { get; set; }
        public bool? sendDefaultReminder { get; set; }
        public long? startTime { get; set; }
        public long? endTime { get; set; }
        public string title { get; set; }
        public List<object> preMeetingProspectReminders { get; set; }
    }
}

