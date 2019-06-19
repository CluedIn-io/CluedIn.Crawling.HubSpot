using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class WorkflowsResponse : Response
    {
        public List<Workflow> workflows { get; set; }
    }
}