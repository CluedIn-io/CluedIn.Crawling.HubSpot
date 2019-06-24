using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class ProductsIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public ProductsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings) : base(client, jobData)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 100;

            var result = new List<object>();
            try
            {
                var properties = Client.GetProductPropertiesAsync(_settings).Result;

                while (true)
                {
                    var response = Client.GetProductsAsync(properties, limit.Value, offset).Result;

                    if (response?.Objects == null || !response.Objects.Any())
                        break;

                    result.AddRange(response.Objects);


                    if (response.Objects.Count < limit || response.Offset == null)
                        break;

                    offset = response.Offset.Value;
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
