using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class RecentDealsIterator : HubSpotIteratorBase
    {
        public RecentDealsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 20;

            while (true)
            {
                var response = Client.GetRecentDealsAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                if (response?.results == null || !response.results.Any())
                    break;

                foreach (var obj in response.results)
                {
                    yield return obj;
                }

                if (response.results.Count < limit)
                    break;

                offset = response.offset;
            }
        }
    }
}
