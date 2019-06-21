using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;

using CluedIn.Core;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using RestSharp;

namespace CluedIn.Crawling.HubSpot.Infrastructure.Installers
{
    public class InstallComponents : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .AddFacilityIfNotExists<TypedFactoryFacility>()
                .Register(Component.For<IHubSpotClientFactory>().AsFactory())
                .Register(Component.For<IHubSpotImageFetcher, HubSpotImageFetcher>())
                .Register(Component.For<IHubSpotClient, HubSpotClient>().LifestyleTransient());

            if (!container.Kernel.HasComponent(typeof(IRestClient)) && !container.Kernel.HasComponent(typeof(RestClient)))
            {
                container.Register(Component.For<IRestClient, RestClient>());
            }
        }
    }
}
