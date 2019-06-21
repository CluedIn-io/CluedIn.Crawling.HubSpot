using System.Collections.Generic;
using System.Configuration;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Core.Configuration;

namespace Crawling.HubSpot.Integration.Test
{
    public static class HubSpotConfiguration
    {
        public static Dictionary<string, object> Create()
        {
            return new Dictionary<string, object> {
                //{ HubSpotConstants.KeyName.BaseUri, "" },
                { HubSpotConstants.KeyName.BaseUri, ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.BaseUri, "https://api.hubapi.com/") },
                { HubSpotConstants.KeyName.ApiToken, ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.ApiToken, "")  },
                { HubSpotConstants.KeyName.CustomerSubDomain, ConfigurationManager.AppSettings.GetValue(HubSpotConstants.KeyName.CustomerSubDomain, "") }
            };
        }
    }
}
