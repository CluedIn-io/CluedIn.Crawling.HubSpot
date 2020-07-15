using System.Linq;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.ClueProducers;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Factories;
using CluedIn.Crawling.HubSpot.Vocabularies;
using Xunit;

namespace Crawling.HubSpot.Unit.Test.ClueProducers
{
    public class ChannelClueProducerTests : BaseClueProducerTest<Channel>
    {
        [Theory,
         AutoData]
        public void MakeClueCreatesClue(string channelId, string accountGuid, string type, string channelGuid, long createdAt, string name, long portalId, long updatedAt)
        {
            var data = new Channel
            {
                channelId = channelId,
                accountGuid = accountGuid,
                type = type,
                channelGuid = channelGuid,
                createdAt = createdAt,
                name = name,
                portalId = portalId,
                updatedAt = updatedAt
            };

            var prod = new ChannelClueProducer(new HubSpotClueFactory());

            var clue = prod.MakeClue(data, OrganizationId);

            Assert.NotNull(clue);
            Assert.Equal(clue.Data.EntityData.Properties[HubSpotVocabulary.Channel.Type], type);

            // ...
            // test all properties
            // ...

            // Test we have an incoming edge pointing to portal
            Assert.Single(clue.Data.EntityData.OutgoingEdges);
            Assert.Equal(clue.Data.EntityData.OutgoingEdges.First().ToReference.Code.Value, portalId.ToString());
        }

        protected override BaseClueProducer<Channel> Sut => new ChannelClueProducer(ClueFactory.Object);
        protected override EntityType ExpectedEntityType { get; } = EntityType.Channel;
    }
}
