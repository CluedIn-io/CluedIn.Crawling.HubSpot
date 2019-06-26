using System;
using System.Collections.Generic;
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
            Sut = new CluedIn.Provider.HubSpot.HubSpotProvider(ApplicationContext, NameClientFactory.Object, Logger.Object, new Mock<SystemNotifications>(SystemContext.Object).Object);

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
    }

//    public class MockSystemContext : SystemContext
//    {
//        private readonly Mock<IWindsorContainer> _container;
//        private ISystemNotifications _notifications;

//        public MockSystemContext(Mock<IWindsorContainer> container) : base(container.Object)
//        {
//            _container = container;
//            _notifications = new Mock<SystemNotifications>(_container.Object).Object;
//        }

//#pragma warning disable 108,114
//        public virtual ISystemNotifications Notifications
//        {
//            get => _notifications;
//            set => _notifications = value;
//        }
//#pragma warning restore 108,114
//    }
}
