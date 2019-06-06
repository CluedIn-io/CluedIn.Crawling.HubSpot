using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (hubspotcrawlJobData == null)
            {
                yield break;
            }

            var client = _clientFactory.CreateNew(hubspotcrawlJobData);

            //TODO
            var settings = client.GetSettingsAsync().Result;

            var companies = client.GetCompaniesAsync().Result;
            foreach (var company in companies.results)
            {
                yield return company;
                if (company.companyId.HasValue)
                {
                    var contacts = client.GetContactsByCompanyAsync(company.companyId.Value).Result;
                    foreach(var contact in contacts.contacts)
                    {
                        yield return contact;
                    }
                }
            }

            //foreach( var folder in client.GetFolders())
            //{
            //    yield return folder;
            //    foreach (var file in client.GetFilesForFolder(folder.Id))
            //    {
            //        yield return file;
            //    }
            //}
            throw new NotImplementedException();
        }
    }
}
