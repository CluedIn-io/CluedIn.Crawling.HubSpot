using CluedIn.Core;
using RestSharp;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot
{
    public class HubSpotContactMeshProcessor : HubSpotUpdateBaseMeshProcessor
    {
        public HubSpotContactMeshProcessor(ApplicationContext appContext)
            : base(appContext, "contacts/v1/contact/vid/:id/profile", "hubspot.contact.", Method.POST, CluedIn.Core.Data.EntityType.Infrastructure.Contact, CluedIn.Core.Data.EntityType.Person)
        {
        }

    }
}
