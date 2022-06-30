using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CluedIn.Core.Mesh;
using CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions;
using RestSharp.Extensions;
using Shouldly;
using Xunit;

namespace CluedIn.Provider.HubSpot.Unit.Test.HubSpotProvider.Extensions
{
    public class PropertiesExtensionsTests
    {
        [Fact]
        internal void ToHubspotProperties_Null_ShouldBeEmpty()
        {
            // arrange
            var properties = new Core.Mesh.Properties();

            // act
            var result = properties.ToHubspotProperties("hubspot.contact.");

            // assert
            Assert.Empty(result.properties);
        }

        [Fact]
        internal void ToHubspotProperties_MissingKeys_ShouldBeEmpty()
        {
            // arrange
            var properties = new Core.Mesh.Properties();
            var internalProperties = new List<Property>
            {
                new Property { name = "acceptance.contact.firstName", value = "test" },
                new Property { name = "salesforce.contact.firstName", value = "test" },
                new Property { name = "d365.contact.firstName", value = "test" }
            };
            properties.properties = internalProperties;

            // act
            var result = properties.ToHubspotProperties("hubspot.contact.");

            // assert
            Assert.Empty(result.properties);
        }

        [Fact]
        internal void ToHubspotProperties_InvalidPrefix_ShouldBeEmpty()
        {
            // arrange
            var properties = new Core.Mesh.Properties();
            var internalProperties = new List<Property>
            {
                new Property { name = "acceptance.contact.firstName", value = "test" },
                new Property { name = "salesforce.contact.firstName", value = "test" },
                new Property { name = "hubspot.contact.firstName", value = "test" }
            };
            properties.properties = internalProperties;

            // act
            var result = properties.ToHubspotProperties(string.Empty);

            // assert
            Assert.Empty(result.properties);
        }

        [Fact]
        internal void ToHubspotProperties_ValidKeys_ShouldHaveOne()
        {
            // arrange
            var properties = new Core.Mesh.Properties();
            var internalProperties = new List<Property>
            {
                new Property { name = "acceptance.contact.firstName", value = "test" },
                new Property { name = "salesforce.contact.firstName", value = "test" },
                new Property { name = "hubspot.contact.firstName", value = "test" }
            };
            properties.properties = internalProperties;

            // act
            var result = properties.ToHubspotProperties("hubspot.contact.");

            // assert
            Assert.NotNull(result);
            Assert.Single(result.properties);
            Assert.Equal("firstname", result.properties.First().name);
        }

        [Fact]
        internal void ToHubspotProperties_ValidKeys_ShouldHaveTwo()
        {
            // arrange
            var properties = new Core.Mesh.Properties();
            var internalProperties = new List<Property>
            {
                new Property { name = "acceptance.contact.firstName", value = "test" },
                new Property { name = "salesforce.contact.firstName", value = "test" },
                new Property { name = "hubspot.contact.firstName", value = "test" },
                new Property { name = "hubspot.contact.lastNAME", value = "mctest" }
            };
            properties.properties = internalProperties;

            // act
            var result = properties.ToHubspotProperties("hubspot.contact.");

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.properties.Count());
            Assert.Equal("firstname", result.properties.First().name);
            Assert.Equal("lastname", result.properties.Skip(1).First().name);
        }
    }
}
