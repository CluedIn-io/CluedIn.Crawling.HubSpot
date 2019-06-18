using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class ProductsIterater : HubSpotIteraterBase
    {

        private readonly IList<string> _properties;

        public ProductsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = Client.GetProductsAsync(_properties, limit, offset).Result;

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
