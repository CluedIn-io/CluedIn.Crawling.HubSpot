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

        private readonly Settings _settings;

        public LineItemsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings) : base(client, jobData)
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
                var properties = Client.GetLineItemPropertiesAsync(_settings).Result;

                while (true)
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
                                var dealAssociations = new AssociationsIterator(Client, JobData, lineItem.ObjectId.Value, AssociationType.LineItemToDeal).Iterate(limit).Cast<long>();
                                lineItem.Associations.AddRange(dealAssociations);
                            }
                            catch
                            {
                                // ignored
                            }
                        }

                        result.Add(lineItem);
                    }

                    if (response.HasMore == false || response.Objects.Count < limit || response.Offset == null)
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
