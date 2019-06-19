namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Domain
    {
        public string actual_cname { get; set; }
        public string actual_ip { get; set; }
        public int consecutive_non_resolving_count { get; set; }
        public long created { get; set; }
        public string domain { get; set; }
        public object full_category_key { get; set; }
        public long id { get; set; }
        public bool is_any_primary { get; set; }
        public bool is_dns_correct { get; set; }
        public bool is_internal_domain { get; set; }
        public bool is_legacy { get; set; }
        public bool is_legacy_domain { get; set; }
        public bool is_resolving { get; set; }
        public bool manually_marked_as_resolving { get; set; }
        public long? portal_id { get; set; }
        public bool primary_blog_post { get; set; }
        public bool primary_email { get; set; }
        public bool primary_landing_page { get; set; }
        public bool primary_legacy_page { get; set; }
        public bool primary_site_page { get; set; }
        public string secondary_to_domain { get; set; }
        public long updated { get; set; }
    }
}
