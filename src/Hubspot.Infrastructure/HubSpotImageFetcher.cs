using System;
using System.Net;
using CluedIn.Core;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Logging;
using CluedIn.Core.Utilities;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public class HubSpotImageFetcher : IHubSpotImageFetcher
    {
        private readonly ILogger _log;

        public HubSpotImageFetcher(ILogger log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public RawDataPart FetchAsRawDataPart(string url, string type, string filename)
        {
            RawDataPart rawDataPart = null;
            try
            {
                using (var webClient = new WebClient()) // TODO Make testable by moving WebClient to its own class
                using (var stream = webClient.OpenRead(url))
                {
                    var inArray = StreamUtilies.ReadFully(stream);
                    if (inArray != null)
                    {
                        rawDataPart = new RawDataPart()
                        {
                            Type = type,
                            MimeType = CluedIn.Core.FileTypes.MimeType.Jpeg.Code,
                            FileName = filename,
                            RawDataMD5 = FileHashUtility.GetMD5Base64String(inArray),
                            RawData = Convert.ToBase64String(inArray)
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                _log.Warn(() => "Could not download HubSpot thumbnail", exception);
            }

            return rawDataPart;
        }
    }
}
