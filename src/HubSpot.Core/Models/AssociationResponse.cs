using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class AssociationResponse : Response
    {
        public List<long> Results { get; set; }
        public bool HasMore { get; set; }
        public int Offset { get; set; }
    }
}
