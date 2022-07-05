using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using CluedIn.Core.Mesh;

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

    public class HubSpotProperties
    {
        public List<HubSpotProperty> properties { get; set; }

        public HubSpotProperties()
        {
            properties = new List<HubSpotProperty>();
        }

        public void AddIfNotExists(HubSpotProperty hubspotProperty)
        {
            if (properties.Any(p => p.property == hubspotProperty.property))
            {
                return;
            }

            properties.Add(hubspotProperty);
        }
    }

    public class HubSpotProperty
    {
        private readonly string _name;
        private readonly string _prefix;

        public HubSpotProperty(string name, string value, string prefix)
        {
            _name = string.IsNullOrEmpty(name) ? string.Empty : name.ToLower();
            _prefix = string.IsNullOrEmpty(prefix) ? string.Empty : prefix.ToLower();

            this.value = value;
        }

        public string property =>
            _name.Replace(_prefix, string.Empty)
                .Replace("companyname", "company");

        public string value { get; }
    }
}
