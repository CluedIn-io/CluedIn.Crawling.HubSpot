using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class LineItemsIterator : HubSpotIteratorBase
    {

        private readonly IList<string> _properties;

        public LineItemsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetLineItemsAsync(_properties, limit.Value, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var lineItem in response.Objects)
                {
                    if (lineItem.ObjectId.HasValue)
                    {
                        var dealAssociations = new AssociationsIterator(Client, JobData, lineItem.ObjectId.Value, AssociationType.LineItemToDeal).Iterate(limit).Cast<long>();
                        lineItem.Associations.AddRange(dealAssociations);
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
