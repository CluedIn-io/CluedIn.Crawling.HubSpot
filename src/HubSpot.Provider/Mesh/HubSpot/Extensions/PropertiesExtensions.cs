using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using CluedIn.Core.Mesh;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions
{
    public static class PropertiesExtensions
    {
        public static HubSpotProperties ToHubSpotProperties(this Properties properties, string prefix)
        {
            var hubspotProperties = new HubSpotProperties();
            if (properties?.properties == null || prefix.IsNullOrEmpty())
            {
                return null;
            }

            foreach (var property in properties.properties.Where(property => property.name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
            {
                hubspotProperties.AddIfNotExists(
                    new HubSpotProperty(property.name, property.value, prefix));
            }

            return hubspotProperties;
        }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class HubSpotProperties
    {
        public List<HubSpotProperty> Properties { get; set; }

        public HubSpotProperties()
        {
            Properties = new List<HubSpotProperty>();
        }

        public void AddIfNotExists(HubSpotProperty hubspotProperty)
        {
            if (Properties.Any(p => p.Property == hubspotProperty.Property))
            {
                return;
            }

            Properties.Add(hubspotProperty);
        }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class HubSpotProperty
    {
        public HubSpotProperty(string name, string value, string prefix)
        {
            this.Property = ResolveProperty(name, prefix);
            this.Value = value;
        }

        public string Property { get; }

        public string Value { get; }

        private static string ResolveProperty(string name, string prefix)
        {
            var returnValue = string.IsNullOrEmpty(name) ? string.Empty : name.ToLower();
            var normalizedPrefix = string.IsNullOrEmpty(prefix) ? string.Empty : prefix.ToLower();

            if (returnValue.StartsWith(normalizedPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                returnValue = returnValue.Remove(0, normalizedPrefix.Length);
            }

            return returnValue.Replace("companyname", "company");
        }
    }
}
