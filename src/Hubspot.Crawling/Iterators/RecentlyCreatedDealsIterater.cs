﻿using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using ikvm.extensions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class RecentlyCreatedDealsIterator : HubSpotIteratorBase
    {
        public RecentlyCreatedDealsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 12000;

            while (true)
            {
                var response = Client.GetRecentlyCreatedDealsAsync(JobData.LastCrawlFinishTime, limit.Value, offset).Result;

                if (response?.results == null || !response.results.Any())
                    break;

                foreach (var obj in response.results)
                {
                    yield return obj;
                }

                if (response.results.Count < limit)
                    break;

                offset = response.offset;
            }
        }
    }
}