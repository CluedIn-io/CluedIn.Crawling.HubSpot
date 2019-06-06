using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class SiteMap
    {
        public long created { get; set; }
        public long deleted_at { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public PagesTree pages_tree { get; set; }
        public long? portal_id { get; set; }
        public long updated { get; set; }
    }

    public class SiteMapResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<SiteMap> objects { get; set; }
        public int? total_count { get; set; }
    }
}
