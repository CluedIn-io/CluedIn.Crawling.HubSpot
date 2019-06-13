using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            //TODO
            var settings = client.GetSettingsAsync().Result;

            var companyProperties = client.GetCompanyPropertiesAsync(settings).Result;

            foreach (var o in GetCompaniesAndAssociatedObjects(client, companyProperties, settings))
            {
                yield return o;
            }


            var dealProperties = client.GetDealPropertiesAsync(settings).Result;


            //var companies = client.GetCompaniesAsync().Result;
            //foreach (var company in companies.results)
            //{
            //    yield return company;
            //    if (company.companyId.HasValue)
            //    {
            //        var contacts = client.GetContactsByCompanyAsync(company.companyId.Value).Result;
            //        foreach(var contact in contacts.contacts)
            //        {
            //            yield return contact;
            //        }
            //    }
            //}

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

        private static IEnumerable<object> GetCompaniesAndAssociatedObjects(HubSpotClient client, List<string> companyProperties, Settings settings)
        {
            int offset = 0;
            long portalId = 0;
            while (true)
            {
                var companies = client.GetCompaniesAsync(companyProperties, 100, offset).Result;

                if (companies.results != null)
                {
                    foreach (var company in companies.results)
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
                }

                if (companies.results == null || companies.hasMore == false)
                    break;

                if (companies.offset == null)
                    break;

                if (companies.results.Count < 100)
                    break;

                offset = companies.offset.Value;

                //System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(10)).Wait(state.CancellationTokenSource.Token);
            }

            // TODO Is this correct? Just get deal pipelines for last company portal id?
            var dealPipelines = client.GetDealPipelinesAsync(portalId);
            foreach (var dealPipeline in dealPipelines.Result)
            {
                yield return dealPipeline;
            }
        }
    }
}
