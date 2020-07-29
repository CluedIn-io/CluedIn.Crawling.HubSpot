using Castle.MicroKernel.Registration;
using CluedIn.Core.Installers;
using CluedIn.Crawling.HubSpot.Core;
using CrawlerIntegrationTesting.CrawlerHost;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Crawling.HubSpot.Integration.Test
{

    public class TestCrawlerHost : DebugCrawlerHost<HubSpotCrawlJobData>
    {
        public TestCrawlerHost(string binFolder)
            : base(binFolder,
                  HubSpotConstants.ProviderName,
                  container =>
                  {
                      container.Install(new VocabulariesInstaller());
                      container.Register(Component.For<ILogger>().UsingFactoryMethod(_ => NullLogger.Instance).LifestyleSingleton());
                      container.Register(Component.For<ILoggerFactory>().UsingFactoryMethod(_ => NullLoggerFactory.Instance).LifestyleSingleton());
                  })
        {
        }
    }
}
