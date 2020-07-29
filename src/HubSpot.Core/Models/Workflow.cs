using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Workflow
    {
        public List<Action> actions { get; set; }
        public bool allowContactToTriggerMultipleTimes { get; set; }
        public bool enabled { get; set; }
        public List<object> goalListIds { get; set; }
        public long? id { get; set; }
        public long insertedAt { get; set; }
        public string lastUpdatedBy { get; set; }
        public bool listening { get; set; }
        public string name { get; set; }
        public NurtureTimeRange nurtureTimeRange { get; set; }
        public bool onlyExecOnBizDays { get; set; }
        public long? originalAuthorUserId { get; set; }
        public long? portalId { get; set; }
        public RecurringSetting recurringSetting { get; set; }
        public List<object> suppressionListIds { get; set; }
        public List<List<Trigger>> triggerSets { get; set; }
        public string type { get; set; }
        public UnenrollmentSetting unenrollmentSetting { get; set; }
        public long updatedAt { get; set; }      
        public int lastUpdatedByUserId { get; set; }
        public List<object> personaTagIds { get; set; }
        public UpdateSource updateSource { get; set; }
        public CreationSource creationSource { get; set; }
        public ContactListIds contactListIds { get; set; }
        public ContactCounts contactCounts { get; set; }
    }
}
