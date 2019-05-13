using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;

namespace Tests.Integration.HubSpot
{
  public static class HubSpotConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { HubSpotConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
