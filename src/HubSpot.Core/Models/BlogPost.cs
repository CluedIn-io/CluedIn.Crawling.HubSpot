using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class BlogPost
    {
        public bool archived { get; set; }
        public long? author_user_id { get; set; }
        public BlogAuthor blog_author { get; set; }
        public long? blog_author_id { get; set; }
        public object campaign { get; set; }
        public object campaign_name { get; set; }
        public object cloned_from { get; set; }
        public int comment_count { get; set; }
        public long? content_group_id { get; set; }
        public long? created { get; set; }
        public long? deleted_at { get; set; }
        public string featured_image { get; set; }
        public string footer_html { get; set; }
        public long? freeze_date { get; set; }
        public bool has_user_changes { get; set; }
        public string head_html { get; set; }
        public string html_title { get; set; }
        public long id { get; set; }
        public bool is_draft { get; set; }
        public string meta_description { get; set; }
        public object meta_keywords { get; set; }
        public string name { get; set; }
        public object performable_url { get; set; }
        public long? portal_id { get; set; }
        public string post_body { get; set; }
        public string post_summary { get; set; }
        public object preview_image_src { get; set; }
        public string preview_key { get; set; }
        public string processing_status { get; set; }
        public long? publish_date { get; set; }
        public object publish_immediately { get; set; }
        public string published_url { get; set; }
        public string rss_body { get; set; }
        public string rss_summary { get; set; }
        public string slug { get; set; }
        public string state { get; set; }
        public long? style_override_id { get; set; }
        public string subcategory { get; set; }
        public List<object> topic_ids { get; set; }
        public long? updated { get; set; }
        public string url { get; set; }
        public object widget_containers { get; set; }
        public object widgetcontainers { get; set; }
        public object widgets { get; set; }
    }
}
