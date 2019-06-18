using System.Collections.Generic;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using Moq;
using RestSharp;
using Xunit;
using Client = CluedIn.Crawling.HubSpot.Infrastructure.HubSpotClient;
using Task = System.Threading.Tasks.Task;

namespace Crawling.HubSpot.Integration.Test.HubSpotClient
{
    public class HappyPath
    {
        private readonly Client _sut;

        public HappyPath()
        {
            var crawlJobData = new HubSpotCrawlJobData(HubSpotConfiguration.Create());

            var logger = new Mock<ILogger>();

            _sut = new Client(logger.Object, crawlJobData, new RestClient("http://127.0.0.1:8080/"));
        }

        [Fact]
        public void IsNotNull()
        {
            Assert.NotNull(_sut);
        }

        [Fact]
        public async Task SettingsAreAvailable()
        {
            var settings = await _sut.GetSettingsAsync();
            Assert.NotNull(settings);
        }

        [Fact]
        public async Task CompanyPropertiesAreAvailable()
        {
            var companyProperties = await _sut.GetCompanyPropertiesAsync(new Settings());
            Assert.NotNull(companyProperties);
        }

        [Fact]
        public async Task UpTo100CompaniesAreAvailable()
        {
            var companies = await _sut.GetCompaniesAsync(new List<string>());
            Assert.InRange(companies.results.Count,1,100);
        }
    }
}
