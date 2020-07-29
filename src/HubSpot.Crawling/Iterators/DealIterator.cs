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
    public class DealIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public DealIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings, ILogger logger)
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
            var properties = Client.GetDealPropertiesAsync(_settings).Result;

            while (canContinue)
            {
                var result = new List<object>();
                try
                {
                    var response = Client.GetDealsAsync(properties, _settings, limit.Value, offset).Result;

                    if (response?.deals == null || !response.deals.Any())
                        canContinue = false;
                    else
                    {
                        foreach (var deal in response.deals)
                        {

                            if (_settings?.currency != null)
                                deal.Currency = _settings.currency;

                            if (deal.dealId.HasValue)
                            {
                                try
                                {
                                    var engagements = Client.GetEngagementByIdAndTypeAsync(deal.dealId.Value, "DEAL").Result;
                                    result.AddRange(engagements);
                                }
                                catch (Exception exception)
                                {
                                    Logger.LogWarning(exception, "Failed to get Engagements for Deal {dealId}", deal.dealId.Value);
                                }
                            }

                            result.Add(deal);
                        }

                        if (response.hasMore == false || response.deals.Count < limit)
                            canContinue = false;
                        else
                        {
                            offset = response.offset;
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
