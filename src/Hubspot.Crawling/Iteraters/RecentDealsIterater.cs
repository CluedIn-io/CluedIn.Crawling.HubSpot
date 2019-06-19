using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class RecentDealsIterater : HubSpotIteraterBase
    {
        public RecentDealsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 20;

            while (true)
            {
                var response = Client.GetRecentDealsAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                foreach (var obj in response.deals)
                {
                    yield return obj;
                }

                if (response.deals.Count < limit)
                    break;

                offset = response.offset;
            }
        }
    }
}
