namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Settings
    {
        public int portalId { get; set; }
        public string timeZone { get; set; }
        public string currency { get; set; }
        public long utcOffsetMilliseconds { get; set; }
        public string utcOffset { get; set; }
    }
}