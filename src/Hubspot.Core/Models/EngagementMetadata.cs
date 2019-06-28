using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
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