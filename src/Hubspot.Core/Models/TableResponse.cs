using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class TableResponse : Response
    {
        public List<Table> Objects { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public new object Message { get; set; }
        public int TotalCount { get; set; }
    }

    public class Column
    {
        public string name { get; set; }
        public string label { get; set; }
        public long id { get; set; }
        public string type { get; set; }
    }

    public class Table
    {
        public long id { get; set; }
        public string name { get; set; }
        public object createdAt { get; set; }
        public long? publishedAt { get; set; }
        public long updatedAt { get; set; }
        public List<Column> columns { get; set; }
        public bool deleted { get; set; }
        public bool useForPages { get; set; }
        public int rowCount { get; set; }
        public object createdBy { get; set; }
        public object updatedBy { get; set; }
        public int columnCount { get; set; }
        public object PortalId { get; set; }
    }
}
