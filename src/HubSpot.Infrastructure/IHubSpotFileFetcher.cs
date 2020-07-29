using CluedIn.Core.Data.Parts;
using RestSharp;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public interface IHubSpotFileFetcher
    {
        RawDataPart FetchAsRawDataPart(string url, string type, string filename);
        RawDataPart FetchAsRawDataPart(RestRequest request, string type, string filename);
        byte[] FetchAsBytes(string url);
    }
}
