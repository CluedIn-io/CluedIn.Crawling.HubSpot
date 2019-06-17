using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Template
    {
        public long? category_id { get; set; }
        public string cdn_minified_url { get; set; }
        public string cdn_url { get; set; }
        public long deleted_at { get; set; }
        public string folder { get; set; }
        public object generated_from_layout_id { get; set; }
        public long id { get; set; }
        public bool is_available_for_new_content { get; set; }
        public bool is_from_layout { get; set; }
        public bool is_read_only { get; set; }
        public string label { get; set; }
        public string linked_style_id { get; set; }
        public string path { get; set; }
        public long? portal_id { get; set; }
        public string source { get; set; }
        public string template_type { get; set; }
        public int thumbnail_width { get; set; }
        public string type { get; set; }
        public long updated { get; set; }
        public string updated_by { get; set; }
    }

    public class TemplateResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<Template> objects { get; set; }
        public int? total_count { get; set; }
    }
}
