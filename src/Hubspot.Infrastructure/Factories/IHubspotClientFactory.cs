using CluedIn.Crawling.Hubspot.Core;

namespace CluedIn.Crawling.Hubspot.Infrastructure.Factories
{
    public interface IHubspotClientFactory
    {
        HubspotClient CreateNew(HubspotCrawlJobData hubspotCrawlJobData);
    }
}
