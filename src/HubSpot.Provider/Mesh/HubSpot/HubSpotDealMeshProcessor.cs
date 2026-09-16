using CluedIn.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using RestSharp;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot
{
    public class HubSpotDealMeshProcessor : HubSpotUpdateBaseMeshProcessor
    {
        public HubSpotDealMeshProcessor(ApplicationContext appContext)
            : base(appContext, "/deals/v1/deal/:id", "hubspot.deal.", HubSpotRestMethod.Put, CluedIn.Core.Data.EntityType.Sales.Deal)
        {
        }
    }
}
