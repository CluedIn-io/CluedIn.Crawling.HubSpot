using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class SiteMapsIterator : HubSpotIteratorBase
    {
        public SiteMapsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 20;

            var result = new List<object>();
            try
            {
                while (true)
                {
                    var response = Client.GetSiteMapsAsync(limit.Value, offset).Result;

                    if (response?.objects == null || !response.objects.Any())
                        break;

                    result.AddRange(response.objects);

                    if (response.objects.Count < limit || response.offset == null)
                        break;

                    offset = response.offset.Value;
                }
            }
            catch
            {
                return Enumerable.Empty<object>();
            }

            return result;
        }
    }
}
