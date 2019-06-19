using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class TicketsIterator : HubSpotIteratorBase
    {

        private readonly IList<string> _properties;

        public TicketsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
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
                        var associations = new AssociationsIterator(Client, JobData, ticket.ObjectId.Value, AssociationType.TicketToContact).Iterate(100).Cast<long>();
                        ticket.Associations.Contacts.AddRange(associations);

                        associations = new AssociationsIterator(Client, JobData, ticket.ObjectId.Value, AssociationType.TicketToEngagement).Iterate(100).Cast<long>();
                        ticket.Associations.Engagements.AddRange(associations);

                        associations = new AssociationsIterator(Client, JobData, ticket.ObjectId.Value, AssociationType.TicketToCompany).Iterate(100).Cast<long>();
                        ticket.Associations.Companies.AddRange(associations);
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
