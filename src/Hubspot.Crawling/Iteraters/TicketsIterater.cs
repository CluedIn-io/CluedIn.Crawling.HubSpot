using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class TicketsIterater : HubSpotIteraterBase
    {

        private readonly IList<string> _properties;

        public TicketsIterater(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate(int? limit)
        {
            int offset = 0;
            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetTicketsAsync(_properties, limit.Value, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var ticket in response.Objects)
                {
                    if (ticket.ObjectId.HasValue)
                    {
                        foreach (var dealAssociation in new DealAssociationsIterater(Client, JobData, ticket.ObjectId.Value).Iterate(null)) // Null will use the default value within the iterator itself
                        {
                            yield return dealAssociation;
                        }
                    }

                    yield return ticket;
                }

                if (response.HasMore == false || response.Objects.Count < limit || response.Offset == null)
                    break;

                offset = response.Offset.Value;
            }
        }

        
    }
}
