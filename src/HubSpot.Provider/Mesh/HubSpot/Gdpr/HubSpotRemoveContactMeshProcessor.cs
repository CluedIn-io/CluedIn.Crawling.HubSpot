using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.Hubspot.Gdpr
{
    public class HubspotRemoveContactMeshProcessor : HubspotRemoveBaseMeshProcessor
    {
        public HubspotRemoveContactMeshProcessor(ApplicationContext appContext)
            : base(appContext, "contacts/v1/contact/vid/", CluedIn.Core.Data.EntityType.Infrastructure.Contact, CluedIn.Core.Data.EntityType.Person)
        {
        }
    }
}
