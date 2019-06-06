using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Action
    {
        public string type { get; set; }
        public string propertyName { get; set; }
        public string newValue { get; set; }
    }

    public class NurtureTimeRange
    {
        public bool enabled { get; set; }
        public int startHour { get; set; }
        public int stopHour { get; set; }
    }

    public class RecurringSetting
    {
        public string type { get; set; }
    }

    public class UnenrollmentSetting
    {
        public List<object> excludedWorkflows { get; set; }
        public string type { get; set; }
    }

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

    public class WorkflowsResponse : Response
    {
        public List<Workflow> workflows { get; set; }
    }

    public class SourceApplication
    {
        public string source { get; set; }
        public string serviceName { get; set; }
    }

    public class UpdatedByUser
    {
        public int userId { get; set; }
        public string userEmail { get; set; }
    }

    public class UpdateSource
    {
        public SourceApplication sourceApplication { get; set; }
        public UpdatedByUser updatedByUser { get; set; }
        public object updatedAt { get; set; }
    }

    public class CreatedByUser
    {
        public int userId { get; set; }
        public string userEmail { get; set; }
    }

    public class CreationSource
    {
        public SourceApplication sourceApplication { get; set; }
        public CreatedByUser createdByUser { get; set; }
        public object createdAt { get; set; }
    }

    public class ContactListIds
    {
        public int enrolled { get; set; }
        public int active { get; set; }
        public List<object> steps { get; set; }
    }

    public class ContactCounts
    {
        public int active { get; set; }
        public int enrolled { get; set; }
    }

    public class Trigger
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}
