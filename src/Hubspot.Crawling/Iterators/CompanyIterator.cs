using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class CompanyIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public CompanyIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings) : base(client, jobData)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            long offset = 0;
            long portalId = 0;
            var retries = 0;
            limit = limit ?? 100;
            var result = new List<object>();
            try
            {
                var properties = Client.GetCompanyPropertiesAsync(_settings).Result;

                while (true)
                {
                    try
                    {
                        var response = Client.GetCompaniesAsync(properties, limit.Value, offset).Result;

                    if (response.results == null || !response.results.Any())
                        break;

                    foreach (var company in response.results)
                    {
                        if (_settings?.currency != null)
                            company.Currency = _settings.currency;

                        if (company.portalId.HasValue)
                            portalId = company.portalId.Value;

                        result.Add(company);

                        if (company.companyId.HasValue)
                        {
                            GetContacts(company, result);

                            GetEngagements(company, result);
                        }
                    }

                    if (response.hasMore == false || response.offset == null || response.results.Count < limit)
                        break;

                    offset = response.offset.Value;
                    retries = 0;
                    }
                    catch (ThrottlingException e)
                    {
                        if (!ShouldRetryThrottledCall(e, retries))
                        {
                            break;
                        }

                        retries++;
                    }
                }

                GetDealPipelines(portalId, result);

                GetTables(portalId, result);
            }
            catch
            {
                return Enumerable.Empty<object>();
            }

            return result;
        }

        private void GetTables(long portalId, List<object> result)
        {
            try
            {
                var tables = Client.GetTablesAsync().Result;

                foreach (var table in tables)
                {
                    table.PortalId = portalId;

                    result.Add(table);
                    result.AddRange(new TableRowsIterator(Client, JobData, table, portalId).Iterate());
                }
            }
            catch
            {
                // ignored
            }
        }

        private void GetDealPipelines(long portalId, List<object> result)
        {
            try
            {
                var dealPipelines = Client.GetDealPipelinesAsync().Result;
                foreach (var dealPipeline in dealPipelines)
                {
                    dealPipeline.portalId = portalId;
                    result.Add(dealPipeline);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void GetEngagements(Company company, List<object> result)
        {
            try
            {
                var engagements = Client.GetEngagementByIdAndTypeAsync(company.companyId.Value, "COMPANY").Result;
                result.AddRange(engagements);
            }
            catch
            {
                // ignored
            }
        }

        private void GetContacts(Company company, List<object> result)
        {
            try
            {
                var contacts = Client.GetContactsByCompanyAsync(company.companyId.Value).Result;
                result.AddRange(contacts.contacts);
            }
            catch
            {
                // ignored
            }
        }
    }
}
