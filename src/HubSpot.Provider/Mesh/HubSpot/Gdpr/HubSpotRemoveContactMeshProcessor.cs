using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Gdpr
{
    public class HubSpotRemoveContactMeshProcessor : HubSpotRemoveBaseMeshProcessor
    {
        public HubSpotRemoveContactMeshProcessor(ApplicationContext appContext)
            : base(appContext, "contacts/v1/contact/vid/", CluedIn.Core.Data.EntityType.Infrastructure.Contact, CluedIn.Core.Data.EntityType.Person)
        {
        }
    }
}
