using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Hubspot.Infrastructure.Factories;
using Moq;

namespace Provider.Hubspot.Test.HubspotProvider
{
  public abstract class HubspotProviderTest
  {
    protected readonly ProviderBase Sut;

    protected Mock<IHubspotClientFactory> NameClientFactory;
    protected Mock<IWindsorContainer> Container;

    protected HubspotProviderTest()
    {
      Container = new Mock<IWindsorContainer>();
      NameClientFactory = new Mock<IHubspotClientFactory>();
      var applicationContext = new ApplicationContext(Container.Object);
      Sut = new CluedIn.Provider.Hubspot.HubspotProvider(applicationContext, NameClientFactory.Object);
    }
  }
}
