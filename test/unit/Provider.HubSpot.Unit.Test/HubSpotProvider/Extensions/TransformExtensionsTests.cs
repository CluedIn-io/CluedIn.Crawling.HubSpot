using System.Collections.Generic;
using CluedIn.Core.Mesh;
using Xunit;
using CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions;
using Shouldly;

namespace CluedIn.Provider.HubSpot.Unit.Test.HubSpotProvider.Extensions
{
    public class TransformExtensionsTests
    {
        [Fact]
        internal void TransformExtensions_NullTransforms_ShouldBeFalse()
        {
            // arrange
            List<Transform> sut = null;

            // act
            var result = sut.HasValidVocabularyKey();

            // assert
            result.ShouldBeFalse();
        }

        [Fact]
        internal void TransformExtensions_EmptyTransforms_ShouldBeFalse()
        {
            // arrange
            var sut = new List<Transform>();

            // act
            var result = sut.HasValidVocabularyKey();

            // assert
            result.ShouldBeFalse();
        }

        [Fact]
        internal void TransformExtensions_MissingVocabTransform_ShouldBeFalse()
        {
            // arrange
            var sut = new List<Transform>()
            {
                new Transform() { key = "user.firstname" },
                new Transform() { key = "salesforce.user.firstname" },
                new Transform() { key = "acceptance.user.firstname" }
            };

            // act
            var result = sut.HasValidVocabularyKey();

            // assert
            result.ShouldBeFalse();
        }

        [Fact]
        internal void TransformExtensions_WithVocabTransform_ShouldBeFalse()
        {
            // arrange
            var sut = new List<Transform>()
            {
                new Transform() { key = "user.firstname" },
                new Transform() { key = "salesforce.user.firstname" },
                new Transform() { key = "hubspot.user.firstname" }
            };

            // act
            var result = sut.HasValidVocabularyKey();

            // assert
            result.ShouldBeTrue();
        }

        [Fact]
        internal void TransformExtensions_WithVocabTransformInVariantCase_ShouldBeFalse()
        {
            // arrange
            var sut = new List<Transform>()
            {
                new Transform() { key = "user.firstname" },
                new Transform() { key = "salesforce.user.firstname" },
                new Transform() { key = "hubSpot.user.firstname" }
            };

            // act
            var result = sut.HasValidVocabularyKey();

            // assert
            result.ShouldBeTrue();
        }
    }
}
