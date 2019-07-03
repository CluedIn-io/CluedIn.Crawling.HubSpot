using System;
using System.Collections.Generic;
using System.Configuration;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.HubSpot.Core;

namespace Crawling.HubSpot.Test.Common
{ 
    public static class HubSpotConfiguration
    {
        public static Dictionary<string, object> Create()
        {
            var result = new Dictionary<string, object> {
                { HubSpotConstants.KeyName.BaseUri, ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.BaseUri, "https://api.hubapi.com") },
                { HubSpotConstants.KeyName.ApiToken, ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.ApiToken, "")  },
                { HubSpotConstants.KeyName.CustomerSubDomain, ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.CustomerSubDomain, "") }
            };

            // Check to see if we have a manually provided last crawl time
            var lastFinishTime = ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.LastCrawlFinishTime, (string) null);

            if (!string.IsNullOrWhiteSpace(lastFinishTime))
            {
                DateTimeOffset lastCrawlFinishTime;
                if (DateTimeOffset.TryParse(lastFinishTime, out lastCrawlFinishTime))
                {
                    result.Add(HubSpotConstants.KeyName.LastCrawlFinishTime, lastCrawlFinishTime);
                }
            }

            return result;
        }
    }
}
