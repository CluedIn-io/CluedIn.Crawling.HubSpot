﻿using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class ContactIterator : HubSpotIteratorBase
    {
        private readonly IList<string> _properties;
        public ContactIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties) : base(client, jobData)
        {
            _properties = properties;
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetContactsFromAllListsAsync(_properties, limit.Value, offset).Result;

                if (response?.contacts == null || !response.contacts.Any())
                    break;

                foreach (var contact in response.contacts)
                {

                    if (contact.Vid.HasValue)
                    {
                        foreach (var engagement in Client.GetEngagementByIdAndTypeAsync(contact.Vid.Value, "CONTACT").Result)
                        {
                            yield return engagement;
                        }
                    }

                    yield return contact;
                }

                if (response.hasMore == false || response.contacts.Count < limit || response.vidOffset == null)
                    break;

                offset = response.vidOffset.Value;
            }
        }
    }
}