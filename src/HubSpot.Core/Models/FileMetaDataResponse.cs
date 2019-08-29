using System.Collections.Generic;

namespace CluedIn.Crawling.HubSpot.Core.Models
{
    public class FileMetaDataResponse : Response
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public List<FileMetaData> objects { get; set; }
        public int? total_count { get; set; }
    }
}