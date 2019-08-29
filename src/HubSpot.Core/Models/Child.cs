using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Child
    {
        public object is_stub { get; set; }
        public object category_id { get; set; }
        public bool is_deleted { get; set; }
        public object level { get; set; }
        public object parent_guid { get; set; }
        public bool has_children { get; set; }
        public object error_message { get; set; }
        public string link_target { get; set; }
        public int content_group_id { get; set; }
        public string label { get; set; }
        public bool is_active_branch { get; set; }
        public object state { get; set; }
        public object top_level_ancestor_guid { get; set; }
        public bool is_active_node { get; set; }
        public string guid { get; set; }
        public int page_id { get; set; }
        public List<object> children { get; set; }
        public bool is_published { get; set; }
        public string url { get; set; }
    }
}