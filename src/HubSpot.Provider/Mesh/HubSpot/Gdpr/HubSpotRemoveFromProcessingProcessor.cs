using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Mesh;
using CluedIn.Core.Messages.Processing;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using RestSharp;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Gdpr
{
    public abstract class HubSpotRemoveFromProcessingProcessor : BaseRemoveFromProcessingProcessor
    {
        protected HubSpotRemoveFromProcessingProcessor(ApplicationContext appContext)
            : base(appContext)
        {
        }

        public EntityType[] EntityType { get; }
        public string DeleteUrl { get; }

        protected HubSpotRemoveFromProcessingProcessor(ApplicationContext appContext, string deleteUrl, params EntityType[] entityType)
            : base(appContext)
        {
            EntityType = entityType;
            DeleteUrl = deleteUrl;
        }

        public override bool Accept(RemoveFromProcessingDataCommand command, MeshQuery query, IEntity entity)
        {
            return command.ProviderId == this.GetProviderId() && query.Action == ActionType.DISASSOCIATE && EntityType.Contains(entity.EntityType);
        }

        // TODO: MeshQUery should be handed in
        //All of the stores should be in a base class and only the client parts are passed in.
        /// <summary>Does the process.</summary>
        /// <param name="context">The context.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override void DoProcess(CluedIn.Core.ExecutionContext context, RemoveFromProcessingDataCommand command, IDictionary<string, object> jobData, MeshQuery query)
        {
            return;
        }

        public override List<Core.Messages.WebApp.RawQuery> GetRawQueries(IDictionary<string, object> config, IEntity entity)
        {
            var queries = new List<Core.Messages.WebApp.RawQuery>();
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format("/automation/v2/workflows/enrollments/contacts/{0}", GetLookupId(entity)), Method.GET);

            client.AddDefaultHeader("Authorization", $"Bearer {hubSpotCrawlJobData.ApiToken}");
            client.AddDefaultHeader("Content-Type", "application/json");

            var result = client.ExecuteTaskAsync<List<Workflow>>(request).Result;
            if (result.Data == null)
            {
                return new List<Core.Messages.WebApp.RawQuery>();
            }

            if (!result.Data.Any())
            {
                return new List<Core.Messages.WebApp.RawQuery>();
            }

            if (entity.Properties[CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email] == null)
            {
                return new List<Core.Messages.WebApp.RawQuery>();
            }

            foreach (var workflow in result.Data)
            {
                queries.Add(new Core.Messages.WebApp.RawQuery()
                {
                    Query = $"curl -X DELETE https://api.hubapi.com/automation/v2/workflows/{workflow.id}/enrollments/contacts/{entity.Properties[CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email]} --header \"Bearer {hubSpotCrawlJobData.ApiToken}\" --header \"Content-Type: application/json\"",
                    Source = "cUrl"
                });
            }

            return queries;
        }

        public override Guid GetProviderId()
        {
            return Constants.Providers.HubSpotId;
        }

        public override string GetVocabularyProviderKey()
        {
            return "hubspot";
        }

        public override string GetLookupId(IEntity entity)
        {
            var code = entity.Codes.ToList().FirstOrDefault(d => d.Origin.Code == "HubSpot");
            long id;
            if (!long.TryParse(code.Value, out id))
            {
                //It does not match the id I need.
            }

            return code.Value;
        }

        public override List<QueryResponse> RunQueries(IDictionary<string, object> config, string id, IEntity entity)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);
            var quereis = new List<QueryResponse>();
            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format("/automation/v2/workflows/enrollments/contacts/{0}", id), Method.GET);

            client.AddDefaultHeader("Authorization", $"Bearer {hubSpotCrawlJobData.ApiToken}");
            client.AddDefaultHeader("Content-Type", "application/json");

            var result = client.ExecuteTaskAsync<List<Workflow>>(request).Result;
            if (result.Data == null)
            {
                return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
            }

            if (!result.Data.Any())
            {
                return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
            }

            if (entity.Properties[CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email] == null)
            {
                return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
            }

            foreach (var workflow in result.Data)
            {
                var removeRequest = new RestRequest(string.Format("/automation/v2/workflows/{0}/enrollments/contacts/{1}", workflow.id, entity.Properties[CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email]), Method.DELETE);

                client.AddDefaultHeader("Authorization", $"Bearer {hubSpotCrawlJobData.ApiToken}");
                client.AddDefaultHeader("Content-Type", "application/json");

                var removeResult = client.ExecuteTaskAsync(removeRequest).Result;

                quereis.Add(new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode });
            }

            return quereis;
        }

        public override List<QueryResponse> Validate(ExecutionContext context, RemoveFromProcessingDataCommand command, IDictionary<string, object> config, string id, MeshQuery query)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);
            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format("/automation/v2/workflows/enrollments/contacts/{0}", id), Method.GET);

            client.AddDefaultHeader("Authorization", $"Bearer {hubSpotCrawlJobData.ApiToken}");
            client.AddDefaultHeader("Content-Type", "application/json");

            var result = client.ExecuteTaskAsync<List<Workflow>>(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }
    }
}
