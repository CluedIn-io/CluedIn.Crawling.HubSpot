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
        public const string CrawlerDescription = "HubSpot is a ... to be completed ..."; // complete the crawler description
        public const string CrawlerDisplayName = "HubSpot";  // RJ - this field is never used can it be removed ?
        public const string Uri = "http://www.sampleurl.com";
        public const string ApiBaseUri = "https://api.hubapi.com";


        public static readonly Guid ProviderId = Guid.Parse("7673C455-074A-499F-AFD3-C77DFBB7BC1F");   
        public const string ProviderName = "HubSpot";         
        public const bool SupportsConfiguration = true;             
        public const bool SupportsWebHooks = false;             
        public const bool SupportsAutomaticWebhookCreation = true;             
        public const bool RequiresAppInstall = false;            
        public const string AppInstallUrl = null;             
        public const string ReAuthEndpoint = null;             
        public const string IconUri = "https://s3-eu-west-1.amazonaws.com/staticcluedin/bitbucket.png"; 

        public static readonly ComponentEmailDetails ComponentEmailDetails = new ComponentEmailDetails {
            Features = new Dictionary<string, string>
                {
                                       { "Tracking",        "Expenses and Invoices against customers" },
                                       { "Intelligence",    "Aggregate types of invoices and expenses against customers and companies." }
                                   },
            Icon = new Uri(IconUri),
            ProviderName = ProviderName,
            ProviderId = ProviderId,
            Webhooks = SupportsWebHooks
        };

        public static IProviderMetadata CreateProviderMetadata()
        {
            return new ProviderMetadata {
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
