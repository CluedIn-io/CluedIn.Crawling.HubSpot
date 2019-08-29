namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class MetaData
    {
        public long? lastProcessingStateChangeAt { get; set; }

        public string processing { get; set; }

        public long? lastSizeChangeAt { get; set; }

        public string error { get; set; }

        public ulong? size { get; set; }

    }
}