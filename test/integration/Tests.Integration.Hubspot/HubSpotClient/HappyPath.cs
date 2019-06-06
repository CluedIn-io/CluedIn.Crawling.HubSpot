using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluedIn.Core.Logging;
using Client = CluedIn.Crawling.HubSpot.Infrastructure.HubSpotClient;
using Moq;
using CluedIn.Crawling.HubSpot.Core;
using RestSharp;
using Xunit;

namespace Tests.Integration.HubSpot.HubSpotClient
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
            var companyProperties = await _sut.GetCompanyPropertiesAsync();
            Assert.NotNull(companyProperties);
        }

        [Fact]
        public async Task UpTo100CompaniesAreAvailable()
        {
            var companies = await _sut.GetCompaniesAsync();
            Assert.InRange(companies.results.Count,1,100);
        }
    }
}
