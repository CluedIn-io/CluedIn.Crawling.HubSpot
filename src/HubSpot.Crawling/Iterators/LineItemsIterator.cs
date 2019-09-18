using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class LineItemsIterator : HubSpotIteratorBase
    {

        private readonly Settings _settings;

        public LineItemsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings, ILogger logger)
            : base(client, jobData, logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            limit = limit ?? 100;

            var result = new List<object>();
            try
            {
                var properties = Client.GetLineItemPropertiesAsync(_settings).Result;

                while (true)
                {
                    try
                    {
                        var response = Client.GetLineItemsAsync(properties, limit.Value, offset).Result;

                        if (response?.Objects == null || !response.Objects.Any())
                            break;

                        foreach (var lineItem in response.Objects)
                        {
                            if (lineItem.ObjectId.HasValue)
                            {
                                try
                                {
                                    var dealAssociations = new AssociationsIterator(Client, JobData, lineItem.ObjectId.Value, AssociationType.LineItemToDeal, Logger).Iterate(limit).Cast<long>();
                                    lineItem.Associations.AddRange(dealAssociations);
                                }
                                catch (Exception exception)
                                {
                                    Logger.Warn(() => $"Failed to get Associations for Line Item {lineItem.ObjectId.Value}", exception);
                                }
                            }

                            result.Add(lineItem);
                        }

                        if (response.HasMore == false || response.Objects.Count < limit || response.Offset == null)
                            break;

                        offset = response.Offset.Value;
                        retries = 0;
                    }
                    catch (ThrottlingException e)
                    {
                        if (!ShouldRetryThrottledCall(e, retries))
                        {
                            break;
                        }

                        retries++;
                    }
                }
            }
            catch
            {
                return CreateEmptyResults();
            }

            return result;
        }


    }
}
