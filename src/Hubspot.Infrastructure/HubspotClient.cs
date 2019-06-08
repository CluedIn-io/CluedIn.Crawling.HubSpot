using System;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public class HubSpotClient
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger _log;
        private readonly IRestClient _client;

        public HubSpotClient(ILogger log, HubSpotCrawlJobData hubspotCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
        {
            if (hubspotCrawlJobData == null)
                throw new ArgumentNullException(nameof(hubspotCrawlJobData));
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _log = log ?? throw new ArgumentNullException(nameof(log));

            // TODO use info from hubspotCrawlJobData to instantiate the connection
            _client = client;
            _client.BaseUrl = new Uri(HubSpotConstants.ApiBaseUri);
            _client.AddDefaultParameter("hapikey", hubspotCrawlJobData.ApiToken, ParameterType.QueryString);
        }

        public async Task<List<PropertyDefinition>> GetCompanyPropertiesAsync() =>
            await GetAsync<List<PropertyDefinition>>("properties/v1/companies/properties");


        public async Task<Settings> GetSettingsAsync() =>
            await GetAsync<Settings>("integrations/v1/me");

        public async Task<CompanyResponse> GetCompaniesAsync(int limit = 100, int offset = 0) =>
           await GetAsync<CompanyResponse>("companies/v2/companies/paged",
               new QueryStringParameter("limit", limit ),
               new QueryStringParameter("offset", offset)
               );

        public async Task<DealResponse> GetRecentDealsAsync(DateTime since, int count = 100, int offset = 0) =>
            throw new NotImplementedException();

        public async Task<DealResponse> GetDealsAsync(int limit = 100, int offset = 0 /*TODO properties*/) =>
          throw new NotImplementedException();

        public async Task<EngagementResponse> GetEngagementsAsync(int limit = 100, int offset = 0 /*TODO properties*/) =>
        throw new NotImplementedException();

        public async Task<CompanyResponse> GetRecentlyCreatedCompaniesAsync(int count = 100, int offset = 0) =>
                throw new NotImplementedException();
        public async Task<CompanyResponse> GetRecentlyModifiedCompaniesAsync(int count = 100, int offset = 0) =>
                throw new NotImplementedException();

        public async Task<List<DealPipeline>> GetDealPipelinesAsync() =>
                throw new NotImplementedException();

        public async Task<ContactResponse> GetContactsByCompanyAsync(long companyId) =>
            await GetAsync<ContactResponse>($"companies/v2/companies/{companyId}/contacts");

        private async Task<T> GetAsync<T>(string url, params QueryStringParameter[] parameters)
        {
            var request = new RestRequest(url, Method.GET);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Parameter);
                }
            }
            var response = await _client.ExecuteTaskAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<T>(response.Content);
                _log.Verbose($"HubspotClient returning {data}");
                return data;
            }
            throw new InvalidOperationException("Communication to hubspot unavailable");
        }

        private class QueryStringParameter
        {
            public Parameter Parameter { get; }
            public QueryStringParameter(string name, object value)
            {
                Parameter =
                  new Parameter()
                  {
                      Type = ParameterType.QueryString,
                      Name = name,
                      Value = value.ToString()
                  };
            }
        }

        public AccountInformation GetAccountInformation()
        {
            return new AccountInformation("", ""); //TODO
        }
    }
}
