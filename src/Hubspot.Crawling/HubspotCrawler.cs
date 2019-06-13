using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;

namespace CluedIn.Crawling.HubSpot
{
    public class HubSpotCrawler : ICrawlerDataGenerator
    {
        private readonly IHubSpotClientFactory _clientFactory;
        public HubSpotCrawler(IHubSpotClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            var hubspotcrawlJobData = jobData as HubSpotCrawlJobData;
            if (hubspotcrawlJobData == null)
            {
                yield break;
            }

            var client = _clientFactory.CreateNew(hubspotcrawlJobData);

            var settings = client.GetSettingsAsync().Result;

            var companyProperties = client.GetCompanyPropertiesAsync(settings).Result;
            yield return GetCompaniesAndAssociatedObjects(client, companyProperties, settings);
            
            var dealProperties = client.GetDealPropertiesAsync(settings).Result;
            yield return GetDealsAndAssociatedObjects(client, dealProperties, settings);

            var contactProperties = client.GetContactPropertiesAsync(settings).Result;
            yield return GetContactsAndAssociatedObjects(client, contactProperties, settings);

            yield return GetDynamicContactLists(client);
            yield return client.GetFormsAsync().Result;
            yield return client.GetKeywordsAsync().Result;
            yield return client.GetOwnersAsync().Result;
            yield return client.GetPublishingChannelsAsync().Result;
            yield return GetFiles(client, jobData);
            yield return GetSiteMaps(client, jobData);
            yield return GetTemplates(client);
            yield return GetUrlMappings(client, jobData);
            yield return GetEngagements(client);
            yield return GetRecentDeals(client, jobData);
            yield return GetRecentlyCreatedDeals(client, jobData);
            yield return client.GetSmtpTokensAsync().Result;
            yield return GetSocialCalendarEvents(client, jobData);
            yield return GetStaticContactLists(client);
            yield return GetTaskCalendarEvents(client, jobData);
            yield return client.GetWorkflowsAsync().Result;
            yield return GetBlogPosts(client, jobData);
            yield return GetBlogs(client, jobData);
            yield return GetBlogTopics(client, jobData);
            yield return GetDomains(client, jobData);
            yield return GetBroadcastMessages(client, jobData);
        }

        private IEnumerable<object> GetFiles(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetFilesAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }
        
        private IEnumerable<object> GetSocialCalendarEvents(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetSocialCalendarEventsAsync(jobData.LastCrawlFinishTime, DateTimeOffset.UtcNow, limit, offset).Result;

                if (response == null || !response.Any())
                    break;

                yield return response;

                if (response.Count < limit)
                    break;

                offset += 100;
            }
        }

        private IEnumerable<object> GetTaskCalendarEvents(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetTaskCalendarEventsAsync(jobData.LastCrawlFinishTime, DateTimeOffset.UtcNow, limit, offset).Result;

                if (response == null || !response.Any())
                    break;

                yield return response;

                if (response.Count < limit)
                    break;

                offset += 100;
            }
        }

        private IEnumerable<object> GetRecentDeals(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetRecentDealsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                yield return response.deals;

                if (response.deals.Count < limit)
                    break;

                offset = response.offset;
            }
        }

        private IEnumerable<object> GetRecentlyCreatedDeals(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetRecentlyCreatedDealsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                yield return response.deals;

                if (response.deals.Count < limit)
                    break;

                offset = response.offset;
            }
        }

        private IEnumerable<object> GetBroadcastMessages(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = client.GetBroadcastMessagesAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                yield return response.deals;

                if (response.deals.Count < limit)
                    break;

                offset += limit;
            }
        }

        private IEnumerable<object> GetUrlMappings(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetUrlMappingsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetTemplates(HubSpotClient client)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetTemplatesAsync(limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetEngagements(HubSpotClient client)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetEngagementsAsync(limit, offset).Result;

                if (response?.results == null || !response.results.Any())
                    break;

                yield return response.results;

                if (response.results.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetSiteMaps(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetSiteMapsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetBlogPosts(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetBlogPostsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetDomains(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetDomainsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetBlogTopics(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetBlogTopicsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetBlogs(HubSpotClient client, CrawlJobData jobData)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetBlogsAsync(jobData.LastCrawlFinishTime, limit, offset).Result;

                if (response?.objects == null || !response.objects.Any())
                    break;

                yield return response.objects;

                if (response.objects.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetDynamicContactLists(HubSpotClient client)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetDynamicContactListsAsync(limit, offset).Result;

                if (response?.lists == null || !response.lists.Any())
                    break;

                yield return response.lists;

                if (response.hasMore == false || response.lists.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetStaticContactLists(HubSpotClient client)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetStaticContactListsAsync(limit, offset).Result;

                if (response?.lists == null || !response.lists.Any())
                    break;

                yield return response.lists;

                if (response.hasMore == false || response.lists.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetContactsAndAssociatedObjects(HubSpotClient client, List<string> properties, Settings settings)
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = client.GetContactsFromAllListsAsync(properties, limit, offset).Result;

                if (response?.contacts == null || !response.contacts.Any())
                    break;

                foreach (var contact in response.contacts)
                {

                    if (contact.Vid.HasValue)
                    {
                        var engagements = client.GetEngagementByIdAndTypeAsync(contact.Vid.Value, "CONTACT").Result;
                        foreach (var engagement in engagements)
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

        private IEnumerable<object> GetDealsAndAssociatedObjects(HubSpotClient client, IList<string> properties, Settings settings)
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = client.GetDealsAsync(properties, settings, limit, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                foreach (var deal in response.deals)
                {

                    if (settings?.currency != null)
                        deal.Currency = settings.currency;

                    if (deal.dealId.HasValue)
                    {
                        var engagements = client.GetEngagementByIdAndTypeAsync(deal.dealId.Value, "DEAL").Result;
                        foreach (var engagement in engagements)
                        {
                            yield return engagement;
                        }
                    }

                    yield return deal;
                }

                if (response.hasMore == false || response.deals.Count < limit)
                    break;

                offset = response.offset;
            }
        }

        private static IEnumerable<object> GetCompaniesAndAssociatedObjects(HubSpotClient client, List<string> companyProperties, Settings settings)
        {
            int offset = 0;
            long portalId = 0;
            while (true)
            {
                var limit = 100;
                var response = client.GetCompaniesAsync(companyProperties, limit, offset).Result;

                if (response.results == null || !response.results.Any())
                    break;

                foreach (var company in response.results)
                {
                    if (settings != null && settings.currency != null)
                        company.Currency = settings.currency;

                    if (company.portalId.HasValue)
                        portalId = company.portalId.Value;

                    yield return company;

                    if (company.companyId.HasValue)
                    {
                        var contacts = client.GetContactsByCompanyAsync(company.companyId.Value).Result;
                        foreach (var contact in contacts.contacts)
                        {
                            yield return contact;
                        }

                        var engagements = client.GetEngagementByIdAndTypeAsync(company.companyId.Value, "COMPANY").Result;
                        foreach (var engagement in engagements)
                        {
                            yield return engagement;
                        }
                    }
                }


                if (response.hasMore == false || response.offset == null || response.results.Count < limit)
                    break;

                offset = response.offset.Value;
            }

            // TODO Is this correct? Just get deal pipelines for last company portal id?
            var dealPipelines = client.GetDealPipelinesAsync(portalId).Result;
            foreach (var dealPipeline in dealPipelines)
            {
                yield return dealPipeline;
            }
        }
    }
}
