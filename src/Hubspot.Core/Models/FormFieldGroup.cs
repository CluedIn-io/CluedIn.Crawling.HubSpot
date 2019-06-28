using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class FormFieldGroup
    {
        public List<Field> fields { get; set; }
        public bool @default { get; set; }
        public bool isSmartGroup { get; set; }
    }
}