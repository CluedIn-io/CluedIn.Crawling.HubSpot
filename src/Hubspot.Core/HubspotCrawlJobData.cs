using System;
using System.Collections.Generic;
using CluedIn.Core.Crawling;

namespace CluedIn.Crawling.HubSpot.Core
{
    public class HubSpotCrawlJobData : CrawlJobData
    {

        public HubSpotCrawlJobData(IDictionary<string, object> configuration)
        {
            if (configuration == null)
            {
                return;
            }

            BaseUri = GetValue<Uri>(configuration, HubSpotConstants.KeyName.BaseUri);
            ApiToken = GetValue<string>(configuration, HubSpotConstants.KeyName.ApiToken);
            CustomerSubDomain = GetValue<string>(configuration, HubSpotConstants.KeyName.CustomerSubDomain);
        }

        public IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object> {
               { HubSpotConstants.KeyName.ApiToken, ApiToken },
               { HubSpotConstants.KeyName.CustomerSubDomain, ApiToken }
            };
        }

        //TODO
        //public IList<CrawlEntry> StartingPoints { get; set; }

        public Uri BaseUri { get; set; }

        public string ApiToken { get; set; }

        public string CustomerSubDomain { get; set; }
    }
}
