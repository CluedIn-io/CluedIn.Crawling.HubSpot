using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Gdpr
{
    public class HubSpotRemoveCompanyMeshProcessor : HubSpotRemoveBaseMeshProcessor
    {
        public HubSpotRemoveCompanyMeshProcessor(ApplicationContext appContext)
            : base(appContext, "companies/v2/companies/", CluedIn.Core.Data.EntityType.Organization)
        {
        }
    }
}
