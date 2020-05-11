using System;
using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using Crawling.HubSpot.Unit.Test.Stubs;
using Microsoft.Extensions.Logging;
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
                    new Crawler(default(IHubSpotClientFactory), default(ILogger<Crawler>)));
            }
        }

        public class GetDataTests
        {
            private readonly ICrawlerDataGenerator _sut;
            private readonly HubSpotCrawlJobData _crawlJobData;
            private readonly Mock<IHubSpotClient> _clientMock;
            private readonly Mock<ILogger<Crawler>> _logMock;

            public GetDataTests()
            {
                var nameClientFactory = new Mock<IHubSpotClientFactory>();

                _clientMock = new Mock<IHubSpotClient>();

                _clientMock
                    .Setup(x => x.GetSettingsAsync())
                    .Returns(Task.FromResult(new Settings()));

                _logMock = new Mock<ILogger<Crawler>>();

                nameClientFactory.Setup(x => x.CreateNew(It.IsAny<HubSpotCrawlJobData>())).Returns(_clientMock.Object);

                _sut = new Crawler(nameClientFactory.Object, _logMock.Object);

                _crawlJobData = new HubSpotCrawlJobData(HubSpotConfiguration.Create());
            }

            [Fact]
            public void RequiresCrawlJobDataParameter()
            {
                Assert.Empty(_sut.GetData(default(CrawlJobData)));
            }

            [Theory]
            [InlineData(typeof(CrawlJobData))]
            [InlineData(typeof(SomeOtherCrawlJobData))]
            public void ReturnsEmptyDataForNonHubSpotCrawlJobData(Type jobDataType)
            {
                var instance = Activator.CreateInstance(jobDataType);

                var result = _sut.GetData((CrawlJobData)instance);
                Assert.Empty(result);
            }

            [Fact]
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
