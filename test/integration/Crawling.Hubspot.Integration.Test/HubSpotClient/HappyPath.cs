using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using CluedIn.Core;
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
        public async Task CompaniesAreAvailable(List<string> properties)
        {
            Assert.NotEmpty(
                (await _sut.GetCompaniesAsync(properties)).results);
        }

        [Theory]
        [InlineAutoData]
        public async Task ContactPropertiesAreAvailable(Settings settings)
        {
            Assert.NotEmpty(
                await _sut.GetContactPropertiesAsync(settings));
        }

        [Theory]
        [InlineData(12345678, "contact")]
        public async Task EngagementsQueryableByIdAndType(long objectId, string objectType)
        {
            Assert.NotNull(
                await _sut.GetEngagementByIdAndTypeAsync(objectId, objectType));
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
        public async Task ProductsAreAvailable(List<string> properties)
        {
            Assert.NotNull(
                (await _sut.GetProductsAsync(properties)).Objects);
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
        public async Task LineItemsAreAvailable(List<string> properties)
        {
            Assert.NotNull(
                (await _sut.GetLineItemsAsync(properties)).Objects);
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
        public async Task TicketsAreAvailable(List<string> properties)
        {
            Assert.NotEmpty(
                (await _sut.GetTicketsAsync(properties)).Objects);
        }

        [Fact]
        public async Task DynamicContactListsAreAvailable()
        {
            Assert.NotEmpty(
                (await _sut.GetDynamicContactListsAsync()).lists);
        }

        [Fact]
        public async Task StaticContactListsAreAvailable()
        {
            Assert.NotEmpty(
                (await _sut.GetStaticContactListsAsync()).lists);
        }

        [Theory]
        [InlineAutoData]
        public async Task DealAssociationsAreAvailable(int objectId)
        {
            Assert.NotNull(
                (await _sut.GetAssociationsAsync(objectId, AssociationType.LineItemToDeal)).Results);
        }

        [Theory]
        [InlineAutoData]
        public async Task ContactsFromAllListsAreAvailable(List<string> properties)
        {
            Assert.NotEmpty(
                (await _sut.GetContactsFromAllListsAsync(properties)).contacts);
        }

        [Theory]
        [InlineAutoData]
        public async Task DealsAreAvailable(List<string> properties, Settings settings)
        {
            Assert.NotEmpty(
                (await _sut.GetDealsAsync(properties, settings)).deals);
        }

        [Theory]
        [InlineAutoData]
        public async Task FilesAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotNull(
                (await _sut.GetFilesAsync(greaterThanEpoch)).objects);
        }

        [Theory]
        [InlineAutoData]
        public async Task SocialCalendarEventsAreAvailable(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            Assert.NotNull(
                await _sut.GetSocialCalendarEventsAsync(startDate, endDate));
        }

        [Theory]
        [InlineAutoData]
        public async Task TaskCalendarEventsAreAvailable(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            Assert.NotNull(
                await _sut.GetTaskCalendarEventsAsync(startDate, endDate));

        }

        [Theory]
        [InlineAutoData]
        public async Task RecentDealsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotNull(
                (await _sut.GetRecentDealsAsync(greaterThanEpoch)).results);
        }

        [Theory]
        [InlineAutoData]
        public async Task RecentlyCreatedDealsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotNull(
                (await _sut.GetRecentlyCreatedDealsAsync(greaterThanEpoch)).results);
        }

        [Theory]
        [InlineAutoData]
        public async Task BroadcastMessagesOfDealsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotEmpty(
                await _sut.GetBroadcastMessagesAsync(greaterThanEpoch));
        }

        [Theory]
        [InlineAutoData]
        public async Task UrlMappingsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotEmpty(
                (await _sut.GetUrlMappingsAsync(greaterThanEpoch)).objects);
        }

        [Fact]
        public async Task TemplatesAreAvailable()
        {
            Assert.NotEmpty(
                (await _sut.GetTemplatesAsync()).objects);
        }

        [Fact]
        public async Task EngagementsAreAvailable()
        {
            Assert.NotEmpty(
                (await _sut.GetEngagementsAsync()).results);
        }

        [Fact]
        public async Task SiteMapsAreAvailable()
        {
            Assert.NotEmpty(
                (await _sut.GetSiteMapsAsync()).objects);
        }

        [Theory]
        [InlineAutoData]
        public async Task BlogPostsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotEmpty(
                (await _sut.GetBlogPostsAsync(greaterThanEpoch)).objects);
        }

        [Theory]
        [InlineAutoData]
        public async Task BlogTopicsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotEmpty(
                (await _sut.GetBlogTopicsAsync(greaterThanEpoch)).objects);
        }

        [Theory]
        [InlineAutoData]
        public async Task BlogsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotEmpty(
                (await _sut.GetBlogsAsync(greaterThanEpoch)).objects);
        }

        [Theory]
        [InlineAutoData]
        public async Task DomainsAreAvailable(DateTimeOffset greaterThanEpoch)
        {
            Assert.NotEmpty(
                (await _sut.GetDomainsAsync(greaterThanEpoch)).objects);
        }

        [Theory]
        [InlineAutoData]
        public async Task TableRowsAreAvailable(long tableId, Column dateColumn, long portalId)
        {
            Assert.NotEmpty(
                (await _sut.GetTableRowsAsync(DateTimeOffset.MaxValue, tableId, dateColumn, portalId)).Objects);
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
            Assert.NotEmpty(
                await _sut.GetTablesAsync());
        }

        [Fact]
        public async Task WorkFlowsAreAvailable()
        {
            Assert.NotEmpty(
                (await _sut.GetWorkflowsAsync()).workflows);
        }

        [Fact]
        public async Task SmtpTokensAreAvailable()
        {
            Assert.NotEmpty(
                await _sut.GetSmtpTokensAsync());
        }

        [Fact]
        public async Task PublishingChannelsAreAvailable()
        {
            Assert.NotEmpty(
                await _sut.GetPublishingChannelsAsync());
        }

        [Fact]
        public async Task OwnersAreAvailable()
        {
            Assert.NotEmpty(
                await _sut.GetOwnersAsync());
        }

        [Fact(Skip = "https://api.hubapi.com/keywords/v2/keywords?hapikey=demo returns 404 Not Found")]
        public async Task KeywordsAreAvailable()
        {
            Assert.NotEmpty(
                await _sut.GetKeywordsAsync());
        }

        [Fact]
        public async Task DealPipelinesAreAvailable()
        {
            Assert.NotEmpty(
                await _sut.GetDealPipelinesAsync());
        }

        [Theory]
        [InlineAutoData]
        public async Task ContactsByCompanyAreAvailable(long companyId)
        {
            Assert.NotNull(
                (await _sut.GetContactsByCompanyAsync(companyId)).contacts);
        }
    }
}
