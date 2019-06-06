using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;

namespace Tests.Integration.HubSpot
{
  public static class HubSpotConfiguration
  {
    public static Dictionary<string, object> Create()
    {
            return new Dictionary<string, object> {
                    { HubSpotConstants.KeyName.ApiToken,"b2fecc8c-1dcc-4254-a95a-73be97b0e8af" },
                    { HubSpotConstants.KeyName.CustomerSubDomain, "" }
            };
    }
  }
}
