using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class AssociationsIterator : HubSpotIteratorBase
    {
        private readonly int _objectId;
        private readonly AssociationType _associationType;

        public AssociationsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, int objectId, AssociationType associationType) : base(client, jobData)
        {
            _objectId = objectId;
            _associationType = associationType;
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;

            limit = limit ?? 20;

            while (true)
            {
                var response = Client.GetAssociationsAsync(_objectId,  _associationType, limit.Value, offset).Result;

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
