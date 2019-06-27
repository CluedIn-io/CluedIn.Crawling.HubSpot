using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class RecentlyCreatedDealsIterator : HubSpotIteratorBase
    {
        public RecentlyCreatedDealsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 12000;

            var result = new List<object>();
            try
            {
                while (true)
                {
                    var response = Client.GetRecentlyCreatedDealsAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                    if (response?.results == null || !response.results.Any())
                        break;

                    result.AddRange(response.results);

                    if (response.results.Count < limit)
                        break;

                    offset = response.offset;
                }
            }
            catch
            {
                return Enumerable.Empty<object>();
            }

            return result;
        }
    }
}
