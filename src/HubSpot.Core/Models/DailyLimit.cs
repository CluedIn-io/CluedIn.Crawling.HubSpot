namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class DailyLimit
    {
        public long usageLimit { get; set; }
        public long currentUsage { get; set; }
        public long resetsAt { get; set; }
    }
}