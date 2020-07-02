using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Agent.Jobs;
using CluedIn.Core.Configuration;
using CluedIn.Core.Data;
using CluedIn.Core.DataStore;
using CluedIn.Core.Messages.Processing;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using CluedIn.Crawling.HubSpot.Core;
using CrawlerIntegrationTesting.CrawlerHost;
using Microsoft.Extensions.Logging;

namespace CluedIn.Provider.HubSpot.WebHooks
{
    public class HubSpotWebhookProcessor : BaseWebhookProcessor
    {
        public HubSpotWebhookProcessor(ApplicationContext appContext)
            : base(appContext)
        {
        }

        public override bool Accept(IWebhookDefinition webhookDefinition)
        {
            return webhookDefinition.ProviderId == HubSpotConstants.ProviderId || base.Accept(webhookDefinition);
        }

        public override IEnumerable<Clue> DoProcess(ExecutionContext context, WebhookDataCommand command)
        {
            try
            {
                if (ConfigurationManagerEx.AppSettings.GetFlag("Feature.Webhooks.Log.Posts", false))
                    context.Log.LogDebug(command.HttpPostData);

                var configurationDataStore = context.ApplicationContext.Container.Resolve<IConfigurationRepository>();
                if (command.WebhookDefinition.ProviderDefinitionId != null)
                {
                    var providerDefinition = context.Organization.Providers.GetProviderDefinition(context, command.WebhookDefinition.ProviderDefinitionId.Value);
                    var jobDataCheck       = context.ApplicationContext.Container.ResolveAll<IProvider>().FirstOrDefault(providerInstance => providerDefinition != null && providerInstance.Id == providerDefinition.ProviderId);
                    var configStoreData    = configurationDataStore.GetConfigurationById(context, command.WebhookDefinition.ProviderDefinitionId.Value);  

                    // If you have stopped the provider then don't process the webhooks
                    if (providerDefinition?.WebHooks != null)
                        if (providerDefinition.WebHooks == false || providerDefinition.IsEnabled == false)
                            return new List<Clue>();

                    if (jobDataCheck != null)
                    {
                        var crawlJobData = new HubSpotCrawlJobData(configStoreData);

                        var clues = new List<Clue>();

                        IAgentJobProcessorArguments jobArgs = new DebugAgentJobProcessorArguments
                        {
                            TaskScheduler = TaskScheduler.Default,
                            Job = new AgentJob(Guid.NewGuid(), AgentJobPriority.Normal, "CluedIn" + HubSpotConstants.ProviderName, ProcessingRestriction.Any, null, null)
                        };

                        var processorState = new AgentJobProcessorState<HubSpotCrawlJobData>(jobArgs, AppContext)
                        {
                            JobData = crawlJobData,
                            Status = new AgentJobStatus { Statistics = new AgentJobStatusStatistics() }
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                using (context.Log.BeginScope(new { command.HttpHeaders, command.HttpQueryString, command.HttpPostData, command.WebhookDefinitionId })) {
                    context.Log.LogError(exception, "Could not process web hook message");
                }                
            }

            return new List<Clue>();
        }
    }
}
