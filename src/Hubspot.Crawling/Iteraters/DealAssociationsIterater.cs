using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class DealAssociationsIterater : HubSpotIteraterBase
    {
        private readonly int _objectId;

        public DealAssociationsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData, int objectId) : base(client, jobData)
        {
            _objectId = objectId;
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = Client.GetDealAssociationsAsync(_objectId, limit, offset).Result;

                if (response?.Results == null || !response.Results.Any())
                    break;

                foreach (var result in response.Results)
                {
                    yield return result;
                }

                if (response.HasMore == false || response.Results.Count < limit)
                    break;

                offset = response.Offset;
            }
        }
    }
}