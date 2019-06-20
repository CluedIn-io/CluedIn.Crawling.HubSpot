using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Crawling.HubSpot.Iterators;

namespace CluedIn.Crawling.HubSpot
{
    public class Crawler : ICrawlerDataGenerator
    {
        private readonly IHubSpotClientFactory _clientFactory;

        public Crawler(IHubSpotClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData));
            }

            if (!(jobData is HubSpotCrawlJobData crawlerJobData))
            {
                return Enumerable.Empty<object>();
            }

            var client = _clientFactory.CreateNew(crawlerJobData);

            var settings = client.GetSettingsAsync().Result;

            var companyProperties = client.GetCompanyPropertiesAsync(settings).Result;

            crawlerJobData.LastCrawlFinishTime = new DateTimeOffset(new DateTime(2019, 6, 18, 0, 0, 0));

            var data = new List<object>();
            data.AddRange(new CompanyIterator(client, crawlerJobData, companyProperties, settings).Iterate());

            var dealProperties = client.GetDealPropertiesAsync(settings).Result;
            data.AddRange(new DealIterator(client, crawlerJobData, dealProperties, settings).Iterate());

            var contactProperties = client.GetContactPropertiesAsync(settings).Result;
            data.AddRange(new ContactIterator(client, crawlerJobData, contactProperties).Iterate());

            data.AddRange(new DynamicContactListIterator(client, crawlerJobData).Iterate());
            data.AddRange(client.GetFormsAsync().Result);
            //data.AddRange(client.GetKeywordsAsync().Result; TODO This is deprecated https://developers.hubspot.com/changelog/2018-02-05-sunsetting-keywords-api-2018
            data.AddRange(client.GetOwnersAsync().Result);
            data.AddRange(client.GetPublishingChannelsAsync().Result); // TODO This is connected to fake LinkedIn using bfi@cluedin.com
            data.AddRange(new FilesIterator(client, crawlerJobData).Iterate());
            data.AddRange(new SiteMapsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new TemplatesIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new UrlMappingsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new EngagementsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new RecentDealsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new RecentlyCreatedDealsIterator(client, crawlerJobData).Iterate());
            //data.AddRange(client.GetSmtpTokensAsync().Result);  TODO Returns Http Forbidden code
            data.AddRange(new SocialCalendarEventsIterator(client, crawlerJobData).Iterate()); // TODO Not sure how to test this
            data.AddRange(new StaticContactListIterator(client,crawlerJobData).Iterate());
            data.AddRange(new TaskCalendarEventsIterator(client, crawlerJobData).Iterate());//  TODO Not sure how to test this
            data.AddRange(client.GetWorkflowsAsync().Result.workflows);
            data.AddRange(new BlogPostsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BlogsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BlogTopicsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new DomainsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BroadcastMessagesIterator(client, crawlerJobData).Iterate()); 

            var productProperties = client.GetProductPropertiesAsync(settings).Result;
            data.AddRange(new ProductsIterator(client, crawlerJobData, productProperties).Iterate());

            var lineItemProperties = client.GetLineItemPropertiesAsync(settings).Result;                      
            data.AddRange(new LineItemsIterator(client, crawlerJobData, lineItemProperties).Iterate());

            var ticketProperties = client.GetTicketPropertiesAsync(settings).Result;              
            data.AddRange(new TicketsIterator(client, crawlerJobData, ticketProperties).Iterate());

            return data;
        }
    }
}
