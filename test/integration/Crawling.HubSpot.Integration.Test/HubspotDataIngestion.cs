using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CluedIn.Core.Data;
using CrawlerIntegrationTesting.Clues;
using Xunit;
using Xunit.Abstractions;

namespace Crawling.HubSpot.Integration.Test
{
    [Trait("Category", "web")]
    public class DataIngestion : IClassFixture<HubSpotTestFixture>
    {
        private readonly HubSpotTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public DataIngestion(HubSpotTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;

            OutputClueOriginEntityCodes();
            //DumpCluesToDisk();
        }

        [Theory]
        [InlineData("/Provider/Root", 1)]
        [InlineData("/Organization", 6)]
        [InlineData("/Infrastructure/Contact", 18)]
        // [InlineData("/Calendar/Meeting", 2)] Setup in HubSpot
        // [InlineData("/Calendar/Event", 2)] Setup in HubSpot
        //[InlineData("/Note", 8)]
        [InlineData("/PhoneCall", 3)]
        [InlineData("/Task", 4)]
        //[InlineData("/Sales/Deal", 6)]
        [InlineData("/Activity", 8)]
        //[InlineData("/News", 6)]
        //[InlineData("/Template", 120)]
        //[InlineData("/Process", 2)]
        //[InlineData("/Topic", 1)]
        [InlineData("/Mail", 5)]
        [InlineData("/Form", 1)]
        [InlineData("/Person", 5)]
        //[InlineData("/Channel", 1)]
        //[InlineData("/Announcement", 1)]
        [InlineData("/Product", 1)]
        [InlineData("/List", 2)]
        [InlineData("/Files/File", 11)]
        [InlineData("/Support/Ticket", 1)]
        public void CorrectNumberOfEntityTypes(string entityType, int expectedCount)
        {
            var foundCount = _fixture.ClueStorage.CountOfType(entityType);
            Assert.Equal(expectedCount, foundCount);
        }

        [Fact(Skip = "This will never work because some entities are duplicated when pulled in via association - ie the same /Infrastructure/Contact can be linked to multiple HubSpot types")]
        public void EntityCodesAreUnique()
        {
            Assert.Equal(
                _fixture.ClueStorage.Clues.Distinct(new ClueComparer()).Count(),
                _fixture.ClueStorage.Clues.Count());
        }

        private void OutputClueOriginEntityCodes()
        {
            foreach (var clue in _fixture.ClueStorage.Clues)
            {
                _output.WriteLine(clue.OriginEntityCode.ToString());
            }
        }

        private void DumpCluesToDisk()
        {
            var baseFolder = Directory.GetCurrentDirectory() + @"..\..\..\..\GeneratedClues\";
            var filePath = Directory.CreateDirectory(baseFolder).FullName;
            _output.WriteLine(filePath);

            foreach (var clue in _fixture.ClueStorage.Clues)
            {
                DebugDumpClueToDisk(clue, filePath);
            }
        }

        private void DebugDumpClueToDisk(Clue clue, string directory)
        {

            Func<string, string> replace = input =>
            {
                if (input == null)
                    return string.Empty;

                var chars = Path.GetInvalidFileNameChars().Union(Path.GetInvalidFileNameChars()).Union(new[] { '\\', '/' });

                var result = chars.Aggregate(input, (current, c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), "-"));

                if (result.Length > 50)
                    result = result.Substring(0, 50);

                return result;
            };

            try
            {
                var serialized = clue.Serialize();
                var file = directory +
                           replace(clue.Data.EntityData.EntityType.ToString()) +
                           " - " +
                           replace(clue.Data.EntityData.Name) +
                           " - " +
                           clue.Id +
                           ".clue.xml";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (File.Exists(file))
                    File.Delete(file);

                using (var writer = File.CreateText(file))
                    writer.Write(serialized);

            }
            catch (Exception ex)
            {
                _output.WriteLine("Could not dump clue to disk: " + ex.Message, ex);
            }

        }
    }
}
