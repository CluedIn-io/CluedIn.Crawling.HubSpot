using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using CluedIn.Core.Providers;
using Provider.HubSpot.Unit.Test.HubspotProvider;
using Should;
using Xunit;

namespace Provider.HubSpot.Unit.Test.HubSpotProvider
{
    public class GetCrawlJobDataBehaviour : HubSpotProviderTest
    {
        [Theory]
        [InlineAutoData]
        public void GetCrawlJobDataTests(Dictionary<string, object> dictionary, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            Sut.GetCrawlJobData(default(ProviderUpdateContext), dictionary, organizationId, userId, providerDefinitionId)
                .ShouldNotBeNull();
        }
    }
}
