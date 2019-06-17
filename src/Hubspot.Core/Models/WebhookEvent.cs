namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class WebhookEvent
    {
        public int objectId { get; set; }
        public string propertyName { get; set; }
        public string propertyValue { get; set; }
        public string changeSource { get; set; }
        public long eventId { get; set; }
        public int subscriptionId { get; set; }
        public int portalId { get; set; }
        public int appId { get; set; }
        public long occurredAt { get; set; }
        public string subscriptionType { get; set; }
        public int attemptNumber { get; set; }
    }

    /*
    *  "vid": 71,
        "canonical-vid": 71,
        "portal-id": 62515,
        "profile-token": "AO_T-mMZs-n9xi4QD7dSSPBnLcWSFuLlza0RH7r5EI12UMul8pXi1bhuR71bsusug6f31W9KKfOntz1jUCfrS2QE18gCk_74Dkip4GUsgxKyk7A0uNGtrCYmRm4ZhR0ThUONh2PglE8R",
        "profile-url": "https://app.hubspot.com/contacts/62515/lists/public/contact/_AO_T-mMZs-n9xi4QD7dSSPBnLcWSFuLlza0RH7r5EI12UMul8pXi1bhuR71bsusug6f31W9KKfOntz1jUCfrS2QE18gCk_74Dkip4GUsgxKyk7A0uNGtrCYmRm4ZhR0ThUONh2PglE8R/",
        "properties": {
    */
}
