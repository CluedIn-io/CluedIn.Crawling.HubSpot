namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class CreationSource
    {
        public SourceApplication sourceApplication { get; set; }
        public CreatedByUser createdByUser { get; set; }
        public object createdAt { get; set; }
    }
}