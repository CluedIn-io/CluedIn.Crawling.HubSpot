using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Should;
using Xunit;

namespace Provider.Hubspot.Unit.Test.HubspotProvider
{
  public class GetCrawlJobDataBehaviour : HubSpotProviderTest
  {
    [Theory]
    [InlineAutoData]
    public void GetCrawlJobDataTests(Dictionary<string, object> dictionary, Guid organizationId, Guid userId, Guid providerDefinitionId)
    {
      //TODO: passing null here does not look good
      Sut.GetCrawlJobData(null, dictionary, organizationId, userId, providerDefinitionId)
          .ShouldNotBeNull();
    }
  }
}
