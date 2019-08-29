using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class TicketsIterator : HubSpotIteratorBase
    {

        private readonly Settings _settings;

        public TicketsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings properties) : base(client, jobData)
        {
            _settings = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            limit = limit ?? 100;

            var result = new List<object>();
            try
            {
                var properties = Client.GetTicketPropertiesAsync(_settings).Result;
                while (true)
                {

                    try
                    {
                        var response = Client.GetTicketsAsync(properties, limit.Value, offset).Result;

                        if (response?.Objects == null || !response.Objects.Any())
                            break;

                        foreach (var ticket in response.Objects)
                        {
                            if (ticket.ObjectId.HasValue)
                            {
                                try
                                {
                                    var associations = new AssociationsIterator(Client, JobData, ticket.ObjectId.Value, AssociationType.TicketToContact).Iterate(100).Cast<long>();
                                    ticket.Associations.Contacts.AddRange(associations);
                                }
                                catch
                                {
                                    // ignored
                                }

                                try
                                {
                                    var associations = new AssociationsIterator(Client, JobData, ticket.ObjectId.Value, AssociationType.TicketToEngagement).Iterate(100).Cast<long>();
                                    ticket.Associations.Engagements.AddRange(associations);
                                }
                                catch
                                {
                                    // ignored
                                }

                                try
                                {
                                    var associations = new AssociationsIterator(Client, JobData, ticket.ObjectId.Value, AssociationType.TicketToCompany).Iterate(100).Cast<long>();
                                    ticket.Associations.Companies.AddRange(associations);
                                }
                                catch
                                {
                                    // ignored
                                }
                            }

                            result.Add(ticket);
                        }

                        if (response.HasMore == false || response.Objects.Count < limit || response.Offset == null)
                            break;

                        offset = response.Offset.Value;
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
