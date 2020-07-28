using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class ContactIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public ContactIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings, ILogger logger)
            : base(client, jobData, logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            var retries = 0;
            limit = limit ?? 100;
            var canContinue = true;
            var properties = Client.GetContactPropertiesAsync(_settings).Result;

            while (canContinue)
            {
                var result = new List<object>();
                try
                {
                    var response = Client.GetContactsFromAllListsAsync(properties, limit.Value, offset).Result;

                    if (response?.contacts == null || !response.contacts.Any())
                        canContinue = false;
                    else
                    {

                        foreach (var contact in response.contacts)
                        {
                            if (contact.Vid.HasValue)
                            {
                                try
                                {
                                    result.AddRange(Client.GetEngagementByIdAndTypeAsync(contact.Vid.Value, "CONTACT").Result);
                                }
                                catch (Exception exception)
                                {
                                    Logger.LogWarning(exception, "Failed to get Engagements for Contact {vid}", contact.Vid.Value);
                                }
                            }

                            result.Add(contact);
                        }

                        if (response.hasMore == false || response.contacts.Count < limit || response.vidOffset == null)
                            canContinue = false;
                        else
                        {
                            offset = response.vidOffset.Value;
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
                    Logger.LogWarning("Failed to retrieve data in {type}", GetType().FullName);
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
