using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CluedIn.Crawling.HubSpot.Core.Models;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public interface IHubSpotClient
    {
        Task<Settings> GetSettingsAsync();
        Task<List<string>> GetCompanyPropertiesAsync(Settings settings);
        Task<List<string>> GetContactPropertiesAsync(Settings settings);
        Task<IEnumerable<object>> GetEngagementByIdAndTypeAsync(long objectId, string objectType);
        Task<IList<string>> GetDealPropertiesAsync(Settings settings);
        Task<CompanyResponse> GetCompaniesAsync(IList<string> properties, int limit = 100, long offset = 0);
        Task<List<string>> GetProductPropertiesAsync(Settings settings);
        Task<ProductResponse> GetProductsAsync(IList<string> properties, int limit = 100, int offset = 0);
        Task<List<string>> GetLineItemPropertiesAsync(Settings settings);
        Task<LineItemResponse> GetLineItemsAsync(IList<string> properties, int limit = 100, int offset = 0);
        Task<List<string>> GetTicketPropertiesAsync(Settings settings);
        Task<TicketResponse> GetTicketsAsync(IList<string> properties, int limit = 100, int offset = 0);
        Task<ContactListResponse> GetDynamicContactListsAsync(int limit = 20, int offset = 0);
        Task<ContactListResponse> GetStaticContactListsAsync(int limit = 20, int offset = 0);
        Task<AssociationResponse> GetAssociationsAsync(int objectId, AssociationType associationType, int limit = 100, int offset = 0);
        Task<ContactResponse> GetContactsFromAllListsAsync(IList<string> properties, int limit = 100, int offset = 0);
        Task<DealResponse> GetDealsAsync(IList<string> properties, Settings settings, int limit = 100, int offset = 0);
        Task<FileMetaDataResponse> GetFilesAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<List<CalendarEvent>> GetSocialCalendarEventsAsync(DateTimeOffset startDate, DateTimeOffset endDate, int limit = 20, int offset = 0);
        Task<List<CalendarEvent>> GetTaskCalendarEventsAsync(DateTimeOffset startDate, DateTimeOffset endDate, int limit = 20, int offset = 0);
        Task<RecentDealResponse> GetRecentDealsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<RecentDealResponse> GetRecentlyCreatedDealsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<IList<BroadcastMessage>> GetBroadcastMessagesAsync(DateTimeOffset greaterThanEpoch, int limit = 100, int offset = 0);
        Task<UrlMappingResponse> GetUrlMappingsAsync(DateTimeOffset greaterThanEpoch, int limit = 100, int offset = 0);
        Task<TemplateResponse> GetTemplatesAsync(int limit = 20, int offset = 0);
        Task<EngagementResponse> GetEngagementsAsync(int limit = 100, long offset = 0);
        Task<SiteMapResponse> GetSiteMapsAsync(int limit = 20, int offset = 0);
        Task<BlogPostResponse> GetBlogPostsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<TopicResponse> GetBlogTopicsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<BlogPostResponse> GetBlogsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<DomainResponse> GetDomainsAsync(DateTimeOffset greaterThanEpoch, int limit = 20, int offset = 0);
        Task<RowResponse> GetTableRowsAsync(DateTimeOffset greaterThanEpoch, long tableId, Column dateColumn, long portalId, int limit = 20, int offset = 0);
        Task<List<Form>> GetFormsAsync();
        Task<List<Table>> GetTablesAsync();
        Task<WorkflowsResponse> GetWorkflowsAsync();
        Task<List<Form>> GetSmtpTokensAsync();
        Task<List<Channel>> GetPublishingChannelsAsync();
        Task<List<Owner>> GetOwnersAsync();
        Task<List<KeywordResponse>> GetKeywordsAsync();
        Task<List<DealPipeline>> GetDealPipelinesAsync();
        Task<ContactResponse> GetContactsByCompanyAsync(long companyId);
        Task<List<OwnerResponse>> GetAccountInformation();
        Task<WebHookResponse> GetWebHooks();
        Task<WebHookResponse> CreateWebHook(string subscription);
    }
}
