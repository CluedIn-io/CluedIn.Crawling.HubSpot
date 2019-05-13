using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CluedIn.Core;

namespace CluedIn.Crawling.Hubspot.Installers
{
    // Use this class to add any further dependencies to the container

    public class InstallComponents : IWindsorInstaller
    {
        public void Install([NotNull] IWindsorContainer container, [NotNull] IConfigurationStore store)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (store == null) throw new ArgumentNullException(nameof(store));

            // TODO Add further dependencies to the container here ...
        }
    }
}
