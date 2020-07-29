using System;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CluedIn.Crawling.HubSpot.Iterators;

namespace CluedIn.Crawling.HubSpot.Installers
{
    public class InstallComponents : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (store == null) throw new ArgumentNullException(nameof(store));

            container
                .Install(
                    new Infrastructure.Installers.InstallComponents()
                )

                .Register(
                    Component.For<IHubSpotIterator>()
                        .ImplementedBy<AssociationsIterator>()
                        .OnlyNewServices())

                ;
        }
    }
}
