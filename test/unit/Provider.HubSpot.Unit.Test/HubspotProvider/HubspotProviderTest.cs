using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Accounts;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using Crawling.HubSpot.Test.Common;
using Moq;
using RestSharp;
using Xunit;
using Owner = java.security.acl.Owner;
using Task = System.Threading.Tasks.Task;

namespace Provider.HubSpot.Unit.Test.HubSpotProvider
{
    public abstract class HubSpotProviderTest
    {
        protected readonly ProviderBase Sut;
        protected readonly Mock<IHubSpotClientFactory> NameClientFactory;
        protected readonly ApplicationContext ApplicationContext;
        protected readonly Mock<IWindsorContainer> Container;
        protected readonly Mock<SystemContext> SystemContext;
        protected readonly Mock<ILogger> Logger;
        protected readonly Guid OrganizationId = Guid.NewGuid();
        protected readonly Dictionary<string, object> Configuration;
        protected readonly HubSpotCrawlJobData CrawlJobData;
        protected readonly Mock<HubSpotClient> Client;

        protected HubSpotProviderTest()
        {
            Container = new Mock<IWindsorContainer>();
            SystemContext = new Mock<SystemContext>(Container.Object);
            NameClientFactory = new Mock<IHubSpotClientFactory>();
            ApplicationContext = new ApplicationContext(Container.Object);
            Logger = new Mock<ILogger>();
            Configuration = HubSpotConfiguration.Create();
            CrawlJobData = new HubSpotCrawlJobData(Configuration);
            Client = new Mock<HubSpotClient>(Logger.Object, CrawlJobData, new RestClient());
            Sut = new CluedIn.Provider.HubSpot.HubSpotProvider(ApplicationContext, NameClientFactory.Object, Logger.Object, null);

            NameClientFactory.Setup(n => n.CreateNew(It.IsAny<HubSpotCrawlJobData>())).Returns(() => Client.Object);
        }

        public class TestAuthenticationTests : HubSpotProviderTest
        {
            [Fact]
            public async Task TestAuthenticationLogsExceptionAndReturnsFalseWhenFails()
            {
                Client.Setup(n => n.GetContactsFromAllListsAsync(It.IsAny<IList<string>>(), 1, 0)).Throws(new Exception());

                await Sut.TestAuthentication(null, Configuration, OrganizationId, Guid.Empty, Guid.Empty);

                Logger.Verify(n => n.Warn(It.IsAny<Func<string>>(), It.IsAny<Exception>()), Times.Once);
            }

            [Fact]
            public async Task TestAuthenticationReturnsTrueIfHasResult()
            {
                Client.Setup(n => n.GetContactsFromAllListsAsync(It.IsAny<IList<string>>(), 1, 0)).ReturnsAsync(new ContactResponse());

                var result = await Sut.TestAuthentication(null, Configuration, OrganizationId, Guid.Empty, Guid.Empty);

                Assert.True(result);
            }
        }

        public class GetHelperConfigurationTests : HubSpotProviderTest
        {
            [Fact]
            public async Task GetHelperConfigurationReturnsWebHooks()
            {
                var result = await Sut.GetHelperConfiguration(null, CrawlJobData, OrganizationId, Guid.Empty, Guid.Empty);

                Assert.NotNull(result);
                Assert.NotEmpty(result);
                Assert.Contains(result, n => n.Key == "webhooks");
                Assert.NotEmpty(result["webhooks"] as List<object>);
            }
        }

        public class GetAccountInformationTests : HubSpotProviderTest
        {
            [Theory(Skip = "Requires HubspotClient.GetAccountInformation to be virtual as we are mocking concrete class NOT the interface"), AutoData]
            public async Task GetAccountInformationReturnsAccountInformationWithPortalId(int portalId)
            {
                Client.Setup(n => n.GetAccountInformation()).ReturnsAsync(new List<OwnerResponse> {new OwnerResponse {portalId = portalId}});
                var result = await Sut.GetAccountInformation(null, CrawlJobData, OrganizationId, Guid.Empty, Guid.Empty);

                Assert.NotNull(result);
                Assert.Equal(portalId.ToString(), result.AccountId);
                Assert.Equal(portalId.ToString(), result.AccountDisplay);
            }

            [Fact(Skip = "Requires HubspotClient.GetAccountInformation to be virtual as we are mocking concrete class NOT the interface")]
            public async Task GetAccountInformationHandlesNullResult()
            {
                Client.Setup(n => n.GetAccountInformation()).ReturnsAsync(default(List<OwnerResponse>));
                var result = await Sut.GetAccountInformation(null, CrawlJobData, OrganizationId, Guid.Empty, Guid.Empty);

                Assert.NotNull(result);
                Assert.Equal(string.Empty, result.AccountId);
                Assert.Equal(string.Empty, result.AccountDisplay);
                Assert.NotEmpty(result.Errors);
            }

            [Fact(Skip = "Requires HubspotClient.GetAccountInformation to be virtual as we are mocking concrete class NOT the interface")]
            public async Task GetAccountInformationHandlesException()
            {
                Client.Setup(n => n.GetAccountInformation()).Throws(new Exception());
                var result = await Sut.GetAccountInformation(null, CrawlJobData, OrganizationId, Guid.Empty, Guid.Empty);

                Assert.NotNull(result);
                Assert.Equal(string.Empty, result.AccountId);
                Assert.Equal(string.Empty, result.AccountDisplay);
                Assert.NotEmpty(result.Errors);
            }
        }
    }
}
