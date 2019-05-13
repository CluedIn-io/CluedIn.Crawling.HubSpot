using CluedIn.Crawling.HubSpot.Core;

namespace CluedIn.Crawling.HubSpot.Infrastructure.Factories
{
    public interface IHubSpotClientFactory
    {
        HubSpotClient CreateNew(HubSpotCrawlJobData hubspotCrawlJobData);
    }
}
