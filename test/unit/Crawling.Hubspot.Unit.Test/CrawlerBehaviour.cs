using System;
using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using Crawling.HubSpot.Unit.Test.Stubs;
using Moq;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Crawling.HubSpot.Unit.Test
{
    public class CrawlerBehaviour
    {
        public class ConstructorTests
        {
            [Fact]
            public void ConstructorRequiresClientFactoryParameter()
            {
                Assert.Throws<ArgumentNullException>(() =>
                    new CluedIn.Crawling.HubSpot.Crawler(default(IHubSpotClientFactory)));
            }
        }

        public class GetDataTests
        {
            private readonly ICrawlerDataGenerator _sut;
            private readonly HubSpotCrawlJobData _crawlJobData;
            private readonly Mock<IHubSpotClient> _clientMock;

            public GetDataTests()
            {
                var nameClientFactory = new Mock<IHubSpotClientFactory>();

                _clientMock = new Mock<IHubSpotClient>();

                _clientMock
                    .Setup(x => x.GetSettingsAsync())
                    .Returns(Task.FromResult(new Settings()));

                nameClientFactory.Setup(x => x.CreateNew(It.IsAny<HubSpotCrawlJobData>())).Returns(_clientMock.Object);

                _sut = new CluedIn.Crawling.HubSpot.Crawler(nameClientFactory.Object);

                _crawlJobData = new HubSpotCrawlJobData(HubSpotConfiguration.Create());
            }

            [Fact]
            public void RequiresCrawlJobDataParameter()
            {
                Assert.Throws<ArgumentNullException>(() =>
                    _sut.GetData(default(CrawlJobData)));
            }

            [Theory]
            [InlineData(typeof(CrawlJobData))]
            [InlineData(typeof(SomeOtherCrawlJobData))]
            public void ReturnsEmptyDataForNonHubSpotCrawlJobData(Type jobDataType)
            {
                var instance = Activator.CreateInstance(jobDataType);

                Assert.Empty(
                    _sut.GetData((CrawlJobData)instance));
            }

            [Fact(Skip = "System.ArgumentNullException ... calls iterator and fails")]
            public void ReturnsEmptyDataWhenSettingsAreNotAvailable()
            {
                _clientMock
                    .Setup(x => x.GetSettingsAsync())
                    .Returns(Task.FromResult(default(Settings)));

                Assert.Empty(
                    _sut.GetData(_crawlJobData));
            }

            [Fact(Skip = "System.ArgumentNullException ... calls iterator and fails")]
            public void ReturnsData()
            {
                Assert.NotEmpty(
                    _sut.GetData(_crawlJobData));
            }
        }
    }
}
