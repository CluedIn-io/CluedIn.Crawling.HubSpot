using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class RowResponse : Response
    {
        public List<Row> Objects { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public new object Message { get; set; }
        public int TotalCount { get; set; }
    }
}
