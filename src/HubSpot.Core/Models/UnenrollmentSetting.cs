using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class UnenrollmentSetting
    {
        public List<object> excludedWorkflows { get; set; }
        public string type { get; set; }
    }
}