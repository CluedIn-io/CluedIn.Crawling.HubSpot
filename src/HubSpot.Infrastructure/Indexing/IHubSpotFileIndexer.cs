using System.Threading.Tasks;
using CluedIn.Core.Data;

namespace CluedIn.Crawling.HubSpot.Infrastructure.Indexing
{
    public interface IHubSpotFileIndexer
    {
        Task Index(byte[] data, string filename, Clue clue);
    }
}
