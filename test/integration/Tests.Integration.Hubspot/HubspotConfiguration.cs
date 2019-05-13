using System.Collections.Generic;
using CluedIn.Crawling.Hubspot.Core;

namespace Tests.Integration.Hubspot
{
  public static class HubspotConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { HubspotConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
