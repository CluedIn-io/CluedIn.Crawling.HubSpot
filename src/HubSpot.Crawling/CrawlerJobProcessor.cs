using CluedIn.Crawling.HubSpot.Core;

namespace CluedIn.Crawling.HubSpot
{
    public class CrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<HubSpotCrawlJobData>
    {
        public CrawlerJobProcessor(CrawlerComponent component) : base(component)
        {
        }
    }
}
