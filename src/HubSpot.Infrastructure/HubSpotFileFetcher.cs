using System;
using CluedIn.Core;
using CluedIn.Core.Data.Parts;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public class HubSpotFileFetcher : IHubSpotFileFetcher
    {
        private readonly ILogger<HubSpotFileFetcher> _log;
        private readonly IRestClient _client;

        public HubSpotFileFetcher(ILogger<HubSpotFileFetcher> log, IRestClient client)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public RawDataPart FetchAsRawDataPart(string url, string type, string filename)
        {
            return FetchAsRawDataPart(new RestRequest(url), type, filename);
        }

        public RawDataPart FetchAsRawDataPart(RestRequest request, string type, string filename)
        {
            RawDataPart rawDataPart = null;

            try
            {
                var data = _client.DownloadData(request);
                rawDataPart = new RawDataPart
                {
                    Type = type,
                    MimeType = CluedIn.Core.FileTypes.MimeType.Jpeg.Code,
                    FileName = filename,
                    RawDataMD5 = FileHashUtility.GetMD5Base64String(data),
                    RawData = Convert.ToBase64String(data)
                };
            }
            catch (Exception exception)
            {
                _log.LogWarning(exception, "Could not download HubSpot thumbnail");  
            }

            return rawDataPart;
        }

        public byte[] FetchAsBytes(string url)
        {
            return _client.DownloadData(new RestRequest(url));
        }
    }
}
