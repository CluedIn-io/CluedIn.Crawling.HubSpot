using System;
using CluedIn.Crawling.HubSpot.Core;
using CluedIn.Crawling.HubSpot.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Configuration;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public class HubSpotClient : IHubSpotClient
    {
        private readonly ILogger<HubSpotClient> _log;
        private readonly IRestClient _client;

        public HubSpotClient(ILogger<HubSpotClient> log, HubSpotCrawlJobData hubspotCrawlJobData, IRestClient client)
        {
            if (hubspotCrawlJobData == null)
                throw new ArgumentNullException(nameof(hubspotCrawlJobData));

            _log = log ?? throw new ArgumentNullException(nameof(log));

            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.BaseUrl =
                hubspotCrawlJobData.BaseUri != null
                    ? hubspotCrawlJobData.BaseUri
                    : new Uri(HubSpotConstants.ApiBaseUri);

            _client.AddDefaultHeader("Authorization", $"Bearer {hubspotCrawlJobData.ApiToken}");
            _client.AddDefaultHeader("Content-Type", "application/json");
        }

        public async Task<Settings> GetSettingsAsync() => await GetAsync<Settings>("integrations/v1/me");

        public async Task<DailyLimit> GetDailyLimitAsync()
        {
            var result = await GetAsync<DailyLimit[]>("integrations/v1/limit/daily");

            return result.FirstOrDefault();
        } 

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

        public async Task<List<string>> GetContactPropertiesAsync(Settings settings)
        {
            var items = await GetAsync<List<PropertyDefinition>>("properties/v1/contacts/properties");

            var properties = items.Select(s => s.Name).ToList();
            if (!properties.Contains("name"))
                properties.Add("name");

            if (!properties.Contains("firstname"))
                properties.Add("firstname");

            if (!properties.Contains("lastname"))
                properties.Add("lastname");

            if (!properties.Contains("company"))
                properties.Add("company");

            if (!properties.Contains("lastmodifieddate"))
                properties.Add("lastmodifieddate");

            if (!properties.Contains("createddate"))
                properties.Add("createddate");

            if (!properties.Contains("ipaddress"))
                properties.Add("ipaddress");

            if (!properties.Contains("lifecyclestage"))
                properties.Add("lifecyclestage");

            if (!properties.Contains("closedate"))
                properties.Add("closedate");

            if (!properties.Contains("message"))
                properties.Add("message");

            if (!properties.Contains("jobtitle"))
                properties.Add("jobtitle");

            if (!properties.Contains("ownername"))
                properties.Add("ownername");

            if (!properties.Contains("owneremail"))
                properties.Add("owneremail");

            if (!properties.Contains("address"))
                properties.Add("address");

            if (!properties.Contains("fax"))
                properties.Add("fax");

            if (!properties.Contains("phone"))
                properties.Add("phone");

            if (!properties.Contains("email"))
                properties.Add("email");

            if (!properties.Contains("city"))
                properties.Add("city");

            if (!properties.Contains("state"))
                properties.Add("state");

            if (!properties.Contains("zip"))
                properties.Add("zip");

            if (!properties.Contains("country"))
                properties.Add("country");

            if (!properties.Contains("hubspot score"))
                properties.Add("hubspot score");

            if (!properties.Contains("salutation"))
                properties.Add("salutation");

            return properties;
        }

        public async Task<IEnumerable<object>> GetEngagementByIdAndTypeAsync(long objectId, string objectType)
        {
            var result = new List<object>();
            long? offset = 0;
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
            var result = await GetAsync<IList<PropertyDefinition>>("properties/v1/deals/properties");

            return result.Select(n => n.Name).ToList();
        }

        public async Task<CompanyResponse> GetCompaniesAsync(IList<string> properties, int limit = 100, long offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<CompanyResponse>("companies/v2/companies/paged", queryStrings);
        }

        public async Task<List<string>> GetProductPropertiesAsync(Settings settings)
        {
            var items = await GetAsync<List<PropertyDefinition>>("properties/v1/contacts/properties");

            return items.Select(s => s.Name).ToList();
        }

        public async Task<ProductResponse> GetProductsAsync(IList<string> properties, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<ProductResponse>("crm-objects/v1/objects/products/paged", queryStrings);
        }

        public async Task<List<string>> GetLineItemPropertiesAsync(Settings settings)
        {
            var items = await GetAsync<List<PropertyDefinition>>("properties/v2/line_items/properties");

            return items.Select(s => s.Name).ToList();
        }

        public async Task<LineItemResponse> GetLineItemsAsync(IList<string> properties, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<LineItemResponse>("crm-objects/v1/objects/line_items/paged", queryStrings);
        }

        public async Task<List<string>> GetTicketPropertiesAsync(Settings settings)
        {
            var items = await GetAsync<List<PropertyDefinition>>("properties/v2/tickets/properties");

            return items.Select(s => s.Name).ToList();
        }

        public async Task<TicketResponse> GetTicketsAsync(IList<string> properties, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<TicketResponse>("crm-objects/v1/objects/tickets/paged", queryStrings);
        }

        public async Task<ContactListResponse> GetDynamicContactListsAsync(int limit = 20, int offset = 0) =>
            await GetAsync<ContactListResponse>("contacts/v1/lists/dynamic", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit)
            });

        public async Task<ContactListResponse> GetStaticContactListsAsync(int limit = 20, int offset = 0) =>
            await GetAsync<ContactListResponse>("contacts/v1/lists/static", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit)
            });


        public async Task<AssociationResponse> GetAssociationsAsync(int objectId, AssociationType associationType, int limit = 100, int offset = 0) =>
            await GetAsync<AssociationResponse>($"crm-associations/v1/associations/{objectId}/HUBSPOT_DEFINED/" + Convert.ToInt32(associationType), new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit)
            });


        public virtual async Task<ContactResponse> GetContactsFromAllListsAsync(IList<string> properties, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("vidOffset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<ContactResponse>("contacts/v1/lists/all/contacts/all", queryStrings);
        }

        public async Task<DealResponse> GetDealsAsync(IList<string> properties, Settings settings, int limit = 100, int offset = 0)
        {
            var queryStrings = properties.Select(n => new QueryStringParameter("properties", n)).ToList();
            queryStrings.Insert(0, new QueryStringParameter("offset", offset));
            queryStrings.Insert(0, new QueryStringParameter("limit", limit));

            return await GetAsync<DealResponse>("deals/v1/deal/paged", queryStrings);
        }

        public async Task<FileMetaDataResponse> GetFilesAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<FileMetaDataResponse>("filemanager/api/v2/files", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("created__gt", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
        });

        public async Task<List<CalendarEvent>> GetSocialCalendarEventsAsync(DateTimeOffset startDate, DateTimeOffset endDate, int limit = 20, int offset = 0) =>
            await GetAsync<List<CalendarEvent>>("calendar/v1/events/social", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("startDate", DateUtilities.ConvertDatetimeToUnixTimeStamp(startDate)),
            new QueryStringParameter("endDate", DateUtilities.ConvertDatetimeToUnixTimeStamp(endDate)),
            new QueryStringParameter("includeNoCampaigns", true)
        });

        public async Task<List<CalendarEvent>> GetTaskCalendarEventsAsync(DateTimeOffset startDate, DateTimeOffset endDate, int limit = 20, int offset = 0) =>
            await GetAsync<List<CalendarEvent>>("calendar/v1/events/task", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("startDate", DateUtilities.ConvertDatetimeToUnixTimeStamp(startDate)),
            new QueryStringParameter("endDate", DateUtilities.ConvertDatetimeToUnixTimeStamp(endDate)),
            new QueryStringParameter("includeNoCampaigns", true)
        });

        public async Task<RecentDealResponse> GetRecentDealsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<RecentDealResponse>("deals/v1/deal/recent/modified", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("since", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
        });

        public async Task<RecentDealResponse> GetRecentlyCreatedDealsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<RecentDealResponse>("deals/v1/deal/recent/created", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("since", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
        });

        public async Task<IList<BroadcastMessage>> GetBroadcastMessagesAsync(DateTimeOffset greaterThanEpoch, int limit = 100, int offset = 0) =>
            await GetAsync<IList<BroadcastMessage>>("broadcast/v1/broadcasts", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("since", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
        });

        public async Task<UrlMappingResponse> GetUrlMappingsAsync(DateTimeOffset greaterThanEpoch, int limit = 100, int offset = 0) =>
            await GetAsync<UrlMappingResponse>("url-mappings/v3/url-mappings", new List<QueryStringParameter>
        {
            new QueryStringParameter("offset", offset),
            new QueryStringParameter("limit", limit),
            new QueryStringParameter("created__gt", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
        });

        public async Task<TemplateResponse> GetTemplatesAsync(int limit = 20, int offset = 0) =>
            await GetAsync<TemplateResponse>("content/api/v2/templates", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit)
            });

        public async Task<EngagementResponse> GetEngagementsAsync(int limit = 100, long offset = 0) =>
            await GetAsync<EngagementResponse>("engagements/v1/engagements/paged", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit)
            });

        public async Task<SiteMapResponse> GetSiteMapsAsync(int limit = 20, int offset = 0) =>
            await GetAsync<SiteMapResponse>("content/api/v2/site-maps", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit)
            });

        public async Task<BlogPostResponse> GetBlogPostsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<BlogPostResponse>("content/api/v2/blog-posts", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit),
                new QueryStringParameter("created__gt", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
            });

        public async Task<TopicResponse> GetBlogTopicsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<TopicResponse>("blogs/v3/topics", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit),
                new QueryStringParameter("created__gt", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
            });

        public async Task<BlogPostResponse> GetBlogsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<BlogPostResponse>("content/api/v2/blogs", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit),
                new QueryStringParameter("created__gt", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
            });

        public async Task<DomainResponse> GetDomainsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0) =>
            await GetAsync<DomainResponse>("content/api/v4/domains", new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit),
                new QueryStringParameter("created__gt", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch))
            });

        public async Task<RowResponse> GetTableRowsAsync(DateTimeOffset greaterThanEpoch, long tableId, Column dateColumn, long portalId, int limit = 20, int offset = 0)
        {
            var queryStrings = new List<QueryStringParameter>
            {
                new QueryStringParameter("offset", offset),
                new QueryStringParameter("limit", limit),
                new QueryStringParameter("portalId", portalId),
            };

            if (dateColumn != null)
            {
                queryStrings.Add(new QueryStringParameter(dateColumn.name + "__gte", DateUtilities.ConvertDatetimeToUnixTimeStamp(greaterThanEpoch)));
            }

            return await GetAsync<RowResponse>($"hubdb/api/v2/tables/{tableId}/rows", queryStrings);
        }

        public async Task<List<Form>> GetFormsAsync() =>
            await GetAsync<List<Form>>("forms/v2/forms");

        public async Task<List<Table>> GetTablesAsync() =>
            await GetAsync<List<Table>>("hubdb/api/v2/tables");

        public async Task<WorkflowsResponse> GetWorkflowsAsync() =>
            await GetAsync<WorkflowsResponse>("automation/v3/workflows");

        public async Task<List<Form>> GetSmtpTokensAsync() =>
            await GetAsync<List<Form>>("email/public/v1/smtpapi/tokens");

        public async Task<List<Channel>> GetPublishingChannelsAsync() =>
            await GetAsync<List<Channel>>("broadcast/v1/channels/setting/publish/current");

        public async Task<List<Owner>> GetOwnersAsync() =>
            await GetAsync<List<Owner>>("owners/v2/owners");

        public async Task<List<KeywordResponse>> GetKeywordsAsync() =>
            await GetAsync<List<KeywordResponse>>("keywords/v1/keywords");

        public async Task<List<DealPipeline>> GetDealPipelinesAsync() =>
            await GetAsync<List<DealPipeline>>("deals/v1/pipelines");

        public async Task<ContactResponse> GetContactsByCompanyAsync(long companyId) =>
            await GetAsync<ContactResponse>($"companies/v2/companies/{companyId}/contacts");

        private async Task<T> GetAsync<T>(string url, IList<QueryStringParameter> parameters = null)
        {
            var request = new RestRequest(url, Method.GET);

            AddParametersToRequest<T>(parameters, request);

            var response = await _client.ExecuteTaskAsync(request);

            return GetRequestResponse<T>(url, response);
        }
        private async Task<T> PostAsync<T>(string url, object body, IList<QueryStringParameter> parameters = null)
        {
            var request = new RestRequest(url, Method.POST);

            AddParametersToRequest<T>(parameters, request);

            if (body != null)
            {
                request.AddBody(body);
            }

            var response = await _client.ExecuteTaskAsync(request);

            return GetRequestResponse<T>(url, response);
        }

        private static void AddParametersToRequest<T>(IList<QueryStringParameter> parameters, RestRequest request)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Parameter);
                }
            }
        }

        private T GetRequestResponse<T>(string url, IRestResponse response)
        {
            _log.LogTrace("HubSpotClient.GetAsync calling {url}", url);
            if ((int)response.StatusCode == 429)
            {
                throw new ThrottlingException
                {
                    DailyRemaining = Convert.ToInt32(response.Headers.Single(n => n.Name == "X-HubSpot-RateLimit-Daily-Remaining")?.Value),
                    RateLimitIntervalMilliseconds = Convert.ToInt32(response.Headers.Single(n => n.Name == "X-HubSpot-RateLimit-Interval-Milliseconds")?.Value),
                    RateLimitRemaining = Convert.ToInt32(response.Headers.Single(n => n.Name == "X-HubSpot-RateLimit-Interval-Milliseconds")?.Value)
                };
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _log.LogError("Request to {url} failed, response {errorMessage} ({statusCode})", $"{_client.BaseUrl}{url}", response.ErrorMessage, response.StatusCode);

                throw new InvalidOperationException("Communication to HubSpot unavailable.");
            }

            var data = JsonConvert.DeserializeObject<T>(response.Content);

            _log.LogTrace("HubSpotClient response content length: {contentLength}", response.Content.Length);

            return data;
        }

        private class QueryStringParameter
        {
            public Parameter Parameter { get; }

            public QueryStringParameter(string name, object value)
            {
                Parameter = new Parameter(name, value.ToString(), ParameterType.QueryString);
            }
        }

        public async Task<List<OwnerResponse>> GetAccountInformation() =>
            await GetAsync<List<OwnerResponse>>("owners/v2/owners");

        public async Task<WebHookResponse> GetWebHooks() =>
            await GetAsync<WebHookResponse>($"webhooks/v1/{ConfigurationManagerEx.AppSettings.GetValue("Providers.HubSpot.AppId", "")}/subscriptions");

        public async Task<WebHookResponse> CreateWebHook(string subscription) =>
            await PostAsync<WebHookResponse>($"webhooks/v1/{ConfigurationManagerEx.AppSettings.GetValue("Providers.HubSpot.AppId", "")}/subscriptions",
                $"{{ \"subscriptionDetails\" : {{ \"subscriptionType\" : \"{subscription}\", \"propertyName\" : \"companyname\"}},\"enabled\" : true }}");
    }
}
