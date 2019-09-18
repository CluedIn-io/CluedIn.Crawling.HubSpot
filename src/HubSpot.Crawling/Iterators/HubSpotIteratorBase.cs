using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

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
                Logger.Warn(() => "Exceeded daily quota");
                return false;
            }

            if (e.RateLimitRemaining > 0)
            {
                Logger.Verbose(() => $"Retrying request, {e.RateLimitRemaining} remaining attempts");

                return true;
            }

            if (retries < MaxRetries)
            {
                Logger.Verbose(() => $"Retrying request in {e.RateLimitIntervalMilliseconds} milliseconds");

                Thread.Sleep(e.RateLimitIntervalMilliseconds);
                return true;
            }

            Logger.Warn(() => $"Failed request after {MaxRetries} retries");

            return false;
        }

        protected IEnumerable<object> CreateEmptyResults()
        {
            Logger.Warn(() => $"Failed to retrieve data in {GetType().FullName}");
            return Enumerable.Empty<object>();
        }
    }
}
