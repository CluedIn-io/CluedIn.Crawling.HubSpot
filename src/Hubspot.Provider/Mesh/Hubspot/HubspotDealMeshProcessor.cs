using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.Hubspot
{
    public class HubspotDealMeshProcessor : HubspotUpdateBaseMeshProcessor
    {
        public HubspotDealMeshProcessor(ApplicationContext appContext)
            : base(appContext, "deals/v1/deal/", CluedIn.Core.Data.EntityType.Sales.Deal)
        {
        }
    }
}
