using System;
using System.Collections.Generic;
using CluedIn.Core.Net.Mail;
using CluedIn.Core.Providers;

namespace CluedIn.Crawling.HubSpot.Core
{
    public class HubSpotConstants
    {
        public struct KeyName
        {
            public const string BaseUri = "baseUri";
            public const string ApiToken = "apiToken";
            public const string CustomerSubDomain = "customerSubDomain";
            public const string LastCrawlFinishTime = "lastCrawlFinishTime";  // Only used in Integration tests
            public const string HubSpotLoginUri = "hubSpotLoginUri";
        }

        public const string User = "User";
        public const string Group = "Group";
        public const string Network = "Network";
        public const string Message = "Message";
        public const string Attachment = "Attachment";
        public const string Thread = "Thread";

        public const string CodeOrigin = "HubSpot";
        public const string ProviderRootCodeValue = "HubSpot";
        public const string CrawlerName = "HubSpotCrawler";
        public const string CrawlerComponentName = "HubSpotCrawler";
        public const string CrawlerDescription = "HubSpot is all-in-one software for inbound marketing, CRM and Sales.";
        public const string CrawlerDisplayName = "HubSpot CRM";
        public const string Uri = "http://www.hubspot.com";
        public const string ApiBaseUri = "https://api.hubapi.com";


        public static readonly Guid ProviderId = Guid.Parse("7673C455-074A-499F-AFD3-C77DFBB7BC1F");
        public const string ProviderName = "HubSpot";
        public const bool SupportsConfiguration = true;
        public const bool SupportsWebHooks = false;
        public const bool SupportsAutomaticWebhookCreation = true;
        public const bool RequiresAppInstall = false;
        public const string AppInstallUrl = null;
        public const string ReAuthEndpoint = null;

        // The below are used by IExtendedProviderMetaData implementation to replace info from provider.js
        public static AuthMethods AuthMethodsJson = new AuthMethods
        {
            token = new Control[]
            {
                new Control
                {
                    name = "apiToken",
                    displayName = "Api Token",
                    type = "input",
                    isRequired = true
                }
            }
        };
        public static IList<string> ServiceTypeJson = new List<string> { "CRMType" };
        public static IList<string> Aliases = new List<string> { "HubSpot", "HubSpot CRM" };
        public const string IconResourceName = "Resources.hubspot.png";
        public const string Instructions = "You will need to login in your HubSpot account and get the Api Token and paste it in the input.";
        public const IntegrationType Type = IntegrationType.Cloud;
        public const string Category = "Contacts";
        public const string Details = "Our HubSpot provider will allow you to search across all your crm data.";

        public static readonly ComponentEmailDetails ComponentEmailDetails = new ComponentEmailDetails
        {
            Features = new Dictionary<string, string>
                {
                                       { "Tracking",        "Expenses and Invoices against customers" },
                                       { "Intelligence",    "Aggregate types of invoices and expenses against customers and companies." }
                                   },
            Icon = ProviderIconFactory.CreateUri(ProviderId),
            ProviderName = ProviderName,
            ProviderId = ProviderId,
            Webhooks = SupportsWebHooks
        };



        public static IProviderMetadata CreateProviderMetadata()
        {
            return new ProviderMetadata
            {
                Id = ProviderId,
                ComponentName = CrawlerName,
                Name = ProviderName,
                Type = "Cloud",
                SupportsConfiguration = SupportsConfiguration,
                SupportsWebHooks = SupportsWebHooks,
                SupportsAutomaticWebhookCreation = SupportsAutomaticWebhookCreation,
                RequiresAppInstall = RequiresAppInstall,
                AppInstallUrl = AppInstallUrl,
                ReAuthEndpoint = ReAuthEndpoint,
                ComponentEmailDetails = ComponentEmailDetails
            };
        }
    }
}
