namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Property
    {
        public string Name { get; set; }
        public object Versions { get; set; }
        public string Value { get; set; }
        public string Timestamp { get; set; }
        public string Source { get; set; }
        public string SourceId { get; set; }
    }

    /*
    *{
            "portalId": 62515,
            "dealId": 3,
            "isDeleted": false,
            "associations": {
                "associatedVids": [
                    1
                ],
                "associatedCompanyIds": [],
                "associatedDealIds": []
            },
            "properties": {
    *
    */

}
