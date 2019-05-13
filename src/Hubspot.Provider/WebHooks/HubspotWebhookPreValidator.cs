using CluedIn.Core.Webhooks;
using CluedIn.Crawling.Hubspot.Core;

namespace CluedIn.Provider.Hubspot.WebHooks
{
    public class Name_WebhookPreValidator : BaseWebhookPrevalidator
    {
        public Name_WebhookPreValidator()
            : base(HubspotConstants.ProviderId, HubspotConstants.ProviderName)
        {
        }
    }
}
