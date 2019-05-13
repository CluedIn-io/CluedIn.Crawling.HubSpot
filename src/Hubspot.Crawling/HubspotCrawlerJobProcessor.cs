using CluedIn.Crawling.HubSpot.Core;

namespace CluedIn.Crawling.HubSpot
{
    public class HubSpotCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<HubSpotCrawlJobData>
    {
        public HubSpotCrawlerJobProcessor(HubSpotCrawlerComponent component) : base(component)
        {
        }
    }
}
