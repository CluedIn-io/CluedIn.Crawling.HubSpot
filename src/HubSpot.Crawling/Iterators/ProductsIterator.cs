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
    public class ProductsIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public ProductsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings, ILogger logger)
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

            var properties = Client.GetProductPropertiesAsync(_settings).Result;

            while (canContinue)
            {

                var result = new List<object>();
                try
                {
                    var response = Client.GetProductsAsync(properties, limit.Value, offset).Result;

                    if (response?.Objects == null || !response.Objects.Any())
                       canContinue = false;
                    else
                    {
                        result.AddRange(response.Objects);


                        if (response.Objects.Count < limit || response.Offset == null)
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
