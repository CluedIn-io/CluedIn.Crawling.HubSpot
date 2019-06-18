using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iteraters
{
    public class CompanyIterater : HubSpotIteraterBase
    {
        private readonly IList<string> _properties;
        private readonly Settings _settings;

        public CompanyIterater(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties, Settings settings) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate()
        {
            int offset = 0;
            long portalId = 0;
            while (true)
            {
                var limit = 100;
                var response = Client.GetCompaniesAsync(_properties, limit, offset).Result;

                if (response.results == null || !response.results.Any())
                    break;

                foreach (var company in response.results)
                {
                    if (_settings != null && _settings.currency != null)
                        company.Currency = _settings.currency;

                    if (company.portalId.HasValue)
                        portalId = company.portalId.Value;

                    yield return company;

                    if (company.companyId.HasValue)
                    {
                        var contacts = Client.GetContactsByCompanyAsync(company.companyId.Value).Result;
                        foreach (var contact in contacts.contacts)
                        {
                            yield return contact;
                        }

                        var engagements = Client.GetEngagementByIdAndTypeAsync(company.companyId.Value, "COMPANY").Result;
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
            var dealPipelines = Client.GetDealPipelinesAsync().Result;
            foreach (var dealPipeline in dealPipelines)
            {
                dealPipeline.portalId = portalId;
                yield return dealPipeline;
            }


            //var tables = client.GetTablesAsync().Result; TODO Forbidden
            //foreach (var table in tables)
            //{
            //    table.PortalId = portalId;

            //    yield return table;
            //    foreach (var row in GetTableRows(client, jobData, table, portalId))
            //    {
            //        yield return row;
            //    }
            //}
        }

        private static IEnumerable<object> GetTableRows(HubSpotClient client, HubSpotCrawlJobData jobData, Table table, long portalId)
        {
            int offset = 0;
            int count = 0;
            while (true)
            {
                var dateColumn = table.columns.Find(c => c.type == "DATE");
                var limit = 500;
                var response = client.GetTableRowsAsync(jobData.LastCrawlFinishTime, table.id, dateColumn, portalId, limit, offset).Result;

                if (response.Objects == null || !response.Objects.Any())
                    break;

                foreach (var row in response.Objects)
                {
                    row.Columns = table.columns;
                    row.Table = table.id;
                    yield return row;
                }

                count += 500;

                if (response.Total < count || response.TotalCount < count)
                    break;

                offset = response.Offset;
            }
        }
    }
}
