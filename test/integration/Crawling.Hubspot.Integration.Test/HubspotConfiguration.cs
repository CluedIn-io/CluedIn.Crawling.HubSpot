using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;

namespace Crawling.HubSpot.Integration.Test
{
    public static class HubSpotConfiguration
    {
        public static Dictionary<string, object> Create()
        {
            return new Dictionary<string, object> {
                { HubSpotConstants.KeyName.BaseUri, "https://api.hubapi.com/" },
                { HubSpotConstants.KeyName.ApiToken,"4fad15b1-8d51-4919-b11a-125bd9346e51" },
                { HubSpotConstants.KeyName.CustomerSubDomain, "" }
            };
        }
    }
}
