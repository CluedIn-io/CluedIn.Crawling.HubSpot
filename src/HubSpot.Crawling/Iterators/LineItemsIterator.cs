using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

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
            var canContinue = true;
            var properties = Client.GetLineItemPropertiesAsync(_settings).Result;

            while (canContinue)
            {

                var result = new List<object>();

                try
                {
                    var response = Client.GetLineItemsAsync(properties, limit.Value, offset).Result;

                    if (response?.Objects == null || !response.Objects.Any())
                        canContinue = false;
                    else
                    {
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
                                    Logger.LogWarning(exception, "Failed to get Associations for Line Item {id}", lineItem.ObjectId.Value);
                                }
                            }

                            result.Add(lineItem);
                        }

                        if (response.HasMore == false || response.Objects.Count < limit || response.Offset == null)
                            canContinue = false;
                        else
                        {
                            offset = response.Offset.Value;
                            retries = 0;
                        }
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
                    Logger.LogWarning("Failed to retrieve data in {type}", GetType().FullName);
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
