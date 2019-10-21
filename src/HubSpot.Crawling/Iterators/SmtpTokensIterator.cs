using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;

namespace CluedIn.Crawling.HubSpot.Iterators
{
    public class SmtpTokensIterator : HubSpotIteratorBase
    {
        public SmtpTokensIterator(IHubSpotClient client, HubSpotCrawlJobData jobData, ILogger logger)
            : base(client, jobData, logger)
        {
        }

        public override IEnumerable<object> Iterate(int? limit = null)
        {
            var result = new List<object>();
            try
            {
                result.AddRange(Client.GetSmtpTokensAsync().Result);
            }
            catch
            {
                Logger.Warn(() => $"Failed to retrieve data in {GetType().FullName}");
            }

            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
