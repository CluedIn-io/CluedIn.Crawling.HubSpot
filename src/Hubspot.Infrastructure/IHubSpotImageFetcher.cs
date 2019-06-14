using CluedIn.Core.Data.Parts;

namespace CluedIn.Crawling.HubSpot.Infrastructure
{
    public interface IHubSpotImageFetcher
    {
        RawDataPart FetchAsRawDataPart(string url, string type, string filename);
    }
}