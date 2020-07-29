namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class WebHookResponse
    {
        public int id { get; set; }
        public int createdAt { get; set; }
        public int createdBy { get; set; }
        public SubscriptionDetails subscriptionDetails { get; set; }
        public bool enabled { get; set; }
    }
}
