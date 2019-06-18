using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class LineItemsIterater : HubSpotIteraterBase
    {

        private readonly IList<string> _properties;

        public LineItemsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = Client.GetLineItemsAsync(_properties, limit, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var lineItem in response.Objects)
                {
                    if (lineItem.ObjectId.HasValue)
                    {
                        foreach (var dealAssociation in new DealAssociationsIterater(Client, JobData, lineItem.ObjectId.Value).Iterate())
                        {
                            yield return dealAssociation;
                        }
                    }

                    yield return lineItem;
                }

                if (response.HasMore == false || response.Objects.Count < limit || response.Offset == null)
                    break;

                offset = response.Offset.Value;
            }
        }

        
    }
}