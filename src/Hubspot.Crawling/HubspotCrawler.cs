using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.Hubspot.Core;
using CluedIn.Crawling.Hubspot.Infrastructure.Factories;

namespace CluedIn.Crawling.Hubspot
{
    public class HubspotCrawler : ICrawlerDataGenerator
    {
        private readonly IHubspotClientFactory _clientFactory;
        public HubspotCrawler(IHubspotClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            var hubspotcrawlJobData = jobData as HubspotCrawlJobData;
            if(hubspotcrawlJobData == null)
            {
                yield break;
            }

            var client = _clientFactory.CreateNew(hubspotcrawlJobData);

            //crawl data from provider and yield objects

            foreach( var folder in client.GetFolders())
            {
                yield return folder;
                foreach (var file in client.GetFilesForFolder(folder.Id))
                {
                    yield return file;
                }
            }
        }       
    }
}
