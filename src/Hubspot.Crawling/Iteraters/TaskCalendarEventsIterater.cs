using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class TaskCalendarEventsIterater : HubSpotIteraterBase
    {
        public TaskCalendarEventsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = Client.GetTaskCalendarEventsAsync(JobData.LastCrawlFinishTime, DateTimeOffset.UtcNow, limit, offset).Result;

                if (response == null || !response.Any())
                    break;

                foreach (var obj in response)
                {
                    yield return obj;
                }

                if (response.Count < limit)
                    break;

                offset += 100;
            }
        }
    }
}
