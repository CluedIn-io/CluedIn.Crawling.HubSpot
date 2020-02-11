using Castle.Windsor;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CrawlerIntegrationTesting.CrawlerHost;

namespace Crawling.HubSpot.Integration.Test
{

    public class TestCrawlerHost : DebugCrawlerHost<HubSpotCrawlJobData>
    {
        public TestCrawlerHost(string binFolder, string providerName) : base(binFolder, providerName)
        {
        }

        public IWindsorContainer ContainerInstance => base.Container;
    }
}
