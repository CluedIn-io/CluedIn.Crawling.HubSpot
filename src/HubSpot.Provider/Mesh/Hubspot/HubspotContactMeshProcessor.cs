using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.Hubspot
{
    public class HubspotContactMeshProcessor : HubspotUpdateBaseMeshProcessor
    {
        public HubspotContactMeshProcessor(ApplicationContext appContext)
            : base(appContext, "contacts/v1/contact/vid/:vid/profile", CluedIn.Core.Data.EntityType.Infrastructure.Contact, CluedIn.Core.Data.EntityType.Person)
        {
        }

    }
}
