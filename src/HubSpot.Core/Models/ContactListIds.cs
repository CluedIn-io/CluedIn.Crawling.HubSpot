using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class ContactListIds
    {
        public int enrolled { get; set; }
        public int active { get; set; }
        public List<object> steps { get; set; }
    }
}