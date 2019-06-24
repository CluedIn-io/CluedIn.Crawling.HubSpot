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

            var result = new List<object>();
            try
            {
                while (true)
                {
                    var response = Client.GetBroadcastMessagesAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                    if (response == null || !response.Any())
                        break;

                    result.AddRange(response);

                    if (response.Count < limit)
                        break;

                    offset += limit.Value;
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
