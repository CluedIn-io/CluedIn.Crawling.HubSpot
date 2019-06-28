using CluedIn.Core;
using CluedIn.Crawling.HubSpot.Core;

using ComponentHost;

namespace CluedIn.Crawling.HubSpot
{
    [Component(HubSpotConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class CrawlerComponent : CrawlerComponentBase
    {
        public CrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

