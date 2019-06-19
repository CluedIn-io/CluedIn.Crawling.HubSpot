using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Row
    {
        public object Id { get; set; }
        public object CreatedAt { get; set; }
        public object Path { get; set; }
        public object Name { get; set; }
        public object Values { get; set; }
        public List<Column> Columns { get; set; }
        public long? Table { get; set; }
    }
}