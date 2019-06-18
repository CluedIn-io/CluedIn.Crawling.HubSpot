using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class StaticContactListIterater : HubSpotIteraterBase
    {
        public StaticContactListIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = Client.GetStaticContactListsAsync(limit, offset).Result;

                if (response?.lists == null || !response.lists.Any())
                    break;

                foreach (var list in response.lists)
                {
                    yield return list;
                }

                if (response.hasMore == false || response.lists.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }
    }
}