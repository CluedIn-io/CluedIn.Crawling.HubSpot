using System;
using System.Collections;
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

            var settings = client.GetSettingsAsync().Result;

            var companyProperties = client.GetCompanyPropertiesAsync(settings).Result;

            foreach (var o in GetCompaniesAndAssociatedObjects(client, companyProperties, settings))
            {
                yield return o;
            }


            var dealProperties = client.GetDealPropertiesAsync(settings).Result;
            foreach (var deal in GetDealsAndAssociatedObjects(client, dealProperties, settings))
            {
                yield return deal;
            }

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
