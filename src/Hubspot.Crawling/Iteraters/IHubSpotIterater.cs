using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public interface IHubSpotIterater
    {
        IEnumerable<object> Iterate(int? limit = null);
    }
}
