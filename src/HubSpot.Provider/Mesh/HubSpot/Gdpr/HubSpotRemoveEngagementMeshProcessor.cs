using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Gdpr
{
    public class HubSpotRemoveEngagementMeshProcessor : HubSpotRemoveBaseMeshProcessor
    {
        public HubSpotRemoveEngagementMeshProcessor(ApplicationContext appContext)
            : base(appContext,  "engagements/v1/engagements/", CluedIn.Core.Data.EntityType.Task, CluedIn.Core.Data.EntityType.Activity)
        {
        }
    }
}



