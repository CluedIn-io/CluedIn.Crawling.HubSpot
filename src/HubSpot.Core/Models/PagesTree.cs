using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class PagesTree
    {
        public object is_stub { get; set; }
        public object category_id { get; set; }
        public bool is_deleted { get; set; }
        public object level { get; set; }
        public object parent_guid { get; set; }
        public bool has_children { get; set; }
        public object error_message { get; set; }
        public string link_target { get; set; }
        public long? content_group_id { get; set; }
        public string label { get; set; }
        public bool is_active_branch { get; set; }
        public object state { get; set; }
        public object top_level_ancestor_guid { get; set; }
        public bool is_active_node { get; set; }
        public object guid { get; set; }
        public long? page_id { get; set; }
        public List<Child> children { get; set; }
        public bool is_published { get; set; }
    }
}