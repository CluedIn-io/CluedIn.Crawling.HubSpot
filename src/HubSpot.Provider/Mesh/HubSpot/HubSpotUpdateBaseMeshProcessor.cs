﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Mesh;
using CluedIn.Core.Messages.Processing;
using CluedIn.Core.Messages.WebApp;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Provider.HubSpot.Mesh.HubSpot.Extensions;
using Newtonsoft.Json;
using RestSharp;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot
{
    public abstract class HubSpotUpdateBaseMeshProcessor : BaseMeshProcessor
    {
        public EntityType[] EntityType { get; }
        public string EditUrl { get; }
        public string VocabPrefix { get; }
        public Method UpdateMethod { get; }

        protected HubSpotUpdateBaseMeshProcessor(ApplicationContext appContext, string editUrl, string vocabPrefix, Method updateMethod, params EntityType[] entityType)
            : base(appContext)
        {
            EntityType = entityType;
            EditUrl = editUrl;
            VocabPrefix = vocabPrefix;
            UpdateMethod = updateMethod;
        }

        public override bool Accept(MeshDataCommand command, MeshQuery query, IEntity entity)
        {
            return command.ProviderId == this.GetProviderId() && query.Action == ActionType.UPDATE && EntityType.Contains(entity.EntityType) && query.Transform.HasValidVocabularyKey();
        }

        public override void DoProcess(CluedIn.Core.ExecutionContext context, MeshDataCommand command, IDictionary<string, object> jobData, MeshQuery query)
        {
            return;
        }

        public override List<RawQuery> GetRawQueries(IDictionary<string, object> config, IEntity entity, Core.Mesh.Properties properties)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            return new List<RawQuery>()
            {
                new RawQuery()
                {
                    Query = $"curl -X PUT https://api.hubapi.com/{EditUrl}{GetLookupId(entity)} --header \"Bearer {hubSpotCrawlJobData.ApiToken}\" --header \"Content-Type: application/json\" --data '{JsonUtility.Serialize(properties)}'",
                    Source = "cUrl"
                }
            };
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

        public override List<QueryResponse> RunQueries(IDictionary<string, object> config, string id, Core.Mesh.Properties properties)
        {
            var hubSpotProperties = properties.ToHubSpotProperties(VocabPrefix);
            if(hubSpotProperties == null)
            {
                return new List<QueryResponse>() { new QueryResponse() { Content = "Properties are invalid.", StatusCode = HttpStatusCode.BadRequest } };
            }

            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);
            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(EditUrl.Replace(":id", id), UpdateMethod);

            client.AddDefaultHeader("Authorization", $"Bearer {hubSpotCrawlJobData.ApiToken}");
            client.AddDefaultHeader("Content-Type", "application/json");

            request.AddJsonBody(JsonConvert.SerializeObject(hubSpotProperties));

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }

        public override List<QueryResponse> Validate(ExecutionContext context, MeshDataCommand command, IDictionary<string, object> config, string id, MeshQuery query)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format(EditUrl + "{0}", id), Method.GET);

            client.AddDefaultHeader("Authorization", $"Bearer {hubSpotCrawlJobData.ApiToken}");
            client.AddDefaultHeader("Content-Type", "application/json");

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }
    }
}
