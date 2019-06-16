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
                return Enumerable.Empty<object>();
            }

            var client = _clientFactory.CreateNew(hubspotcrawlJobData);

            var settings = client.GetSettingsAsync().Result;

            var companyProperties = client.GetCompanyPropertiesAsync(settings).Result;

            var data = new List<object>();
            data.AddRange(GetCompaniesAndAssociatedObjects(client, hubspotcrawlJobData, companyProperties, settings));

            var dealProperties = client.GetDealPropertiesAsync(settings).Result;
            data.AddRange(GetDealsAndAssociatedObjects(client, dealProperties, settings));

            var contactProperties = client.GetContactPropertiesAsync(settings).Result;
            data.AddRange(GetContactsAndAssociatedObjects(client, contactProperties));

            data.AddRange(GetDynamicContactLists(client));
            data.AddRange(client.GetFormsAsync().Result);
            // yield return client.GetKeywordsAsync().Result; TODO This is deprecated https://developers.hubspot.com/changelog/2018-02-05-sunsetting-keywords-api-2018
            data.AddRange(client.GetOwnersAsync().Result);
            //yield return client.GetPublishingChannelsAsync().Result;  TODO Returns Http Forbidden code
            data.AddRange(GetFiles(client, jobData));
            data.AddRange(GetSiteMaps(client, jobData));
            data.AddRange(GetTemplates(client));
            data.AddRange(GetUrlMappings(client, jobData));
            data.AddRange(GetEngagements(client));
            data.AddRange(GetRecentDeals(client, jobData));
            data.AddRange(GetRecentlyCreatedDeals(client, jobData));
            //yield return client.GetSmtpTokensAsync().Result; TODO Returns Http Forbidden code
            data.AddRange(GetSocialCalendarEvents(client, jobData));
            data.AddRange(GetStaticContactLists(client));
            data.AddRange(GetTaskCalendarEvents(client, jobData));
            //yield return client.GetWorkflowsAsync().Result; TODO Returns Http Forbidden code
            data.AddRange(GetBlogPosts(client, jobData));
            data.AddRange(GetBlogs(client, jobData));
            data.AddRange(GetBlogTopics(client, jobData));
            data.AddRange(GetDomains(client, jobData));
            data.AddRange(GetBroadcastMessages(client, jobData));

            var productProperties = client.GetProductPropertiesAsync(settings).Result;
            data.AddRange(GetProducts(client, productProperties));

            var lineItemProperties = client.GetLineItemPropertiesAsync(settings).Result;
            data.AddRange(GetLineItemsAndAssociatedObjects(client, lineItemProperties));

            var ticketProperties = client.GetTicketPropertiesAsync(settings).Result;
            data.AddRange(GetTicketsAndAssociatedObjects(client, ticketProperties));

            return data;
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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response)
                {
                    yield return obj;
                }

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

                foreach (var obj in response)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.deals)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.deals)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.deals)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.results)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var obj in response.objects)
                {
                    yield return obj;
                }

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

                foreach (var list in response.lists)
                {
                    yield return list;
                }

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

                foreach (var list in response.lists)
                {
                    yield return list;
                }
                

                if (response.hasMore == false || response.lists.Count < limit || response.offset == null)
                    break;

                offset = response.offset.Value;
            }
        }

        private IEnumerable<object> GetDealAssociations(HubSpotClient client, int objectId)
        {
            int offset = 0;

            while (true)
            {
                var limit = 20;
                var response = client.GetDealAssociationsAsync(objectId, limit, offset).Result;

                if (response?.Results == null || !response.Results.Any())
                    break;

                foreach (var result in response.Results)
                {
                    yield return result;
                }

                if (response.HasMore == false || response.Results.Count < limit)
                    break;

                offset = response.Offset;
            }
        }

        private IEnumerable<object> GetContactsAndAssociatedObjects(HubSpotClient client, List<string> properties)
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
                        foreach (var engagement in client.GetEngagementByIdAndTypeAsync(contact.Vid.Value, "CONTACT").Result)
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
        private IEnumerable<object> GetLineItemsAndAssociatedObjects(HubSpotClient client, List<string> properties)
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = client.GetLineItemsAsync(properties, limit, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var lineItem in response.Objects)
                {
                    if (lineItem.ObjectId.HasValue)
                    {
                        foreach (var dealAssociation in GetDealAssociations(client, lineItem.ObjectId.Value))
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

        private IEnumerable<object> GetTicketsAndAssociatedObjects(HubSpotClient client, List<string> properties)
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = client.GetTicketsAsync(properties, limit, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var ticket in response.Objects)
                {
                    if (ticket.ObjectId.HasValue)
                    {
                        foreach (var dealAssociation in GetDealAssociations(client, ticket.ObjectId.Value))
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

        private IEnumerable<object> GetProducts(HubSpotClient client, List<string> properties)
        {
            int offset = 0;

            while (true)
            {
                var limit = 100;
                var response = client.GetProductsAsync(properties, limit, offset).Result;

                if (response?.Objects == null || !response.Objects.Any())
                    break;

                foreach (var obj in response.Objects)
                {
                    yield return obj;
                }
                


                if (response.Objects.Count < limit || response.Offset == null)
                    break;

                offset = response.Offset.Value;
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

        private static IEnumerable<object> GetCompaniesAndAssociatedObjects(HubSpotClient client, HubSpotCrawlJobData jobData, List<string> companyProperties, Settings settings)
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
            var dealPipelines = client.GetDealPipelinesAsync().Result;
            foreach (var dealPipeline in dealPipelines)
            {
                dealPipeline.portalId = portalId;
                yield return dealPipeline;
            }


            var tables = client.GetTablesAsync().Result;
            foreach (var table in tables)
            {
                table.PortalId = portalId;

                yield return table;
                foreach (var row in GetTableRows(client, jobData, table, portalId))
                {
                    yield return row;
                }
            }

        }

        private static IEnumerable<object> GetTableRows(HubSpotClient client, HubSpotCrawlJobData jobData, Table table, long portalId)
        {
            int offset = 0;
            int count = 0;
            while (true)
            {
                var dateColumn = table.columns.Find(c => c.type == "DATE");
                var limit = 500;
                var response = client.GetTableRowsAsync(jobData.LastCrawlFinishTime, table.id, dateColumn, portalId, limit, offset).Result;

                if (response.Objects == null || !response.Objects.Any())
                    break;

                foreach (var row in response.Objects)
                {
                    row.Columns = table.columns;
                    row.Table = table.id;
                    yield return row;
                }

                count += 500;

                if (response.Total < count || response.TotalCount < count)
                    break;

                offset = response.Offset;
            }
        }
    }
}
