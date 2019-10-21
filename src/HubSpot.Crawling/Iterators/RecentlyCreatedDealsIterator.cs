using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class RecentlyCreatedDealsIterator : HubSpotIteratorBase
    {
        public RecentlyCreatedDealsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, ILogger logger)
            : base(client, jobData, logger)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            limit = limit ?? 12000;

            var canContinue = true;

            while (canContinue)
            {
                var result = new List<object>();
                try
                {
                    var response = Client.GetRecentlyCreatedDealsAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                    if (response?.results == null || !response.results.Any())
                        canContinue = false;
                    else
                    {

                        result.AddRange(response.results);

                        if (response.results.Count < limit)
                            canContinue = false;
                        else
                        {
                            offset = response.offset;
                            retries = 0;
                        }
                    }
                }
                catch (ThrottlingException e)
                {
                    if (!ShouldRetryThrottledCall(e, retries))
                    {
                        break;
                    }

                    retries++;
                }
                catch
                {
                    Logger.Warn(() => $"Failed to retrieve data in {GetType().FullName}");
                    break;
                }

                foreach (var item in result)
                {
                    yield return item;
                }
            }

        }
    }
}
