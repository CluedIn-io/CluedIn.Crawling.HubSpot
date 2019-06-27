using CluedIn.Core;

namespace CluedIn.Provider.HubSpot.Mesh.Hubspot.Gdpr
{
    public class HubspotRemoveEngagementMeshProcessor : HubspotRemoveBaseMeshProcessor
    {
        public HubspotRemoveEngagementMeshProcessor(ApplicationContext appContext)
            : base(appContext,  "engagements/v1/engagements/", CluedIn.Core.Data.EntityType.Task, CluedIn.Core.Data.EntityType.Activity)
        {
        }
    }
}



