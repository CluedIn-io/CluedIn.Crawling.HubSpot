namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class HubSpotError
    {
        public string status { get; set; }
        public string message { get; set; }
        public string correlationId { get; set; }
        public string requestId { get; set; }
    }
}
