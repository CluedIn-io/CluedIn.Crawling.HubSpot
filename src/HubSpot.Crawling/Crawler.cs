using System;
using System.Collections.Generic;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Crawling.HubSpot.Iterators;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot
{
    public class Crawler : ICrawlerDataGenerator
    {
        private readonly IHubSpotClientFactory _clientFactory;
        private readonly ILogger<Crawler> _log;
        
        public Crawler(IHubSpotClientFactory clientFactory, ILogger<Crawler> log)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (jobData == null)
            {
                yield break;
            }

            if (!(jobData is HubSpotCrawlJobData crawlerJobData))
            {
                yield break;
            }
            
            var client = _clientFactory.CreateNew(crawlerJobData);

            var settings = client.GetSettingsAsync().Result;

            if (settings == null)
            {
                _log.LogError("Settings could not be obtained from HubSpot");
                yield break;
            }

            var dailyLimit = client.GetDailyLimitAsync().Result;
            if (dailyLimit.currentUsage >= dailyLimit.usageLimit)
            {
                _log.LogError("HubSpot daily usage limit has been reached");
                yield break;
            }

            foreach (var item in GetCompanies(client, crawlerJobData, settings))
            {
                yield return item;
            }

            foreach (var item in new DealIterator(client, crawlerJobData, settings, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new ContactIterator(client, crawlerJobData, settings, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new DynamicContactListIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new FormsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new OwnersIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new PublishingChannelsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new FilesIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new SiteMapsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new TemplatesIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new UrlMappingsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new EngagementsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new RecentDealsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new RecentlyCreatedDealsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new SmtpTokensIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new SocialCalendarEventsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new StaticContactListIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new TaskCalendarEventsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new WorkflowsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new BlogPostsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new BlogsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new BlogTopicsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new DomainsIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new BroadcastMessagesIterator(client, crawlerJobData, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new ProductsIterator(client, crawlerJobData, settings, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new LineItemsIterator(client, crawlerJobData, settings, _log).Iterate())
            {
                yield return item;
            }

            foreach (var item in new TicketsIterator(client, crawlerJobData, settings, _log).Iterate())
            {
                yield return item;
            }

        }

        private IEnumerable<object> GetCompanies(IHubSpotClient client, HubSpotCrawlJobData crawlerJobData, Settings settings)
        {
            long? portalId = null;
            var companyIterator = new CompanyIterator(client, crawlerJobData, settings, _log);
            foreach (var item in companyIterator.Iterate())
            {
                yield return item;

                if (item is Company company && company.portalId.HasValue)
                {
                    portalId = company.portalId.Value;
                }
            }

            if (portalId != null)
            {
                foreach (var item in companyIterator.GetDealPipelines(portalId.Value))
                {
                    yield return item;
                }


                foreach (var item in companyIterator.GetTables(portalId.Value))
                {
                    yield return item;
                }
            }
        }

    }
}
