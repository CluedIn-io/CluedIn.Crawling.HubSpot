using CluedIn.Crawling;
using CluedIn.Crawling.Hubspot.Core;
using System.IO;
using System.Reflection;
using CrawlerIntegrationTesting.Clues;

namespace Tests.Integration.Hubspot
{
    public class HubspotTestFixture
    {
        public HubspotTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;
            var p = new DebugCrawlerHost<HubspotCrawlJobData>(executingFolder, HubspotConstants.ProviderName);

            ClueStorage = new ClueStorage();

            p.ProcessClue += ClueStorage.AddClue;            

            p.Execute(HubspotConfiguration.Create(), HubspotConstants.ProviderId);
        }

        public ClueStorage ClueStorage { get; }

        public void Dispose()
        {
        }

    }
}


