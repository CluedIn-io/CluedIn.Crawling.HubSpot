using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class BroadcastMessagesIterater : HubSpotIteraterBase
    {
        public BroadcastMessagesIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit)
        {
            int offset = 0;
            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetBroadcastMessagesAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                foreach (var obj in response.deals)
                {
                    yield return obj;
                }

                if (response.deals.Count < limit)
                    break;

                offset += limit.Value;
            }
        }
    }
}
