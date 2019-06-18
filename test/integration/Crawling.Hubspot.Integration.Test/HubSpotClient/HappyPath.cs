using System.Collections.Generic;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using Moq;
using RestSharp;
using Xunit;
using Client = CluedIn.Crawling.HubSpot.Infrastructure.HubSpotClient;
using Task = System.Threading.Tasks.Task;

namespace Crawling.Hubspot.Integration.Test.HubSpotClient
{
    public class HappyPath
    {
        private readonly Mock<ILogger> _logger;
        private readonly Client _sut;
        public HappyPath()
        {
            var crawlJobData = new HubSpotCrawlJobData(HubSpotConfiguration.Create());

            _logger = new Mock<ILogger>();

            _sut = new Client(_logger.Object, crawlJobData, new RestClient());
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
