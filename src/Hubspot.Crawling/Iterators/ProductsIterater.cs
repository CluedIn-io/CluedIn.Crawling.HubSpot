using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class ProductsIterator : HubSpotIteratorBase
    {

        private readonly IList<string> _properties;

        public ProductsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetProductsAsync(_properties, limit.Value, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var obj in response.Objects)
                {
                    yield return obj;
                }
                

                if (response.Objects.Count < limit || response.Offset == null)
                    break;

                offset = response.Offset.Value;
            }
        }
    }
}
