using System;
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
                0, 100);
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

        [Theory]
        [InlineAutoData]
        public async Task UpTo100ContactsFromAllListsAreAvailable(List<string> properties)
        {
            Assert.InRange(
                (await _sut.GetContactsFromAllListsAsync(properties)).contacts.Count,
                0, 100);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100DealsAreAvailable(List<string> properties, Settings settings)
        {
            Assert.InRange(
                (await _sut.GetDealsAsync(properties, settings)).deals.Count,
                0, 100);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20FilesAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetFilesAsync(greaterThanEpoch)).objects.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20SocialCalendarEventsAreAvailable(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            Assert.InRange(
                (await _sut.GetSocialCalendarEventsAsync(startDate, endDate)).Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20TaskCalendarEventsAreAvailable(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            Assert.InRange(
                (await _sut.GetTaskCalendarEventsAsync(startDate, endDate)).Count,
                0, 20);

        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20RecentDealsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetRecentDealsAsync(greaterThanEpoch)).deals.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20RecentlyCreatedDealsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetRecentlyCreatedDealsAsync(greaterThanEpoch)).deals.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100BroadcastMessagesOfDealsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetBroadcastMessagesAsync(greaterThanEpoch)).deals.Count,
                0, 100);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo100UrlMappingsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetUrlMappingsAsync(greaterThanEpoch)).objects.Count,
                0, 100);
        }

        [Fact]
        public async Task UpTo20TemplatesAreAvailable()
        {
            Assert.InRange(
                (await _sut.GetTemplatesAsync()).objects.Count,
                0, 20);
        }

        [Fact]
        public async Task UpTo100EngagementsAreAvailable()
        {
            Assert.InRange(
                (await _sut.GetEngagementsAsync()).results.Count,
                0, 100);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20SiteMapsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetSiteMapsAsync(greaterThanEpoch)).objects.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20BlogPostsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetBlogPostsAsync(greaterThanEpoch)).objects.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20BlogTopicsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetBlogTopicsAsync(greaterThanEpoch)).objects.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20BlogsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetBlogsAsync(greaterThanEpoch)).objects.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20DomainsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.InRange(
                (await _sut.GetDomainsAsync(greaterThanEpoch)).objects.Count,
                0, 20);
        }

        [Theory]
        [InlineAutoData]
        public async Task UpTo20TableRowsAreAvailable(DateTimeOffset greaterThanEpoch, long tableId, Column dateColumn, long portalId)
        {
            Assert.InRange(
                (await _sut.GetTableRowsAsync(greaterThanEpoch, tableId, dateColumn, portalId)).Objects.Count,
                0, 20);
        }

        [Fact]
        public async Task FormsAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetFormsAsync());
        }

        [Fact]
        public async Task TablesAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetTablesAsync());
        }

        [Fact]
        public async Task WorkFlowsAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetWorkflowsAsync());
        }

        [Fact]
        public async Task SmtpTokensAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetSmtpTokensAsync());
        }

        [Fact]
        public async Task PublishingChannelsAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetPublishingChannelsAsync());
        }

        [Fact]
        public async Task OwnersAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetOwnersAsync());
        }

        [Fact]
        public async Task KeywordsAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetKeywordsAsync());
        }

        [Fact]
        public async Task DealPipelinesAreAvailable()
        {
            Assert.NotNull(
                await _sut.GetDealPipelinesAsync());
        }
    }
}
