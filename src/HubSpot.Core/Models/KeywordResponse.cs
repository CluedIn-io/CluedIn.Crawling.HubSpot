using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class KeywordResponse : Response
    {
        public int? limit { get; set; }
        public List<Keyword> keywords { get; set; }
    }
}