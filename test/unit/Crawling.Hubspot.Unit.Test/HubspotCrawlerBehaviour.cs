using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Hubspot.Unit.Test
{
  public class HubSpotCrawlerBehaviour
  {
    private readonly ICrawlerDataGenerator _sut;

    public HubSpotCrawlerBehaviour()
    {
        var nameClientFactory = new Mock<IHubSpotClientFactory>();

        _sut = new HubSpotCrawler(nameClientFactory.Object);
    }

    [Fact]
    public void GetDataReturnsData()
    {
      var jobData = new CrawlJobData();

      _sut.GetData(jobData)
          .ShouldNotBeNull();
    }
  }
}
