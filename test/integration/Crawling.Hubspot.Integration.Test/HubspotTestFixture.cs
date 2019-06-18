using System.IO;
using System.Reflection;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CrawlerIntegrationTesting.Clues;

namespace Crawling.Hubspot.Integration.Test
{
    public class HubSpotTestFixture
    {
        //private readonly ITestOutputHelper _outputHelper;

        public HubSpotTestFixture()
        {
            //_outputHelper = new TestOutputHelper();

            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;

            //_outputHelper.WriteLine($"Creating crawler host {HubSpotConstants.ProviderName} from folder {executingFolder}");

            var crawlerHost = new DebugCrawlerHost<HubSpotCrawlJobData>(executingFolder, HubSpotConstants.ProviderName);

            ClueStorage = new ClueStorage();

            crawlerHost.ProcessClue += CrawlerHost_ProcessClue;

            var credentials = HubSpotConfiguration.Create();

            //_outputHelper.WriteLine($"Executing crawler host {HubSpotConstants.ProviderName} ({HubSpotConstants.ProviderId}) with credentials {JsonConvert.SerializeObject(credentials)}");

            crawlerHost.Execute(credentials, HubSpotConstants.ProviderId);

            //_outputHelper.WriteLine($"Executing crawler host {HubSpotConstants.ProviderName} completed");
        }

        public ClueStorage ClueStorage { get; }

        private void CrawlerHost_ProcessClue(CluedIn.Core.Data.Clue clue)
        {
            //_outputHelper.WriteLine($"Processing crawler clue {JsonConvert.SerializeObject(clue)}");

            ClueStorage.AddClue(clue);
        }
    }
}
