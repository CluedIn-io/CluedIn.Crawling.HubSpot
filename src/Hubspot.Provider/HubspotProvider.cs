using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Configuration;
using System.Globalization;
using System.Linq;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Infrastructure.Factories;
using CluedIn.Providers.Models;

namespace CluedIn.Provider.HubSpot
{
    public class HubSpotProvider : ProviderBase
    {
        private readonly IHubSpotClientFactory _hubspotClientFactory;

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        public HubSpotProvider([NotNull] ApplicationContext appContext, IHubSpotClientFactory hubspotClientFactory)
            : base(appContext, HubSpotConstants.CreateProviderMetadata())
        {
            _hubspotClientFactory = hubspotClientFactory;
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
                context.Log.Warn(() => "Could not add HubSpot provider", exception);
                return false;
            }
        }

        public override Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            throw new NotImplementedException();
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
                //TODO add the transformations from specific CrawlJobData object to dictionary
                // add tests to GetHelperConfigurationBehaviour.cs
                var result = hubspotCrawlJobData.ToDictionary();

                //result.Add("webhooks", new List<WebhookEventType>()
                //{
                //    new WebhookEventType() { DisplayName = "New Contact",       Name = "contact.creation",     Status = "ACTIVE", Description = "When a new contact is created."},
                //    new WebhookEventType() { DisplayName = "Deleted Contact",       Name = "contact.deletion",     Status = "ACTIVE", Description = "When a contact is deleted."},
                //    new WebhookEventType() { DisplayName = "Contact Update",       Name = "contact.propertyChange",     Status = "ACTIVE", Description = "When a contact is updated."},
                //    new WebhookEventType() { DisplayName = "New Company",       Name = "company.creation",     Status = "ACTIVE", Description = "When a company is created."},
                //    new WebhookEventType() { DisplayName = "Deleted Company",       Name = "company.deletion",     Status = "ACTIVE", Description = "When a company is deleted."},
                //    new WebhookEventType() { DisplayName = "Company Update",       Name = "company.propertyChange",     Status = "ACTIVE", Description = "When a company is updated."},
                //    new WebhookEventType() { DisplayName = "New Deal",       Name = "deal.creation",     Status = "ACTIVE", Description = "When a deal is created."},
                //    new WebhookEventType() { DisplayName = "Deleted Deal",       Name = "deal.deletion",     Status = "ACTIVE", Description = "When a deal is deleted"},
                //    new WebhookEventType() { DisplayName = "Deal Update",       Name = "deal.propertyChange",     Status = "ACTIVE", Description = "When a deal is updated."}
                //});

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
                var portalId = 0;
                if (result != null)
                {
                    portalId = result.First().portalId;
                }

                if (portalId == 0)
                {
                    return new AccountInformation(string.Empty, string.Empty) { Errors = new Dictionary<string, string>() { { "error", "Please contact CluedIn support in the top menu to help you setup with Hubspot." } } };
                }

                return new AccountInformation(portalId.ToString(CultureInfo.InvariantCulture), portalId.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception)
            {
                return new AccountInformation(string.Empty, string.Empty) { Errors = new Dictionary<string, string>() { { "error", "Please contact CluedIn support in the top menu to help you setup with Hubspot." } } };
            }
        }

        public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
        {
            //TODO is this common for all providers?
            return webHooksEnabled && ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
                : $"{relativeDateTime.Minute} 0/4 * * *";
        }

        public override Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));
            if (webhookDefinition == null)
                throw new ArgumentNullException(nameof(webhookDefinition));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            throw new NotImplementedException();
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
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            // TODO should ids also be checked for being empty ?

            var endpoints = new List<string> { "https://hooks.cluedin.net/manage/hubspot/hooks" };
            return endpoints;
        }

        public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
        {
            if (jobData == null)
                throw new ArgumentNullException(nameof(jobData));

            //TODO what the hell is this?
            //There is no limit set, so you can pull as often and as much as you want.
            return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
        }
    }
}
