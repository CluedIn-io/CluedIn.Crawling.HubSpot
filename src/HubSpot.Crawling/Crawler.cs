using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Crawling.HubSpot.Iterators;

namespace CluedIn.Crawling.HubSpot
{
    public class Crawler : ICrawlerDataGenerator
    {
        private readonly IHubSpotClientFactory _clientFactory;

        private static readonly IEnumerable<object> EmptyResult = Enumerable.Empty<object>();

        public Crawler(IHubSpotClientFactory clientFactory) // TODO add ILogger logger
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            try
            {
                return GetDataAsync(jobData).Result;
            }
            catch (AggregateException e)
            {
                // TODO Log.Error(e);
                throw e.InnerExceptions.First();
            }
        }

        public async Task<IEnumerable<object>> GetDataAsync(CrawlJobData jobData)
        {
            if (jobData == null)
            {
                throw new ArgumentNullException(nameof(jobData));
            }

            if (!(jobData is HubSpotCrawlJobData crawlerJobData))
            {
                return EmptyResult;
            }

            var client = _clientFactory.CreateNew(crawlerJobData);

            var settings = await client.GetSettingsAsync();

            if (settings == null)
            {
                // TODO Log the missing settings ...
                return EmptyResult;
            }

            var companyProperties = await client.GetCompanyPropertiesAsync(settings);

            var data = new List<object>();
            data.AddRange(new CompanyIterator(client, crawlerJobData, companyProperties, settings).Iterate());

            var dealProperties = await client.GetDealPropertiesAsync(settings);
            data.AddRange(new DealIterator(client, crawlerJobData, dealProperties, settings).Iterate());

            var contactProperties = await client.GetContactPropertiesAsync(settings);
            data.AddRange(new ContactIterator(client, crawlerJobData, contactProperties).Iterate());

            data.AddRange(new DynamicContactListIterator(client, crawlerJobData).Iterate());
            data.AddRange(await client.GetFormsAsync());
            //data.AddRange(await client.GetKeywordsAsync(); TODO This is deprecated https://developers.hubspot.com/changelog/2018-02-05-sunsetting-keywords-api-2018
            data.AddRange(await client.GetOwnersAsync());
            data.AddRange(await client.GetPublishingChannelsAsync()); // TODO This is connected to fake LinkedIn using bfi@cluedin.com
            data.AddRange(new FilesIterator(client, crawlerJobData).Iterate());
            data.AddRange(new SiteMapsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new TemplatesIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new UrlMappingsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new EngagementsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new RecentDealsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new RecentlyCreatedDealsIterator(client, crawlerJobData).Iterate());
            //data.AddRange(await client.GetSmtpTokensAsync());  TODO Returns Http Forbidden code
            data.AddRange(new SocialCalendarEventsIterator(client, crawlerJobData).Iterate()); // TODO Not sure how to test this
            data.AddRange(new StaticContactListIterator(client,crawlerJobData).Iterate());
            data.AddRange(new TaskCalendarEventsIterator(client, crawlerJobData).Iterate());//  TODO Not sure how to test this
            data.AddRange((await client.GetWorkflowsAsync()).workflows);
            data.AddRange(new BlogPostsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BlogsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BlogTopicsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new DomainsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BroadcastMessagesIterator(client, crawlerJobData).Iterate()); 

            var productProperties = await client.GetProductPropertiesAsync(settings);
            data.AddRange(new ProductsIterator(client, crawlerJobData, productProperties).Iterate());

            var lineItemProperties = await client.GetLineItemPropertiesAsync(settings);                      
            data.AddRange(new LineItemsIterator(client, crawlerJobData, lineItemProperties).Iterate());

            var ticketProperties = await client.GetTicketPropertiesAsync(settings);              
            data.AddRange(new TicketsIterator(client, crawlerJobData, ticketProperties).Iterate());

            return data;
        }
    }
}
