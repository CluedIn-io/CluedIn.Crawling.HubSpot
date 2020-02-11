using System;
using System.Collections.Generic;
using System.Configuration;
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
using CluedIn.Crawling;
using CluedIn.Crawling.HubSpot.Core;
using CrawlerIntegrationTesting.CrawlerHost;

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
                if (ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Log.Posts", false))
                    context.Log.Debug(() => command.HttpPostData);

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
                context.Log.Error(new { command.HttpHeaders, command.HttpQueryString, command.HttpPostData, command.WebhookDefinitionId }, () => "Could not process web hook message", exception);
            }

            return new List<Clue>();
        }
    }
}
