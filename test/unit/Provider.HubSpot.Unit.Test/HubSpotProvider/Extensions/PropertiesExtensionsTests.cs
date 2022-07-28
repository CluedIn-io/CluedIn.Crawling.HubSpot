using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Mesh;
using CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions;
using Newtonsoft.Json;
using RestSharp;
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
            var result = properties.ToHubSpotProperties("hubspot.contact.");

            // assert
            Assert.Null(result);
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
            var result = properties.ToHubSpotProperties("hubspot.contact.");

            // assert
            Assert.Empty(result.Properties);
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
            var result = properties.ToHubSpotProperties(string.Empty);

            // assert
            Assert.Null(result);
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
            var result = properties.ToHubSpotProperties("hubspot.contact.");

            // assert
            Assert.NotNull(result);
            Assert.Single(result.Properties);
            Assert.Equal("firstname", result.Properties.First().Property);
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
            var result = properties.ToHubSpotProperties("hubspot.contact.");

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Properties.Count());
            Assert.Equal("firstname", result.Properties.First().Property);
            Assert.Equal("lastname", result.Properties.Skip(1).First().Property);
        }

        [Fact]
        internal void ToHubspotProperties_ValidKeys_ShouldBeCamelCase()
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
            var result = properties.ToHubSpotProperties("hubspot.contact.");

            var json = JsonConvert.SerializeObject(result);

            // assert
            Assert.Equal("{\"properties\":[{\"property\":\"firstname\",\"value\":\"test\"},{\"property\":\"lastname\",\"value\":\"mctest\"}]}", json);
        }
    }
}
