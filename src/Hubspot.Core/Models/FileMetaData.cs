using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Thumb
    {
        public string cloud_key { get; set; }
        public string friendly_url { get; set; }
        public string s3_url { get; set; }
        public string image_name { get; set; }
    }

    public class Thumbs
    {
        public Thumb medium { get; set; }
        public Thumb thumb { get; set; }
        public Thumb icon { get; set; }
    }

    public class Meta
    {
        public Thumbs thumbs { get; set; }
    }

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

    public class FileMetaDataResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<FileMetaData> objects { get; set; }
        public int? total_count { get; set; }
    }
}
