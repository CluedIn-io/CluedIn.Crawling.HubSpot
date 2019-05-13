using System.Collections.Generic;

using CluedIn.Core.Crawling;
using CluedIn.Crawling.HubSpot.Core;
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
