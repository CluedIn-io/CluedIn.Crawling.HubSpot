using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;

using CluedIn.Core;
using CluedIn.Crawling.Hubspot.Infrastructure.Factories;
using RestSharp;

namespace CluedIn.Crawling.Hubspot.Infrastructure.Installers
{
  public class InstallComponents : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container
          .AddFacilityIfNotExists<TypedFactoryFacility>()
          .Register(Component.For<IHubspotClientFactory>().AsFactory())
          .Register(Component.For<HubspotClient>().LifestyleTransient());

      if (!container.Kernel.HasComponent(typeof(IRestClient)) && !container.Kernel.HasComponent(typeof(RestClient)))
        container.Register(Component.For<IRestClient, RestClient>());
    }
  }
}
