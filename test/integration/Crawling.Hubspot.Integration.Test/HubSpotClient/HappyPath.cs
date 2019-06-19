using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using Moq;
using RestSharp;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Crawling.HubSpot.Integration.Test.HubSpotClient
{
    public class HappyPath
    {
        private readonly CluedIn.Crawling.HubSpot.Infrastructure.HubSpotClient _sut;

        public HappyPath()
        {
            var crawlJobData = new HubSpotCrawlJobData(HubSpotConfiguration.Create());

            var logger = new Mock<ILogger>();

            _sut = new CluedIn.Crawling.HubSpot.Infrastructure.HubSpotClient(logger.Object, crawlJobData,
                new RestClient());
        }

        [Fact]
        public void IsNotNull()
        {
            Assert.NotNull(_sut);
        }

        [Fact]
        public async Task SettingsAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetSettingsAsync());
        }

        [Theory]
        [InlineAutoData]
        public async Task CompanyPropertiesAreAvailable(Settings settings)
        {
            Assert.NotEmpty(
                await _sut.GetCompanyPropertiesAsync(settings));
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100CompaniesAreAvailable(List<string> properties)
        {
            Assert.InRange(
                (await _sut.GetCompaniesAsync(properties)).results.Count,
                0,100);
        }

        [Theory]
        [InlineAutoData]
        public async Task ContactPropertiesAreAvailable(Settings settings)
        {
            Assert.NotEmpty(
                await _sut.GetContactPropertiesAsync(settings));
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20ResultsInEngagementsQueryableByIdAndType(long objectId, string objectType)
        {
            Assert.InRange(
                (await _sut.GetEngagementByIdAndTypeAsync(objectId, objectType)).Count(),
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task DealPropertiesAreAvailable(Settings settings)
        {
            Assert.NotEmpty(
                await _sut.GetDealPropertiesAsync(settings));
        }

        [Theory]
        [InlineAutoData]
        public async Task ProductPropertiesAreAvailable(Settings settings)
        {
            Assert.NotEmpty(
                await _sut.GetProductPropertiesAsync(settings));
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100ProductsAreAvailable(List<string> properties)
        {
            Assert.InRange(
                (await _sut.GetProductsAsync(properties)).Objects.Count,
                0, 100);
        }

        [Theory]
        [InlineAutoData]
        public async Task LineItemPropertiesAreAvailable(Settings settings)
        {
            Assert.NotNull(
                await _sut.GetLineItemPropertiesAsync(settings));
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100LineItemsAreAvailable(List<string> properties)
        {
            Assert.InRange(
                (await _sut.GetLineItemsAsync(properties)).Objects.Count,
                0, 100);
        }

        [Theory]
        [InlineAutoData]
        public async Task TicketPropertiesAreAvailable(Settings settings)
        {
            Assert.NotNull(
                await _sut.GetTicketPropertiesAsync(settings));
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100TicketsAreAvailable(List<string> properties)
        {
            Assert.InRange(
                (await _sut.GetTicketsAsync(properties)).Objects.Count,
                0, 100);
        }

        [Fact]
        public async Task UpTo20DynamicContactListsAreAvailable()
        {
            Assert.InRange(
                (await _sut.GetDynamicContactListsAsync()).contacts.Count,
                0, 20);
        }

        [Fact]
        public async Task UpTo20StaticContactListsAreAvailable()
        {
            Assert.InRange(
                (await _sut.GetStaticContactListsAsync()).lists.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100DealAssociationsAreAvailable(int objectId)
        {
            Assert.InRange(
                (await _sut.GetDealAssociationsAsync(objectId)).Results.Count,
                0, 100);
        }
    }
}
