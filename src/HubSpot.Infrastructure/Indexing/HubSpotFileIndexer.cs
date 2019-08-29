using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Agent.Jobs;
using CluedIn.Core.Configuration;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data;
using CluedIn.Core.IO;

namespace CluedIn.Crawling.HubSpot.Infrastructure.Indexing
{
    /// <summary>The HubSpot file indexer.</summary>
    public class HubSpotFileIndexer : IHubSpotFileIndexer
    {
        private readonly IAgentJobProcessorArguments _args;
        private readonly ApplicationContext _context;

        public HubSpotFileIndexer([NotNull] IAgentJobProcessorState<CrawlJobData> args, [NotNull] ApplicationContext context)
        {
            _args = args ?? throw new ArgumentNullException(nameof(args));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Index(byte[] data, string filename, Clue clue)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (clue == null)
                throw new ArgumentNullException(nameof(clue));

            if (!ConfigurationManager.AppSettings.GetFlag("Crawl.InitialCrawl.FileIndexing", true))
                return;

            if (data.Length > Constants.MaxFileIndexingFileSize)
                return;

            using (var tempFile = new TemporaryFile(filename))
            {
                await CreatePhysicalFile(data, tempFile).ConfigureAwait(false);

                FileCrawlingUtility.IndexFile(tempFile, clue.Data, clue, _args, _context);
            }
        }

        protected async Task CreatePhysicalFile(byte[] data, FileInfo info)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var fs = new FileStream(info.FullName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await stream.CopyToAsync(fs).ConfigureAwait(false);
                }
            }
        }
    }
}
