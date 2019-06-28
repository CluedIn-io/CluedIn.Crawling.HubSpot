using System;
using AutoFixture.Xunit2;
using CluedIn.Core.Data;
using CluedIn.Core.Logging;
using CluedIn.Crawling;
using CluedIn.Crawling.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.HubSpot.Unit.Test.ClueProducers
{
    public abstract class BaseClueProducerTest<T>
    {
        protected readonly Mock<ILogger> Logger;
        protected readonly Mock<IClueFactory> ClueFactory;
        protected readonly Guid OrganizationId = Guid.NewGuid();

        protected abstract BaseClueProducer<T> Sut { get; }
        protected abstract EntityType ExpectedEntityType { get; }

        protected BaseClueProducerTest()
        {
            Logger = new Mock<ILogger>();
            ClueFactory = new Mock<IClueFactory>();
            var entityCode = new Mock<IEntityCode>();

            ClueFactory.Setup(f =>
                f.Create(It.IsAny<EntityType>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(new Clue(entityCode.Object, Guid.NewGuid()));
        }

        [Theory]
        [InlineAutoData]
        public void ClueHasId(T input) =>
            Sut.MakeClue(input, new Guid())
                .Id.ShouldNotBeSameAs(Guid.Empty);

        [Theory]
        [InlineAutoData]
        public void ClueHasName(T input) =>
            Sut.MakeClue(input, new Guid())
                .Data.EntityData.Name.ShouldNotBeEmpty();

        [Theory]
        [InlineAutoData]
        protected void ClueIsOfType(T input)
        {
            Sut.MakeClue(input, new Guid());
            ClueFactory.Verify(
                f => f.Create(ExpectedEntityType, It.IsAny<string>(), It.IsAny<Guid>())
                );
        }

    }
}

