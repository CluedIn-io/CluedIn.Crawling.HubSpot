using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Mesh;
using CluedIn.Core.Messages.Processing;
using CluedIn.Core.Messages.WebApp;
using CluedIn.Crawling.HubSpot.Core;
using RestSharp;

namespace CluedIn.Provider.HubSpot.Mesh.Hubspot
{
    public abstract class HubspotUpdateBaseMeshProcessor : BaseMeshProcessor
    {
        public EntityType[] EntityType { get; }
        public string EditUrl { get; }
     
        protected HubspotUpdateBaseMeshProcessor(ApplicationContext appContext, string editUrl, params EntityType[] entityType)
            : base(appContext)
        {
            EntityType = entityType;
            EditUrl = editUrl;
        }

        public override bool Accept(MeshDataCommand command, MeshQuery query, IEntity entity)
        {
            return command.ProviderId == this.GetProviderId() && query.Action == ActionType.UPDATE && EntityType.Contains(entity.EntityType);
        }

        public override void DoProcess(CluedIn.Core.ExecutionContext context, MeshDataCommand command, IDictionary<string, object> jobData, MeshQuery query)
        {
            return;
        }
        
        public override List<RawQuery> GetRawQueries(IDictionary<string, object> config, IEntity entity, Core.Mesh.Properties properties)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            return new List<Core.Messages.WebApp.RawQuery>()
            {
                new Core.Messages.WebApp.RawQuery()
                {
                    Query = string.Format("curl -X PUT https://api.hubapi.com/" + EditUrl + "{1}?hapikey={0} "  + "--header \"Content-Type: application/json\"" + " --data '{2}'", hubSpotCrawlJobData.ApiToken, this.GetLookupId(entity), JsonUtility.Serialize(properties)),
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
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);
            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format(EditUrl + "{0}", id), Method.PUT);
            request.AddQueryParameter("hapikey", hubSpotCrawlJobData.ApiToken); // adds to POST or URL querystring based on Method
            request.AddJsonBody(properties);

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }

        public override List<QueryResponse> Validate(ExecutionContext context, MeshDataCommand command, IDictionary<string, object> config, string id, MeshQuery query)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format(EditUrl + "{0}", id), Method.GET);
            request.AddQueryParameter("hapikey", hubSpotCrawlJobData.ApiToken); // adds to POST or URL querystring based on Method

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }

        
    }
}
