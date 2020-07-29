using System;
using System.Collections.Generic;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure;
using Moq;

namespace Crawling.HubSpot.Unit.Test.IteratorTests
{
    public class BaseIteratorTest
    {
        protected Mock<IHubSpotClient> Client;
        protected HubSpotCrawlJobData JobData { get; set; }

        public BaseIteratorTest()
        {
            Client = new Mock<IHubSpotClient>();
            JobData = new HubSpotCrawlJobData(new Dictionary<string, object>
            {
                { HubSpotConstants.KeyName.BaseUri, ""},
                { HubSpotConstants.KeyName.ApiToken, Guid.NewGuid() },
                { HubSpotConstants.KeyName.CustomerSubDomain, "" },
                { HubSpotConstants.KeyName.LastCrawlFinishTime, DateTimeOffset.Now }
            });
        }

       
    }
}
