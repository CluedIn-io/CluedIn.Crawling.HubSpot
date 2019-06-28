using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class DealPipeline
    {
        public string pipelineId { get; set; }
        public List<Stage> stages { get; set; }
        public string label { get; set; }
        public bool active { get; set; }
        public int displayOrder { get; set; }
        public long portalId { get; set; }
    }
}
