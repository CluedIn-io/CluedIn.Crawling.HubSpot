using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public interface IHubSpotIterator
    {
        IEnumerable<object> Iterate(int? limit = null);
    }
}
