using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class TableRowsIterator : HubSpotIteratorBase
    {
        private readonly Table _table;
        private readonly long _portalId;

        public TableRowsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Table table, long portalId) : base(client, jobData)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _portalId = portalId;
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;
            int count = 0;
            limit = limit ?? 500;
            var result = new List<object>();
            try
            {
                while (true)
                {
                    var dateColumn = _table.columns.Find(c => c.type == "DATE");
                    var response = Client.GetTableRowsAsync(JobData.LastCrawlFinishTime, _table.id, dateColumn, _portalId, limit.Value, offset).Result;

                    if (response.Objects == null || !response.Objects.Any())
                        break;

                    foreach (var row in response.Objects)
                    {
                        row.Columns = _table.columns;
                        row.Table = _table.id;
                        result.Add(row);
                    }

                    count += 500;

                    if (response.Total < count || response.TotalCount < count)
                        break;

                    offset = response.Offset;
                }
            }
            catch
            {
                return Enumerable.Empty<object>();
            }

            return result;
        }
    }
}
