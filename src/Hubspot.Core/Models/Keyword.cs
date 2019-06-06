using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Keyword
    {
        public string keyword { get; set; }
        public string keyword_guid { get; set; }
        public int country { get; set; }
        public int visits { get; set; }
        public int contacts { get; set; }
        public int leads { get; set; }
        public long created_at { get; set; }
    }

    public class KeywordResponse : Response
    {
        public int? limit { get; set; }
        public List<Keyword> keywords { get; set; }
    }
}
