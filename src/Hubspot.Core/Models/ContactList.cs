using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class ContactList
    {
        public string name { get; set; }

        public long? internalListId { get; set; }

        public long? listId { get; set; }

        public bool deleted { get; set; }

        public bool dynamic { get; set; }

        public long? portalId { get; set; }

        public List<object> filters { get; set; }

        public long? updatedAt { get; set; }

        public long? createdAt { get; set; }

        public MetaData metaData { get; set; }

    }
}


/*
*
*{
            'name': 'atestlist',
            'internalListId': 1,
            'listId': 1,
            'deleted': False,
            'dynamic': False,
            'portalId': 62515,
            'filters': [

            ],
            'updatedAt': 1331836468512,
            'createdAt': 1331836468512,
            'metaData': {
                'lastProcessingStateChangeAt': 1331836468512,
                'processing': 'DONE',
                'lastSizeChangeAt': 0,
                'error': '',
                'size': 0
            }
}
*
*/
