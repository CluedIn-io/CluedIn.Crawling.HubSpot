using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class BroadcastMessagesIterator : HubSpotIteratorBase
    {
        public BroadcastMessagesIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            limit = limit ?? 100;

            var result = new List<object>();
            try
            {
                while (true)
                {
                    try
                    {
                        var response = Client.GetBroadcastMessagesAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                        if (response == null || !response.Any())
                            break;

                        result.AddRange(response);

                        if (response.Count < limit)
                            break;

                        offset += limit.Value;
                        retries = 0;
                    }
                    catch (ThrottlingException e)
                    {
                        if (!ShouldRetryThrottledCall(e, retries))
                        {
                            break;
                        }

                        retries++;
                    }
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
