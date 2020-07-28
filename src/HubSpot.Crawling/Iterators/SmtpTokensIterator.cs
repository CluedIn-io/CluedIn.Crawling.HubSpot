using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using Microsoft.Extensions.Logging;

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
                Logger.LogWarning("Failed to retrieve data in {type}", GetType().FullName);
            }

            foreach (var item in result)
            {
                yield return item;
            }
        }
    }
}
