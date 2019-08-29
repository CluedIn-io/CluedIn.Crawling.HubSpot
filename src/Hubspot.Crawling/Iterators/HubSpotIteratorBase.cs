using System;
using System.Collections.Generic;
using System.Threading;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public abstract class HubSpotIteratorBase : IHubSpotIterator
    {
        protected HubSpotIteratorBase(IHubSpotClient client, HubSpotCrawlJobData jobData)
        {
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
                return false;
            }

            if (e.RateLimitRemaining > 0)
            {
                return true;
            }

            if (retries < MaxRetries)
            {
                Thread.Sleep(e.RateLimitIntervalMilliseconds);
                return true;
            }

            return false;

        }
    }
}
