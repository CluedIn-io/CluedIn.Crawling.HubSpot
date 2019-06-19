using System.Linq;
using CrawlerIntegrationTesting.Clues;
using Xunit;
using Xunit.Abstractions;

namespace Crawling.HubSpot.Integration.Test
{
    public class DataIngestion : IClassFixture<HubSpotTestFixture>
    {
        private readonly HubSpotTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public DataIngestion(HubSpotTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;

            OutputClueOriginEntityCodes();
        }

        [Theory(Skip = "The number entity types in incorrect at present while test instance of HubSpot is being populated")]
        [InlineData("/Provider/Root", 1)]
        [InlineData("/Organization", 48)]
        [InlineData("/Infrastructure/Contact", 46)]
        [InlineData("/Calendar/Meeting", 2)]
        [InlineData("/Note", 5)]
        [InlineData("/PhoneCall", 2)]
        [InlineData("/Task", 2)]
        [InlineData("/Process", 1)]
        [InlineData("/Sales/Deal", 3)]
        [InlineData("/Person", 1)]
        [InlineData("/Activity", 20)]
        public void CorrectNumberOfEntityTypes(string entityType, int expectedCount)
        {
            var foundCount = _fixture.ClueStorage.CountOfType(entityType);
            Assert.Equal(expectedCount, foundCount);
        }

        [Fact]
        public void EntityCodesAreUnique()
        {
            Assert.Equal(
                _fixture.ClueStorage.Clues.Distinct(new ClueComparer()).Count(),
                _fixture.ClueStorage.Clues.Count());
        }

        private void OutputClueOriginEntityCodes()
        {
            foreach(var clue in _fixture.ClueStorage.Clues)
            {
                _output.WriteLine(clue.OriginEntityCode.ToString());
            }
        }
    }
}
