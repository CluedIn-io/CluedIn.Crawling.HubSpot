using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using System.IO;
using System.Reflection;
using CrawlerIntegrationTesting.Clues;

namespace Tests.Integration.HubSpot
{
    public class HubSpotTestFixture
    {
        public HubSpotTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;
            var p = new DebugCrawlerHost<HubSpotCrawlJobData>(executingFolder, HubSpotConstants.ProviderName);

            ClueStorage = new ClueStorage();

            p.ProcessClue += ClueStorage.AddClue;

            p.Execute(HubSpotConfiguration.Create(), HubSpotConstants.ProviderId);
        }

        public ClueStorage ClueStorage { get; }

        public void Dispose()
        {
        }

    }
}


