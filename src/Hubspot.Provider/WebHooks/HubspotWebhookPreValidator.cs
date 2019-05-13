using CluedIn.Core.Webhooks;
using CluedIn.Crawling.HubSpot.Core;

namespace CluedIn.Provider.HubSpot.WebHooks
{
    public class Name_WebhookPreValidator : BaseWebhookPrevalidator
    {
        public Name_WebhookPreValidator()
            : base(HubSpotConstants.ProviderId, HubSpotConstants.ProviderName)
        {
        }
    }
}
