using System;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.Hubspot.Core;
using CluedIn.Crawling.Hubspot.Core.Models;
using System.Collections.Generic;
using RestSharp;

namespace CluedIn.Crawling.Hubspot.Infrastructure
{
  public class HubspotClient
  {
    private const string s_baseUri = "http://sample.com";

    // ReSharper disable once NotAccessedField.Local
    private readonly ILogger _log;

    public HubspotClient(ILogger log, HubspotCrawlJobData hubspotCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
    {
        if (hubspotCrawlJobData == null) throw new ArgumentNullException(nameof(hubspotCrawlJobData));
       if (client == null) throw new ArgumentNullException(nameof(client));

       _log = log ?? throw new ArgumentNullException(nameof(log));

      // TODO use info from hubspotCrawlJobData to instantiate the connection
      client.BaseUrl = new Uri(s_baseUri);
      client.AddDefaultParameter("api_key", hubspotCrawlJobData.ApiKey, ParameterType.QueryString);
    }

    public IEnumerable<_SampleFolder_> GetFolders()
    {
      return new[] { new _SampleFolder_ { Id = 1, Name = "Sample folder" } };
    }

    public IEnumerable<_SampleFile_> GetFilesForFolder(int folderId)
    {
      return new[] {
                new _SampleFile_ { Id= 11, Name = "Sample file 1", FolderId = folderId, Uri = new Uri("http://foo.com/file1")},
                new _SampleFile_ { Id= 12, Name = "Sample file 2", FolderId = folderId, Uri = new Uri("http://foo.com/file1")}
            };
    }

    public AccountInformation GetAccountInformation()
    {
      return new AccountInformation("", ""); //TODO
    }
  }
}
