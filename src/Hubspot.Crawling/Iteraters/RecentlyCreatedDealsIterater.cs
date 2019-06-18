using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class RecentlyCreatedDealsIterater : HubSpotIteraterBase
    {
        public RecentlyCreatedDealsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = Client.GetRecentlyCreatedDealsAsync(JobData.LastCrawlFinishTime, limit, offset).Result;

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
