namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class CalendarEvent
    {
        public string id { get; set; }
        public string eventType { get; set; }
        public long eventDate { get; set; }
        public string category { get; set; }
        public long? categoryId { get; set; }
        public long? contentId { get; set; }
        public string state { get; set; }
        public string campaignGuid { get; set; }
        public long? portalId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public long? ownerId { get; set; }
        public long? createdById { get; set; }
        public long? createdBy { get; set; }
        public string previewKey { get; set; }
        public object socialUsername { get; set; }
        public object socialDisplayName { get; set; }
        public object avatarUrl { get; set; }
        public object topicIds { get; set; }
        public long? contentGroupId { get; set; }
        public bool recurring { get; set; }
    }
}

/*
*
*{
    "id": "2087009576",
    "eventType": "CONTENT",
    "eventDate": 1415975962000,
    "category": "email",
    "categoryId": 2,
    "contentId": 2087009576,
    "state": "PUBLISHED",
    "campaignGuid": "",
    "portalId": 62515,
    "name": "Z 20141114-0937",
    "description": "",
    "url": "http:\/\/fakesite.com\/slug",
    "ownerId": null,
    "createdById": null,
    "createdBy": 21501033030,
    "previewKey": "jwVDH6Z9",
    "socialUsername": null,
    "socialDisplayName": null,
    "avatarUrl": null,
    "topicIds": null,
    "contentGroupId": null,
    "recurring": false
  }
*
*/
