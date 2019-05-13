using Castle.MicroKernel.Registration;

using CluedIn.Core;
using CluedIn.Core.Providers;
// 
using CluedIn.Core.Webhooks;
// 
using CluedIn.Crawling.Hubspot.Core;
using CluedIn.Crawling.Hubspot.Infrastructure.Installers;
// 
using CluedIn.Server;
using ComponentHost;

namespace CluedIn.Provider.Hubspot
{
    [Component(HubspotConstants.ProviderName, "Providers", ComponentType.Service, ServerComponents.ProviderWebApi, Components.Server, Components.DataStores, Isolation = ComponentIsolation.NotIsolated)]
    public sealed class HubspotProviderComponent : ServiceApplicationComponent<EmbeddedServer>
    {
        /**********************************************************************************************************
         * CONSTRUCTOR
         **********************************************************************************************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="HubspotProviderComponent" /> class.
        /// </summary>
        /// <param name="componentInfo">The component information.</param>
        public HubspotProviderComponent(ComponentInfo componentInfo)
            : base(componentInfo)
        {
            // Dev. Note: Potential for compiler warning here ... CA2214: Do not call overridable methods in constructors
            //   this class has been sealed to prevent the CA2214 waring being raised by the compiler
            Container.Register(Component.For<HubspotProviderComponent>().Instance(this));  
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Starts this instance.</summary>
        public override void Start()
        {
            Container.Install(new InstallComponents());

            Container.Register(Types.FromThisAssembly().BasedOn<IProvider>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromThisAssembly().BasedOn<IEntityActionBuilder>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());

            Container.Register(Types.FromThisAssembly().BasedOn<IWebhookProcessor>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromThisAssembly().BasedOn<IWebhookPrevalidator>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());



            State = ServiceState.Started;
        }

        /// <summary>Stops this instance.</summary>
        public override void Stop()
        {
            if (State == ServiceState.Stopped)
                return;

            State = ServiceState.Stopped;
        }
    }
}
