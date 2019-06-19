using System;
using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public abstract class HubSpotIteratorBase : IHubSpotIterator
    {
        protected readonly IHubSpotClient Client;
        protected readonly HubSpotCrawlJobData JobData;

        public HubSpotIteratorBase(IHubSpotClient client, HubSpotCrawlJobData jobData)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            JobData = jobData ?? throw new ArgumentNullException(nameof(jobData));
        }

        public abstract IEnumerable<object> Iterate(int? limit = null);
    }
}
