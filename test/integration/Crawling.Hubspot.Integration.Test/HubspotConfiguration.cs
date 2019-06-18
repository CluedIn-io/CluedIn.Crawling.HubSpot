using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;

namespace Crawling.HubSpot.Integration.Test
{
    public static class HubSpotConfiguration
    {
        public static Dictionary<string, object> Create()
        {
            return new Dictionary<string, object> {
                    { HubSpotConstants.KeyName.BaseUri, "http://127.0.0.1:8080/" },
                    { HubSpotConstants.KeyName.ApiToken,"b2fecc8c-1dcc-4254-a95a-73be97b0e8af" },
                    { HubSpotConstants.KeyName.CustomerSubDomain, "" }
            };
        }
    }
}
