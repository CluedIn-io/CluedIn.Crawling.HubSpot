using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Globalization;
using System.Linq;
using CluedIn.Core.Configuration;
using CluedIn.Core.Messages.WebApp;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Providers.Models;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;
using Microsoft.Extensions.Logging;

namespace CluedIn.Provider.HubSpot
{
    public class HubSpotProvider : ProviderBase, IExtendedProviderMetadata
    {
        private readonly IHubSpotClientFactory _hubspotClientFactory;
        private readonly ILogger<HubSpotProvider> _log;
        private readonly ISystemNotifications _notifications;

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        public HubSpotProvider([NotNull] ApplicationContext appContext, IHubSpotClientFactory hubspotClientFactory, ILogger<HubSpotProvider> log, ISystemNotifications notifications)
            : base(appContext, HubSpotConstants.CreateProviderMetadata())
        {
            _hubspotClientFactory = hubspotClientFactory ?? throw new ArgumentNullException(nameof(hubspotClientFactory));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _notifications = notifications;
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        public override async Task<CrawlJobData> GetCrawlJobData(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var hubspotCrawlJobData = new HubSpotCrawlJobData(configuration);

            return await Task.FromResult(hubspotCrawlJobData);
        }

        public override async Task<bool> TestAuthentication(
            ProviderUpdateContext context,
            IDictionary<string, object> configuration,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            var hubspotCrawlJobData = new HubSpotCrawlJobData(configuration);

            try
            {
                var client = _hubspotClientFactory.CreateNew(hubspotCrawlJobData);
                var result = await client.GetContactsFromAllListsAsync(new List<string>(), 1, 0);
                return result != null;
            }
            catch (Exception exception)
            {
                _log.LogWarning(exception, "Could not add HubSpot provider");
                return false;
            }
        }

        public override async Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            return await Task.FromResult(default(ExpectedStatistics));
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            [NotNull] CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));


            if (jobData is HubSpotCrawlJobData hubspotCrawlJobData)
            {
                if (_notifications != null) _notifications.PublishCommand(new ProviderMessageCommand() { OrganizationId = organizationId, ProviderDefinitionId = providerDefinitionId, ProviderId = this.Id, ProviderName = this.Name, Message = "Authenticating", UserId = userId });

                var result = hubspotCrawlJobData.ToDictionary();

                result.Add("webhooks", new List<object>()
                {
                    new { DisplayName = "New Contact", Name = "contact.creation", Status = "ACTIVE", Description = "When a new contact is created."},
                    new { DisplayName = "Deleted Contact", Name = "contact.deletion", Status = "ACTIVE", Description = "When a contact is deleted."},
                    new { DisplayName = "Contact Update", Name = "contact.propertyChange", Status = "ACTIVE", Description = "When a contact is updated."},
                    new { DisplayName = "New Company", Name = "company.creation", Status = "ACTIVE", Description = "When a company is created."},
                    new { DisplayName = "Deleted Company", Name = "company.deletion", Status = "ACTIVE", Description = "When a company is deleted."},
                    new { DisplayName = "Company Update", Name = "company.propertyChange", Status = "ACTIVE", Description = "When a company is updated."},
                    new { DisplayName = "New Deal", Name = "deal.creation", Status = "ACTIVE", Description = "When a deal is created."},
                    new { DisplayName = "Deleted Deal", Name = "deal.deletion", Status = "ACTIVE", Description = "When a deal is deleted"},
                    new { DisplayName = "Deal Update", Name = "deal.propertyChange", Status = "ACTIVE", Description = "When a deal is updated."}
                });

                result.Add("expectedStatistics", hubspotCrawlJobData.ExpectedStatistics);

                return await Task.FromResult(result);
            }
            throw new InvalidOperationException($"Unpexected data type for HubSpotJobCrawlData, {jobData.GetType()}");
        }

        public override async Task<IDictionary<string, object>> GetHelperConfiguration(
            ProviderUpdateContext context,
            CrawlJobData jobData,
            Guid organizationId,
            Guid userId,
            Guid providerDefinitionId,
            string folderId)
        {
            return await GetHelperConfiguration(context, jobData, organizationId, userId, providerDefinitionId);
        }

        public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            var hubspotCrawlJobData = jobData as HubSpotCrawlJobData;

            if (hubspotCrawlJobData == null)
            {
                throw new Exception("Wrong CrawlJobData type");
            }

            try
            {
                var client = _hubspotClientFactory.CreateNew(hubspotCrawlJobData);
                var result = await client.GetAccountInformation();
                if (result != null)
                {
                    var portalId = result.First().portalId;
                    return new AccountInformation(portalId.ToString(CultureInfo.InvariantCulture), portalId.ToString(CultureInfo.InvariantCulture));
                }
                return new AccountInformation(string.Empty, string.Empty) { Errors = new Dictionary<string, string>() { { "error", "Please contact CluedIn support in the top menu to help you setup with HubSpot." } } };

            }
            catch (Exception e)
            {
                _log.LogError(e, "There was an error getting HubSpot account information {message}", e.Message);
                return new AccountInformation(string.Empty, string.Empty) { Errors = new Dictionary<string, string>() { { "error", "Please contact CluedIn support in the top menu to help you setup with HubSpot." }, {"exception", e.Message } }};
            }
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            return webHooksEnabled && ConfigurationManagerEx.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
                : $"{relativeDateTime.Minute} 0/4 * * *";
        }

        public override async Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var hubSpotCrawlJobData = (HubSpotCrawlJobData)jobData;
            var webhookSignatures = new List<WebHookSignature>();
            try
            {
                var client = _hubspotClientFactory.CreateNew(hubSpotCrawlJobData);

                var data = await client.GetWebHooks();

                if (data == null)
                    return webhookSignatures;

                var hookTypes = new[] { "contact.creation", "contact.deletion", "contact.propertyChange", "company.creation", "company.deletion", "company.propertyChange", "deal.creation", "deal.deletion", "deal.propertyChange" };

                foreach (var subscription in hookTypes)
                {
                    if (config.ContainsKey("webhooks"))
                    {
                        var enabledHooks = (List<Webhook>)config["webhooks"];
                        var enabled = enabledHooks.Where(s => s.Status == "ACTIVE").Select(s => s.Name);
                        if (!enabled.Contains(subscription))
                        {
                            continue;
                        }
                    }

                    try
                    {
                        await client.CreateWebHook(subscription);
                        webhookSignatures.Add(new WebHookSignature { Signature = webhookDefinition.ProviderDefinitionId.ToString(), ExternalVersion = "v1", ExternalId = null, EventTypes = "contact.creation,contact.deletion,contact.propertyChange,company.creation,company.deletion,company.propertyChange,deal.creation,deal.deletion,deal.propertyChange" });
                    }
                    catch (Exception exception)
                    {
                        _log.LogWarning(exception, "Could not create HubSpot Webhook for subscription: {subscription}", subscription);
                    }
                }

                webhookDefinition.Uri = new Uri(this.appContext.System.Configuration.WebhookReturnUrl.Trim('/') + ConfigurationManagerEx.AppSettings["Providers.HubSpot.WebhookEndpoint"]);

                webhookDefinition.Verified = true;
            }
            catch (Exception exception)
            {
                _log.LogWarning(exception, "Could not create HubSpot Webhook");
            }

            var organizationProviderDataStore = context.Organization.DataStores.GetDataStore<ProviderDefinition>();
            if (organizationProviderDataStore != null)
            {
                if (webhookDefinition.ProviderDefinitionId != null)
                {
                    var webhookEnabled = organizationProviderDataStore.GetById(context, webhookDefinition.ProviderDefinitionId.Value);
                    if (webhookEnabled != null)
                    {
                        webhookEnabled.WebHooks = true;
                        organizationProviderDataStore.Update(context, webhookEnabled);
                    }
                }
            }

            return webhookSignatures;
        }

        public override async Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
        {
            var webhookDefinitionDataStore = context.Organization.DataStores.GetDataStore<WebhookDefinition>();
            return await webhookDefinitionDataStore.SelectAsync(context, s => s.Verified != null && s.Verified.Value);
        }

        public override async Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));

            await Task.Run(() =>
            {
                var webhookDefinitionProviderDataStore = context.Organization.DataStores.GetDataStore<WebhookDefinition>();
                if (webhookDefinitionProviderDataStore != null)
                {
                    var webhook = webhookDefinitionProviderDataStore.GetById(context, webhookDefinition.Id);
                    if (webhook != null)
                    {
                        webhookDefinitionProviderDataStore.Delete(context, webhook);
                    }
                }

                var organizationProviderDataStore = context.Organization.DataStores.GetDataStore<ProviderDefinition>();
                if (organizationProviderDataStore != null)
                {
                    if (webhookDefinition.ProviderDefinitionId != null)
                    {
                        var webhookEnabled = organizationProviderDataStore.GetById(context, webhookDefinition.ProviderDefinitionId.Value);
                        if (webhookEnabled != null)
                        {
                            webhookEnabled.WebHooks = false;
                            organizationProviderDataStore.Update(context, webhookEnabled);
                        }
                    }
                }
            });
        }

        public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
        {
            var endpoints = new List<string> { "https://hooks.cluedin.net/manage/hubspot/hooks" };
            return endpoints;
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }

        public string Icon => HubSpotConstants.IconResourceName;
        public string Domain => HubSpotConstants.Uri;
        public string About => HubSpotConstants.CrawlerDescription;
        public AuthMethods AuthMethods => HubSpotConstants.AuthMethodsJson;
        public IEnumerable<Control> Properties => null;
        public string ServiceType => JsonConvert.SerializeObject(HubSpotConstants.ServiceTypeJson);
        public string Aliases => JsonConvert.SerializeObject(HubSpotConstants.Aliases);
        public Guide Guide => new Guide
        {
            Instructions = HubSpotConstants.Instructions,
            Value = new List<string> { HubSpotConstants.CrawlerDescription },
                Details = HubSpotConstants.Details

        };

        public string Details => HubSpotConstants.Details;
        public string Category => HubSpotConstants.Category;
        public new IntegrationType Type => HubSpotConstants.Type;





    }
}
