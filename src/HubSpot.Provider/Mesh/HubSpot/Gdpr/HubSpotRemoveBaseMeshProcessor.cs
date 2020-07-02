using System;
using System.Collections.Generic;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Mesh;
using CluedIn.Core.Messages.Processing;
using CluedIn.Crawling.HubSpot.Core;
using RestSharp;

namespace CluedIn.Provider.HubSpot.Mesh.HubSpot.Gdpr
{
    public abstract class HubSpotRemoveBaseMeshProcessor : BaseRemoveProcessor
    {
        public EntityType[] EntityType { get; }
        public string DeleteUrl { get; }

        protected HubSpotRemoveBaseMeshProcessor(ApplicationContext appContext)
           : base(appContext)
        {
        }

        protected HubSpotRemoveBaseMeshProcessor(ApplicationContext appContext, string deleteUrl, params EntityType[] entityType)
            : base(appContext)
        {
            EntityType = entityType;
            DeleteUrl = deleteUrl;
        }

        public override bool Accept(RemoveDataCommand command, MeshQuery query, IEntity entity)
        {
            return command.ProviderId == this.GetProviderId() && query.Action == ActionType.REMOVE && EntityType.Contains(entity.EntityType);
        }

        // TODO: MeshQUery should be handed in
        //All of the stores should be in a base class and only the client parts are passed in.
        /// <summary>Does the process.</summary>
        /// <param name="context">The context.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override void DoProcess(CluedIn.Core.ExecutionContext context, RemoveDataCommand command, IDictionary<string, object> jobData, MeshQuery query)
        {
            return;
        }

        public override List<Core.Messages.WebApp.RawQuery> GetRawQueries(IDictionary<string, object> config, IEntity entity)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            return new List<Core.Messages.WebApp.RawQuery>()
            {
                new Core.Messages.WebApp.RawQuery()
                {
                    Query = string.Format("curl -X DELETE https://api.hubapi.com/" + DeleteUrl + "{0}?hapikey={1} "  + "--header \"Content-Type: application/json\"", hubSpotCrawlJobData.ApiToken, this.GetLookupId(entity)),
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

        public override List<QueryResponse> RunQueries(IDictionary<string, object> config, string id)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);
            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format(DeleteUrl + "{0}", id), Method.DELETE);
            request.AddQueryParameter("hapikey", hubSpotCrawlJobData.ApiToken); // adds to POST or URL querystring based on Method

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }

        public override List<QueryResponse> Validate(ExecutionContext context, RemoveDataCommand command, IDictionary<string, object> config, string id, MeshQuery query)
        {
            var hubSpotCrawlJobData = new HubSpotCrawlJobData(config);

            var client = new RestClient("https://api.hubapi.com");
            var request = new RestRequest(string.Format(DeleteUrl + "{0}", id), Method.GET);
            request.AddQueryParameter("hapikey", hubSpotCrawlJobData.ApiToken); // adds to POST or URL querystring based on Method

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }
    }
}
