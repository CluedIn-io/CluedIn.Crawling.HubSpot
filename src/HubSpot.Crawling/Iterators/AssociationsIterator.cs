using System.Collections.Generic;
using System.Linq;

using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class AssociationsIterator : HubSpotIteratorBase
    {
        private readonly int _objectId;
        private readonly AssociationType _associationType;

        public AssociationsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, int objectId, AssociationType associationType, ILogger logger)
            : base(client, jobData, logger)
        {
            _objectId = objectId;
            _associationType = associationType;
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            limit = limit ?? 20;
            var canContinue = true;

            while (canContinue)
            {
                var result = new List<object>();
                try
                {
                    var response = Client.GetAssociationsAsync(_objectId, _associationType, limit.Value, offset).Result;

                    if (response?.Results == null || !response.Results.Any())
                        canContinue = false;
                    else
                    {

                        foreach (var assoc in response.Results)
                        {
                            result.Add(assoc);
                        }

                        if (response.HasMore == false || response.Results.Count < limit)
                            canContinue = false;

                        offset = response.Offset;
                        retries = 0;
                    }
                }
                catch (ThrottlingException e)
                {
                    if (!ShouldRetryThrottledCall(e, retries))
                    {
                        canContinue = false;
                    }

                    retries++;
                }
                catch
                {
                    Logger.Warn(() => $"Failed to retrieve data in {GetType().FullName}");
                    canContinue = false;
                }

                foreach (var item in result)
                {
                    yield return item;
                }
            }

        }
    }
}
