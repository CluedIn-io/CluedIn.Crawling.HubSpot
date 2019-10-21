using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class CompanyIterator : HubSpotIteratorBase
    {
        private readonly Settings _settings;

        public CompanyIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Settings settings, ILogger logger)
            : base(client, jobData, logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            long offset = 0;
            var retries = 0;
            limit = limit ?? 100;
            var canContinue = true;
            var properties = Client.GetCompanyPropertiesAsync(_settings).Result;

            while (canContinue)
            {
                var result = new List<object>();
                try
                {
                    var response = Client.GetCompaniesAsync(properties, limit.Value, offset).Result;

                    if (response.results == null || !response.results.Any())
                        canContinue = false;
                    else
                    {

                        foreach (var company in response.results)
                        {
                            if (_settings?.currency != null)
                                company.Currency = _settings.currency;

                            result.Add(company);

                            if (company.companyId.HasValue)
                            {
                                GetContacts(company, result);

                                GetEngagements(company, result);
                            }
                        }

                        if (response.hasMore == false || response.offset == null || response.results.Count < limit)
                            canContinue = false;
                        else
                        {
                            offset = response.offset.Value;
                            retries = 0;
                        }
                    }
                }
                catch (ThrottlingException e)
                {
                    if (!ShouldRetryThrottledCall(e, retries))
                    {
                        canContinue = false;
                    }

                    retries++;
                }
                catch
                {
                    Logger.Warn(() => $"Failed to retrieve data in {GetType().FullName}");
                    canContinue = false;
                }

                foreach (var item in result)
                {
                    yield return item;
                }
            } 
        }

        public IEnumerable<object> GetTables(long portalId)
        {
            IList<object> result = new List<object>();
            try
            {
                var tables = Client.GetTablesAsync().Result;

                foreach (var table in tables)
                {
                    table.PortalId = portalId;

                    result.Add(table);
                    result.AddRange(new TableRowsIterator(Client, JobData, table, portalId, Logger).Iterate());
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(() => $"Failed to get Tables for portal Id {portalId}", exception);
            }

            return result;
        }

        public IEnumerable<object> GetDealPipelines(long portalId)
        {
            IList<object> result = new List<object>();
            try
            {
                var dealPipelines = Client.GetDealPipelinesAsync().Result;
                foreach (var dealPipeline in dealPipelines)
                {
                    dealPipeline.portalId = portalId;
                    result.Add(dealPipeline);
                }
            }
            catch (Exception exception)
            {
                Logger.Warn(() => $"Failed to get Deal Pipelines for portal Id {portalId}", exception);
            }

            return result;
        }

        private void GetEngagements(Company company, List<object> result)
        {
            try
            {
                var engagements = Client.GetEngagementByIdAndTypeAsync(company.companyId.Value, "COMPANY").Result;
                result.AddRange(engagements);
            }
            catch (Exception exception)
            {
                Logger.Warn(() => $"Failed to get Engagements for Company {company.companyId} ", exception);
            }
        }

        private void GetContacts(Company company, List<object> result)
        {
            try
            {
                var contacts = Client.GetContactsByCompanyAsync(company.companyId.Value).Result;
                result.AddRange(contacts.contacts);
            }
            catch (Exception exception)
            {
                Logger.Warn(() => $"Failed to get Contacts for Company {company.companyId} ", exception);
            }
        }
    }
}
