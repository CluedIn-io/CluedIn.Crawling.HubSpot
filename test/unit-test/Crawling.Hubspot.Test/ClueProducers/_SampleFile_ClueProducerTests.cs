using System;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Crawling;
using CluedIn.Crawling.Hubspot.ClueProducers;
using CluedIn.Crawling.Hubspot.Core.Models;
using Xunit;

namespace Crawling.Hubspot.Test.ClueProducers
{
    public class _SampleFile_ClueProducerTests : BaseClueProducerTest<_SampleFile_>
    {
        protected override BaseClueProducer<_SampleFile_> Sut =>
            new _SampleFile_ClueProducer(_clueFactory.Object);

        protected override EntityType ExpectedEntityType => EntityType.Files.File;

        [Theory]
        [InlineAutoData]
        public void ClueHasEdgeToFolder(_SampleFile_ samplefile)
        {
            var clue = Sut.MakeClue(samplefile, Guid.NewGuid());
            _clueFactory.Verify(
                f => f.CreateOutgoingEntityReference(
                    clue, EntityType.Files.Directory,
                    EntityEdgeType.PartOf,
                    samplefile.FolderId.ToString(),
                    samplefile.FolderId.ToString())
                );
        }
    }
}
