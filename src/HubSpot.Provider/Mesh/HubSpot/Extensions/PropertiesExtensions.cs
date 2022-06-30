﻿using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core.Mesh;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions
{
    public static class PropertiesExtensions
    {
        public static HubspotProperties ToHubspotProperties(this Properties properties)
        {
            var hubspotProperties = new HubspotProperties();
            foreach (var property in properties.properties.Where(property => property.name.Contains("hubspot.contact", StringComparison.OrdinalIgnoreCase)))
            {
                hubspotProperties.TryAdd(
                    new HubspotProperty(property.name, property.value));
            }

            return hubspotProperties;
        }
    }

    public class HubspotProperties
    {
        public List<HubspotProperty> properties { get; set; }

        public HubspotProperties()
        {
            properties = new List<HubspotProperty>();
        }

        public void TryAdd(HubspotProperty hubspotProperty)
        {
            if (properties.Any(p => p.property == hubspotProperty.property))
            {
                return;
            }

            properties.Add(hubspotProperty);
        }
    }

    public class HubspotProperty
    {
        public HubspotProperty(string property, string value)
        {
            this.property = property.ToLower().Replace("hubspot.contact.", "");
            this.value = value;
        }

        public string property { get; set; }
        public string value { get; set; }
    }
}
