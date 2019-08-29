using System.IO;
using System.Reflection;
using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Installers;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CrawlerIntegrationTesting.Clues;
using Crawling.HubSpot.Test.Common;

namespace Crawling.HubSpot.Integration.Test
{
    public class HubSpotTestFixture
    {
        //private readonly ITestOutputHelper _outputHelper;

        public HubSpotTestFixture()
        {
            //_outputHelper = new TestOutputHelper();

            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;

            //_outputHelper.WriteLine($"Creating crawler host {HubSpotConstants.ProviderName} from folder {executingFolder}");

            var crawlerHost = new TestCrawlerHost(executingFolder, HubSpotConstants.ProviderName);

            crawlerHost.ContainerInstance.Install(new VocabulariesInstaller());

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
