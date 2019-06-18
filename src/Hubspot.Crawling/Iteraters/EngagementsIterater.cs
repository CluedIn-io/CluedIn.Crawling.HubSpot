using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class EngagementsIterater : HubSpotIteraterBase
    {
        public EngagementsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = Client.GetEngagementsAsync(limit, offset).Result;

                if (response?.results == null || !response.results.Any())
                    break;

                foreach (var obj in response.results)
                {
                    yield return obj;
                }

                if (response.results.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }
    }
}
