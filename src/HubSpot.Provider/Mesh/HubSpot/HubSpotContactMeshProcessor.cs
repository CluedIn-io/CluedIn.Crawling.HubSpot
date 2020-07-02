using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot
{
    public class HubSpotContactMeshProcessor : HubSpotUpdateBaseMeshProcessor
    {
        public HubSpotContactMeshProcessor(ApplicationContext appContext)
            : base(appContext, "contacts/v1/contact/vid/:vid/profile", CluedIn.Core.Data.EntityType.Infrastructure.Contact, CluedIn.Core.Data.EntityType.Person)
        {
        }

    }
}
