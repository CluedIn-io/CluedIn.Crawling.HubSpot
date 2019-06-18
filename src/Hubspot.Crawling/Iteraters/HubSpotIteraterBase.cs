using System;
using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public abstract class HubSpotIteraterBase : IHubSpotIterater
    {
        protected readonly IHubSpotClient Client;
        protected readonly HubSpotCrawlJobData JobData;

        public HubSpotIteraterBase(IHubSpotClient client, HubSpotCrawlJobData jobData)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            JobData = jobData ?? throw new ArgumentNullException(nameof(jobData));
        }

        public abstract IEnumerable<object> Iterate();
    }
}