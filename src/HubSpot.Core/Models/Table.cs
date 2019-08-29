using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
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