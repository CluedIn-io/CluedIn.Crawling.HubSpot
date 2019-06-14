using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class Value
    {
        public string type { get; set; }
        public string value { get; set; }
        public object timestamp { get; set; }
    }
}
