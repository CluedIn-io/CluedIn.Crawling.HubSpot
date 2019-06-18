using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class FilesIterater : HubSpotIteraterBase
    {
        public FilesIterater(IHubSpotClient client, HubSpotCrawlJobData jobData) : base(client, jobData)
        {
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = Client.GetFilesAsync(JobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }
    }
}
