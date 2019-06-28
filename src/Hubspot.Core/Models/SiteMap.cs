namespace CluedIn.Crawling.HubSpot.Core.Models
{
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
}
