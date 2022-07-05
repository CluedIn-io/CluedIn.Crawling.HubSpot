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
        private readonly string _name;
        private readonly string _prefix;

        public HubSpotProperty(string name, string value, string prefix)
        {
            _name = string.IsNullOrEmpty(name) ? string.Empty : name.ToLower();
            _prefix = string.IsNullOrEmpty(prefix) ? string.Empty : prefix.ToLower();

            this.Value = value;
        }

        public string Property
        {
            get
            {
                var returnValue = _name;

                if (returnValue.StartsWith(_prefix, StringComparison.InvariantCultureIgnoreCase))
                {
                    returnValue = returnValue.Remove(0, _prefix.Length);
                }

                return returnValue.Replace("companyname", "company");
            }
        }

        public string Value { get; }
    }
}
