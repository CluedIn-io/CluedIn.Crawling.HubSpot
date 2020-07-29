using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public abstract class HubSpotIteratorBase : IHubSpotIterator
    {
        protected readonly ILogger Logger;

        protected HubSpotIteratorBase(IHubSpotClient client, HubSpotCrawlJobData jobData, ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Client = client ?? throw new ArgumentNullException(nameof(client));
            JobData = jobData ?? throw new ArgumentNullException(nameof(jobData));
        }

        public abstract IEnumerable<object> Iterate(int? limit = null);

        public IHubSpotClient Client { get; }

        public HubSpotCrawlJobData JobData { get; }

        public int MaxRetries { get; set; } = 3;


        protected bool ShouldRetryThrottledCall(ThrottlingException e, int retries)
        {
            if (e.DailyRemaining <= 0)
            {
                Logger.LogWarning("Exceeded daily quota");
                return false;
            }

            if (e.RateLimitRemaining > 0)
            {
                Logger.LogTrace("Retrying request, {remainingAttempts} remaining attempts", e.RateLimitRemaining);

                return true;
            }

            if (retries < MaxRetries)
            {
                Logger.LogTrace("Retrying request in {millisecondsInterval} milliseconds", e.RateLimitIntervalMilliseconds);

                Thread.Sleep(e.RateLimitIntervalMilliseconds);
                return true;
            }

            Logger.LogWarning("Failed request after {MaxRetries} retries", MaxRetries);

            return false;
        }

        protected IEnumerable<object> CreateEmptyResults()
        {
            Logger.LogWarning("Failed to retrieve data in {type}", GetType().FullName);
            return Enumerable.Empty<object>();
        }
    }
}
