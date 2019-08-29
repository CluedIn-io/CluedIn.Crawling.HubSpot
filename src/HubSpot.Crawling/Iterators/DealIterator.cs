using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class DealIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public DealIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings) : base(client, jobData)
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
                var properties = Client.GetDealPropertiesAsync(_settings).Result;

                while (true)
                {
                    try
                    {
                        var response = Client.GetDealsAsync(properties, _settings, limit.Value, offset).Result;

                        if (response?.deals == null || !response.deals.Any())
                            break;

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
                                catch
                                {
                                    // ignored
                                }
                            }

                            result.Add(deal);
                        }

                        if (response.hasMore == false || response.deals.Count < limit)
                            break;

                        offset = response.offset;
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
                return Enumerable.Empty<object>();
            }

            return result;
        }
    }
}
