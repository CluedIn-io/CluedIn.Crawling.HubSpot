using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CluedIn.Core.Crawling;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Crawling.HubSpot.Iterators;

namespace CluedIn.Crawling.HubSpot
{
    public class Crawler : ICrawlerDataGenerator
    {
        private readonly IHubSpotClientFactory _clientFactory;
        private readonly ILogger _log;

        private static readonly IEnumerable<object> EmptyResult = Enumerable.Empty<object>();

        public Crawler(IHubSpotClientFactory clientFactory, ILogger log)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            try
            {
                return GetDataAsync(jobData).Result;
            }
            catch (AggregateException e)
            {
                _log.Error(e.InnerExceptions.First().Message);
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
                _log.Error("Settings could not be obtained from HubSpot");
                return EmptyResult;
            }

            

            var data = new List<object>();

            data.AddRange(new CompanyIterator(client, crawlerJobData, settings).Iterate());
            data.AddRange(new DealIterator(client, crawlerJobData, settings).Iterate());
            data.AddRange(new ContactIterator(client, crawlerJobData, settings).Iterate());
            data.AddRange(new DynamicContactListIterator(client, crawlerJobData).Iterate());
            data.AddRange(new FormsIterator(client, crawlerJobData).Iterate());
            //data.AddRange(await client.GetKeywordsAsync(); TODO This is deprecated https://developers.hubspot.com/changelog/2018-02-05-sunsetting-keywords-api-2018
            data.AddRange(new OwnersIterator(client, crawlerJobData).Iterate());
            data.AddRange(new PublishingChannelsIterator(client, crawlerJobData).Iterate()); // TODO This is connected to fake LinkedIn using bfi@cluedin.com
            data.AddRange(new FilesIterator(client, crawlerJobData).Iterate());
            data.AddRange(new SiteMapsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new TemplatesIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new UrlMappingsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new EngagementsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new RecentDealsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new RecentlyCreatedDealsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new SmtpTokensIterator(client, crawlerJobData).Iterate());            // TODO Returns Http Forbidden code
            data.AddRange(new SocialCalendarEventsIterator(client, crawlerJobData).Iterate()); // TODO Not sure how to test this
            data.AddRange(new StaticContactListIterator(client,crawlerJobData).Iterate());
            data.AddRange(new TaskCalendarEventsIterator(client, crawlerJobData).Iterate());//  TODO Not sure how to test this
            data.AddRange(new WorkflowsIterator(client, crawlerJobData).Iterate());
            data.AddRange(new BlogPostsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BlogsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BlogTopicsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new DomainsIterator(client, crawlerJobData).Iterate()); 
            data.AddRange(new BroadcastMessagesIterator(client, crawlerJobData).Iterate());
            data.AddRange(new ProductsIterator(client, crawlerJobData, settings).Iterate());
            data.AddRange(new LineItemsIterator(client, crawlerJobData, settings).Iterate());
            data.AddRange(new TicketsIterator(client, crawlerJobData, settings).Iterate());

            return data;
        }
    }
}
