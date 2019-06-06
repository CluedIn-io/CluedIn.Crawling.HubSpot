using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class PropertyDefinition
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Groupname { get; set; }
        public string Type { get; set; }
        public string Fieldtype { get; set; }
        public bool Hidden { get; set; }
        public List<object> Options { get; set; }
        public bool Calculated { get; set; }
        public bool Externaloptions { get; set; }
        public bool? Hubspotdefined { get; set; }
        public bool Formfield { get; set; }
        public int Displayorder { get; set; }
        public bool Readonlyvalue { get; set; }
        public bool Readonlydefinition { get; set; }
        public bool? Deleted { get; set; }
        public bool Mutabledefinitionnotdeletable { get; set; }
        public bool Favorited { get; set; }
        public int Favoritedorder { get; set; }
        public string Displaymode { get; set; }
        public bool? Showcurrencysymbol { get; set; }
        public long? Createduserid { get; set; }
        public string Textdisplayhint { get; set; }
        public object Numberdisplayhint { get; set; }
        public bool? Optionsaremutable { get; set; }
        public string Referencedobjecttype { get; set; }
        public bool Iscustomizeddefault { get; set; }
        public long? Createdat { get; set; }
        public long? Updatedat { get; set; }
        public int? Updateduserid { get; set; }
    }

    public class PropertyOption
    {
        public bool hidden { get; set; }
        public string description { get; set; }
        public int displayOrder { get; set; }
        public string label { get; set; }
        public object doubleData { get; set; }
        public object readOnly { get; set; }
        public string value { get; set; }
        //public bool? hubspotDefined { get; set; }
    }

    /*
*
*"name": "anothercustom",
        "label": "another custom",
        "description": "",
        "groupName": "contactinformation",
        "type": "string",
        "fieldType": "text",
        "formField": true,
        "displayOrder": 0,
        "options": []
*
*/
}
