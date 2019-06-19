using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Crawling.HubSpot.Iteraters;

namespace CluedIn.Crawling.HubSpot
{
    public class HubSpotCrawler : ICrawlerDataGenerator
    {
        private readonly IHubSpotClientFactory _clientFactory;

        public HubSpotCrawler(IHubSpotClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (!(jobData is HubSpotCrawlJobData crawlerJobData))
            {
                return Enumerable.Empty<object>();
            }

            var client = _clientFactory.CreateNew(crawlerJobData);

            var settings = client.GetSettingsAsync().Result;

            var companyProperties = client.GetCompanyPropertiesAsync(settings).Result;

            var data = new List<object>();
            data.AddRange(new CompanyIterater(client, crawlerJobData, companyProperties, settings).Iterate());

            var dealProperties = client.GetDealPropertiesAsync(settings).Result;
            data.AddRange(new DealIterater(client, crawlerJobData, dealProperties, settings).Iterate());

            var contactProperties = client.GetContactPropertiesAsync(settings).Result;
            data.AddRange(new ContactIterater(client, crawlerJobData, contactProperties).Iterate());

            data.AddRange(new DynamicContactListIterater(client, crawlerJobData).Iterate());
            data.AddRange(client.GetFormsAsync().Result);
            //data.AddRange(client.GetKeywordsAsync().Result; TODO This is deprecated https://developers.hubspot.com/changelog/2018-02-05-sunsetting-keywords-api-2018
            data.AddRange(client.GetOwnersAsync().Result);
            //data.AddRange(client.GetPublishingChannelsAsync().Result);  TODO Returns Http Forbidden code
            data.AddRange(new FilesIterater(client, crawlerJobData).Iterate());
            data.AddRange(new SiteMapsIterater(client, crawlerJobData).Iterate());
            data.AddRange(new TemplatesIterater(client, crawlerJobData).Iterate()); 
            data.AddRange(new UrlMappingsIterater(client, crawlerJobData).Iterate()); 
            data.AddRange(new EngagementsIterater(client, crawlerJobData).Iterate());
            data.AddRange(new RecentDealsIterater(client, crawlerJobData).Iterate());
            data.AddRange(new RecentlyCreatedDealsIterater(client, crawlerJobData).Iterate());
            //data.AddRange(client.GetSmtpTokensAsync().Result); TODO Returns Http Forbidden code
            //data.AddRange(new SocialCalendarEventsIterater(client, crawlerJobData).Iterate()); TODO Returns Http Forbidden code
            data.AddRange(new StaticContactListIterater(client,crawlerJobData).Iterate());
            //data.AddRange(new TaskCalendarEventsIterater(client, crawlerJobData).Iterate());  TODO Returns Http Forbidden code
            //data.AddRange(client.GetWorkflowsAsync().Result)); TODO Returns Http Forbidden code
            //data.AddRange(new BlogPostsIterater(client, crawlerJobData).Iterate()); TODO Returns Http Forbidden code
            //data.AddRange(new BlogsIterater(client, crawlerJobData).Iterate()); TODO Returns Http Forbidden code
            //data.AddRange(new BlogTopicsIterater(client, crawlerJobData).Iterate()); TODO Returns Http Forbidden code
            //data.AddRange(new DomainsIterater(client, crawlerJobData).Iterate()); TODO Returns Http Forbidden code
            //data.AddRange(new BroadcastMessagesIterater(client, crawlerJobData).Iterate()); TODO Returns Http Forbidden code

            var productProperties = client.GetProductPropertiesAsync(settings).Result;
            data.AddRange(new ProductsIterater(client, crawlerJobData, productProperties).Iterate());

            //var lineItemProperties = client.GetLineItemPropertiesAsync(settings).Result;                      // TODO Must have scope DEAL_LINE_ITEM_READ
            //data.AddRange(new LineItemsIterater(client, lineItemProperties).Iterate());

            //var ticketProperties = client.GetTicketPropertiesAsync(settings).Result;              // TODO Must have scope TICKETS_READ
            //data.AddRange(new TicketsIterater(client, ticketProperties).Iterate());
            
            return data;
        }
    }
}
