using System;
using System.Linq;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Crawling.HubSpot.ClueProducers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Factories;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Xunit;

namespace Provider.HubSpot.Unit.Test.ClueProducerTests
{
    public class ClueProducerTests
    {
        private readonly Guid _organizationId = Guid.NewGuid();

        public class ChannelClueProducerTests : ClueProducerTests
        {
            private Clue MakeClue(Channel data)
            {
                var prod = new ChannelClueProducer(new HubSpotClueFactory());
                var clue = prod.MakeClue(data, _organizationId);
                return clue;
            }

            [Theory,
            AutoData]
            public void MakeClueCreatesClue(string channelId, string accountGuid, string type, string channelGuid, long createdAt, string name, long portalId, long updatedAt)
            {
                var data = new Channel {channelId = channelId, accountGuid = accountGuid, type = type, channelGuid = channelGuid, createdAt = createdAt, name = name, portalId = portalId, updatedAt = updatedAt};

                var clue = MakeClue(data);

                Assert.NotNull(clue);
                Assert.Equal(clue.Data.EntityData.Properties[HubSpotVocabulary.Channel.Type], type);

                // ...
                // TODO test all properties
                // ...

                // Test we have an incoming edge pointing to portal
                Assert.True(clue.Data.EntityData.IncomingEdges.Count == 1);
                Assert.Equal(clue.Data.EntityData.IncomingEdges.First().FromReference.Code.Value, portalId.ToString());
            }

        }
    }
}
