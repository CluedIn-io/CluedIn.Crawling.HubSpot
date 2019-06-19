using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class DealIterator : HubSpotIteratorBase
    {
        private readonly IList<string> _properties;
        private readonly Settings _settings;

        public DealIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, IList<string> properties, Settings settings) : base(client, jobData)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            int offset = 0;

            limit = limit ?? 100;

            while (true)
            {
                var response = Client.GetDealsAsync(_properties, _settings, limit.Value, offset).Result;

                if (response?.deals == null || !response.deals.Any())
                    break;

                foreach (var deal in response.deals)
                {

                    if (_settings?.currency != null)
                        deal.Currency = _settings.currency;

                    if (deal.dealId.HasValue)
                    {
                        var engagements = Client.GetEngagementByIdAndTypeAsync(deal.dealId.Value, "DEAL").Result;
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
    }
}
