using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class TemplateResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<Template> objects { get; set; }
        public int? total_count { get; set; }
    }
}