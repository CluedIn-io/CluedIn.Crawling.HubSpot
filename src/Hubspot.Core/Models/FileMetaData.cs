namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class FileMetaData
    {
        public string alt_key { get; set; }
        public string alt_key_hash { get; set; }
        public string alt_url { get; set; }
        public bool archived { get; set; }
        public long created { get; set; }
        public long deleted_at { get; set; }
        public string extension { get; set; }
        public object folder_id { get; set; }
        public int? height { get; set; }
        public long id { get; set; }
        public bool is_cta_image { get; set; }
        public Meta meta { get; set; }
        public string name { get; set; }
        public long? portal_id { get; set; }
        public long? size { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public long updated { get; set; }
        public string url { get; set; }
        public int? version { get; set; }
        public int? width { get; set; }
    }
}
