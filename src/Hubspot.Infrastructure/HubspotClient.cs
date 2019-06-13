using System;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Task = CluedIn.Crawling.HubSpot.Core.Models.Task;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public class HubSpotClient : IHubSpotClient
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

        public async Task<Settings> GetSettingsAsync() => await GetAsync<Settings>("integrations/v1/me");

        public async Task<List<string>> GetCompanyPropertiesAsync(Settings settings)
        {
            var items = await GetAsync<List<PropertyDefinition>>("properties/v1/companies/properties");

            var properties = items.Select(s => s.Name).ToList();
            if (!properties.Contains("name"))
                properties.Add("name");
            if (!properties.Contains("companyname"))
                properties.Add("companyname");
            if (!properties.Contains("about_us"))
                properties.Add("about_us");
            if (!properties.Contains("numemployees"))
                properties.Add("numemployees");
            if (!properties.Contains("annual"))
                properties.Add("annual");
            if (!properties.Contains("company_notes"))
                properties.Add("company_notes");
            if (!properties.Contains("first_deal_created_date"))
                properties.Add("first_deal_created_date");
            if (!properties.Contains("industry"))
                properties.Add("industry");
            if (!properties.Contains("hs_analytics_source"))
                properties.Add("hs_analytics_source");
            if (!properties.Contains("hs_lastmodifieddate"))
                properties.Add("hs_lastmodifieddate");
            if (!properties.Contains("integrations"))
                properties.Add("integrations");
            if (!properties.Contains("domain"))
                properties.Add("domain");
            if (!properties.Contains("facebook_company_page"))
                properties.Add("facebook_company_page");
            if (!properties.Contains("founded_year"))
                properties.Add("founded_year");
            if (!properties.Contains("is_public"))
                properties.Add("is_public");
            if (!properties.Contains("linkedin_company_page"))
                properties.Add("linkedin_company_page");
            if (!properties.Contains("num_associated_contacts"))
                properties.Add("num_associated_contacts");
            if (!properties.Contains("timezone"))
                properties.Add("timezone");
            if (!properties.Contains("days_to_close"))
                properties.Add("days_to_close");
            if (!properties.Contains("description"))
                properties.Add("description");

            return properties;
        }

        public async Task<IEnumerable<object>> GetEngagementByIdAndTypeAsync(long objectId, string objectType)
        {
            var result = new List<object>();
            int? offset = 0;
            while (true)
            {
                var queryStrings = new List<QueryStringParameter>
                {
                    new QueryStringParameter("limit", "20"),
                    new QueryStringParameter("offset", offset)
                };

                var response = await GetAsync<EngagementResponse>($"/engagements/v1/engagements/associated/{objectType}/{objectId}/paged",
                    queryStrings);

                if (response?.results == null)
                    return result;


                foreach (var engagement in response.results)
                {
                    if (engagement?.engagement == null)
                        continue;

                    foreach (var c in engagement.Converttospecific())
                        result.Add(c);
                }

                if (!response.results.Any())
                    break;

                if (!response.hasMore)
                    break;

                offset = response.offset;


            }

            return result;
        }
        
        public async Task<IList<string>> GetDealPropertiesAsync(Settings settings)
        {
          var result =  await GetAsync<IList<PropertyDefinition>>("properties/v1/deals/properties");

          return result.Select(n => n.Name).ToList();
        }
        
        public async Task<CompanyResponse> GetCompaniesAsync(IList<string> properties, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<CompanyResponse>("companies/v2/companies/paged", queryStrings);
        }


        public async Task<DealResponse> GetRecentDealsAsync(DateTime since, int count = 100, int offset = 0) =>
            throw new NotImplementedException();

        public async Task<DealResponse> GetDealsAsync(IList<string> properties, Settings settings, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<DealResponse>("deals/v1/deal/paged", queryStrings);
        }

        public async Task<EngagementResponse> GetEngagementsAsync(int limit = 100, int offset = 0 /*TODO properties*/) =>
        throw new NotImplementedException();

        public async Task<CompanyResponse> GetRecentlyCreatedCompaniesAsync(int count = 100, int offset = 0) =>
                throw new NotImplementedException();
        public async Task<CompanyResponse> GetRecentlyModifiedCompaniesAsync(int count = 100, int offset = 0) =>
                throw new NotImplementedException();

        public async Task<List<DealPipeline>> GetDealPipelinesAsync(long portalId) =>
                throw new NotImplementedException();

        public async Task<ContactResponse> GetContactsByCompanyAsync(long companyId) =>
            await GetAsync<ContactResponse>($"companies/v2/companies/{companyId}/contacts");

        private async Task<T> GetAsync<T>(string url, IList<QueryStringParameter> parameters = null)
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

    public interface IHubSpotClient
    {
        Task<Settings> GetSettingsAsync();
        Task<List<string>> GetCompanyPropertiesAsync(Settings settings);
        Task<IEnumerable<object>> GetEngagementByIdAndTypeAsync(long objectId, string objectType);
        Task<IList<string>> GetDealPropertiesAsync(Settings settings);
        Task<CompanyResponse> GetCompaniesAsync(IList<string> properties, int limit = 100, int offset = 0);
        Task<DealResponse> GetRecentDealsAsync(DateTime since, int count = 100, int offset = 0);
        Task<DealResponse> GetDealsAsync(IList<string> properties, Settings settings, int limit = 100, int offset = 0);
        Task<EngagementResponse> GetEngagementsAsync(int limit = 100, int offset = 0 /*TODO properties*/);
        Task<CompanyResponse> GetRecentlyCreatedCompaniesAsync(int count = 100, int offset = 0);
        Task<CompanyResponse> GetRecentlyModifiedCompaniesAsync(int count = 100, int offset = 0);
        Task<List<DealPipeline>> GetDealPipelinesAsync(long portalId);
        Task<ContactResponse> GetContactsByCompanyAsync(long companyId);
        AccountInformation GetAccountInformation();
    }
}
