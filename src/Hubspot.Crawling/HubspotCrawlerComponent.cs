using CluedIn.Core;
using CluedIn.Crawling.Hubspot.Core;

using ComponentHost;

namespace CluedIn.Crawling.Hubspot
{
    [Component(HubspotConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class HubspotCrawlerComponent : CrawlerComponentBase
    {
        public HubspotCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

