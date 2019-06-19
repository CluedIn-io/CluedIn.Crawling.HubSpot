using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class BroadcastMessagesIterator : HubSpotIteratorBase
    {
        public BroadcastMessagesIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetBroadcastMessagesAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                if (response == null || !response.Any())
                    break;

                foreach (var obj in response)
                {
                    yield return obj;
                }

                if (response.Count < limit)
                    break;

                offset += limit.Value;
            }
        }
    }
}
