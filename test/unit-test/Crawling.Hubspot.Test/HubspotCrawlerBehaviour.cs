using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.Hubspot;
using CluedIn.Crawling.Hubspot.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.Hubspot.Test
{
  public class HubspotCrawlerBehaviour
  {
    private readonly ICrawlerDataGenerator _sut;

    public HubspotCrawlerBehaviour()
    {
        var nameClientFactory = new Mock<IHubspotClientFactory>();

        _sut = new HubspotCrawler(nameClientFactory.Object);
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
