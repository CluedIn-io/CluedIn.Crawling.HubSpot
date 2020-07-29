namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Stage
    {
        public string stageId { get; set; }
        public string label { get; set; }
        public double probability { get; set; }
        public bool active { get; set; }
        public int displayOrder { get; set; }
        public bool closedWon { get; set; }
    }
}