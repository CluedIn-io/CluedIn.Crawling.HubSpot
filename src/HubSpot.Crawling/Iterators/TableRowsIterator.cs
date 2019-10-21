using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class TableRowsIterator : HubSpotIteratorBase
    {
        private readonly Table _table;
        private readonly long _portalId;

        public TableRowsIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, Table table, long portalId, ILogger logger)
            : base(client, jobData, logger)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _portalId = portalId;
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var offset = 0;
            var retries = 0;
            int count = 0;
            limit = limit ?? 500;
            var canContinue = true;

            while (canContinue)
            {

                var result = new List<object>();
                try
                {
                    var dateColumn = _table.columns.Find(c => c.type == "DATE");
                    var response = Client.GetTableRowsAsync(JobData.LastCrawlFinishTime, _table.id, dateColumn, _portalId, limit.Value, offset).Result;

                    if (response.Objects == null || !response.Objects.Any())
                        canContinue = false;
                    else
                    {
                        foreach (var row in response.Objects)
                        {
                            row.Columns = _table.columns;
                            row.Table = _table.id;
                            result.Add(row);
                        }

                        count += 500;

                        if (response.Total < count || response.TotalCount < count)
                            canContinue = false;
                        else
                        {
                            offset = response.Offset;
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
    }
}
