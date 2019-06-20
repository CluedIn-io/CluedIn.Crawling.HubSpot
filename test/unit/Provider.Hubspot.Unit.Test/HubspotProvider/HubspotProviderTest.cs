using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using Moq;

namespace Provider.Hubspot.Unit.Test.HubspotProvider
{
  public abstract class HubSpotProviderTest
  {
    protected readonly ProviderBase Sut;

    protected Mock<IHubSpotClientFactory> NameClientFactory;
    protected Mock<IWindsorContainer> Container;

    protected HubSpotProviderTest()
    {
      Container = new Mock<IWindsorContainer>();
      NameClientFactory = new Mock<IHubSpotClientFactory>();
      var applicationContext = new ApplicationContext(Container.Object);
      Sut = new CluedIn.Provider.HubSpot.HubSpotProvider(applicationContext, NameClientFactory.Object);
    }
  }
}
