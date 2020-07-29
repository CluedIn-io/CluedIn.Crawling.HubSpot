﻿using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class TemplatesIterator : HubSpotIteratorBase
    {
        public TemplatesIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, ILogger logger)
            : base(client, jobData, logger)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            limit = limit ?? 100;

            var canContinue = true;

            while (canContinue)
            {
                var result = new List<object>();
                try
                {
                    var response = Client.GetTemplatesAsync(limit.Value, offset).Result;

                    if (response?.objects == null || !response.objects.Any())
                        canContinue = false;
                    else
                    {
                        result.AddRange(response.objects);

                        if (response.objects.Count < limit || response.offset == null)
                            canContinue = false;
                        else
                        {
                            offset += limit.Value;
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
