using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Castle.Core.Internal;
using CluedIn.Core.Mesh;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions
{
    public static class PropertiesExtensions
    {
        public static HubSpotProperties ToHubSpotProperties(this Properties properties, string prefix)
        {
            var hubspotProperties = new HubSpotProperties();
            if(properties?.properties == null || prefix.IsNullOrEmpty())
            {
                return null;
            }

            foreach (var property in properties.properties.Where(property => property.name.Contains(prefix, StringComparison.OrdinalIgnoreCase)))
            {
                hubspotProperties.AddIfNotExists(
                    new HubSpotProperty(property.name, property.value, prefix));
            }

            return hubspotProperties;
        }
    }

    public class HubSpotProperties
    {
        public List<HubSpotProperty> properties { get; set; }

        public HubSpotProperties()
        {
            properties = new List<HubSpotProperty>();
        }

        public void AddIfNotExists(HubSpotProperty hubspotProperty)
        {
            if (properties.Any(p => p.Property == hubspotProperty.Property))
            {
                return;
            }

            properties.Add(hubspotProperty);
        }
    }

    public class HubSpotProperty
    {
        
        private string? Name { get; set; }
        private string? Prefix { get; set; }

        public HubSpotProperty(string name, string value, string prefix)
        {
            Name = name;
            Value = value;
            Prefix = prefix;
        }

        [JsonPropertyName("property")]
        public string Property
        {
            get
            {
                if(string.IsNullOrEmpty(Prefix)) Prefix = string.Empty;

                return string.IsNullOrEmpty(Name) ? string.Empty : Name.Replace(Prefix, string.Empty)
                    .ToLower()
                    .Replace("companyname", "company");
            }
        }

        [JsonPropertyName("value")]
        public string Value { get; }
    }
}
