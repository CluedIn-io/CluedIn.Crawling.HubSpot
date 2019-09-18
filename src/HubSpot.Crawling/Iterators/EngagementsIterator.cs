using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

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

            var result = new List<object>();
            try
            {
                while (true)
                {
                    try
                    {
                        var response = Client.GetEngagementsAsync(limit.Value, offset).Result;

                        if (response?.results == null || !response.results.Any())
                            break;

                        result.AddRange(response.results);

                        if (response.results.Count < limit || response.offset == null)
                            break;

                        offset = response.offset.Value;
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
                return CreateEmptyResults();
            }

            return result;
        }
    }
}
