using CluedIn.Crawling.Hubspot.Core;

namespace CluedIn.Crawling.Hubspot
{
    public class HubspotCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<HubspotCrawlJobData>
    {
        public HubspotCrawlerJobProcessor(HubspotCrawlerComponent component) : base(component)
        {
        }
    }
}
