using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class EngagementsIterator : HubSpotIteratorBase
    {
        public EngagementsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, ILogger logger)
            : base(client, jobData, logger)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            long offset = 0;
            var retries = 0;
            limit = limit ?? 20;

            var canContinue = true;

            while (canContinue)
            {

                var result = new List<object>();
                try
                {
                    var response = Client.GetEngagementsAsync(limit.Value, offset).Result;

                    if (response?.results == null || !response.results.Any())
                        canContinue = false;
                    else
                    {
                        result.AddRange(response.results);

                        if (response.results.Count < limit || response.offset == null)
                            canContinue = false;
                        else
                        {
                            offset = response.offset.Value;
                            retries = 0;
                        }
                    }
                }
                catch (ThrottlingException e)
                {
                    if (!ShouldRetryThrottledCall(e, retries))
                    {
                        canContinue = false;
                    }

                    retries++;
                }
                catch
                {
                    Logger.LogWarning("Failed to retrieve data in {type}", GetType().FullName);
                    canContinue = false;
                }

                foreach (var item in result)
                {
                    yield return item;
                }
            }

        }
    }
}
