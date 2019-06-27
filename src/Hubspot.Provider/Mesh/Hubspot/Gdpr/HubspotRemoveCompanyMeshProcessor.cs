using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.Hubspot.Gdpr
{
    public class HubspotRemoveCompanyMeshProcessor : HubspotRemoveBaseMeshProcessor
    {
        public HubspotRemoveCompanyMeshProcessor(ApplicationContext appContext)
            : base(appContext, "companies/v2/companies/", CluedIn.Core.Data.EntityType.Organization)
        {
        }
    }
}
