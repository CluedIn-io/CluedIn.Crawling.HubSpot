using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class PublishingChannelsIterator : HubSpotIteratorBase
    {
        public PublishingChannelsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            try
            {
                return Client.GetPublishingChannelsAsync().Result;
            }
            catch
            {
                return Enumerable.Empty<object>();
            }
        }
    }
}
