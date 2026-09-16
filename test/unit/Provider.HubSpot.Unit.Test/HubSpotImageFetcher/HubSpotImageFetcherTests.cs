using System;
using System.IO;
using System.Reflection;
using System.Threading;
using CluedIn.Core;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Resources;
using CluedIn.Crawling.HubSpot.Infrastructure;
using CluedIn.Crawling.HubSpot.Test.Common;
using Microsoft.Extensions.Logging;
using Moq;
using RestSharp;
using Xunit;

namespace Provider.HubSpot.Unit.Test.HubSpotImageFetcher
{
    public class HubSpotImageFetcherTests
    {
        private readonly IHubSpotFileFetcher _sut;
        private readonly Mock<IRestClient> _restClient;
        private readonly Mock<ILogger<HubSpotFileFetcher>> _log;

        public HubSpotImageFetcherTests()
        {
            _restClient = new Mock<IRestClient>();
            _log = new Mock<ILogger<HubSpotFileFetcher>>();

            _sut = new HubSpotFileFetcher(_log.Object, _restClient.Object);
        }

        [Fact]
        public void CanBeCreated() {
            Assert.NotNull(_sut);
        }

        [Theory,
         InlineData("HubSpotImageFetcher.CluedIn.png", "https://some-url.com", "/RawData/PreviewImage")]
        public void CheckDataPartIsReturnedFromFetchAsRawDataPart(string filename, string url, string type)
        {
            var fileData = ResourceHelper.GetFile(filename, Assembly.GetAssembly(typeof(HubSpotImageFetcherTests))).ToArray();

            // Production code calls IRestClient.DownloadData(request) on both RestSharp
            // generations, but that resolves differently: on 106.x (pre-5.0) it's a real
            // interface member (sync, returns byte[]), directly mockable. On 114.x (5.0+) it's
            // only a RestClientExtensions.DownloadData static extension method that delegates to
            // the real interface member DownloadStreamAsync(RestRequest, CancellationToken) -
            // Moq can't intercept extension method calls at all ("Extension methods may not be
            // used in setup / verification expressions"), so DownloadStreamAsync must be mocked
            // directly there instead.
#if CLUEDIN_V50
            _restClient.Setup(n => n.DownloadStreamAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new MemoryStream(fileData));
#else
            _restClient.Setup(n => n.DownloadData(It.IsAny<IRestRequest>()))
                .Returns(fileData);
#endif

            var result = _sut.FetchAsRawDataPart(url, type, filename);

            Assert.IsType<RawDataPart>(result);
        }

        [Theory,
         InlineData("HubSpotImageFetcher.CluedIn.png", "https://some-url.com", "/RawData/PreviewImage")]
        public void CheckExceptionsAreLoggedFromFetchAsRawDataPart(string filename, string url, string type)
        {
            var request = new RestRequest(url);
#if CLUEDIN_V50
            _restClient.Setup(n => n.DownloadStreamAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Invalid URL"));
#else
            _restClient.Setup(n => n.DownloadData(It.IsAny<IRestRequest>()))
                .Throws(new Exception("Invalid URL"));
#endif

            _sut.FetchAsRawDataPart(request, type, filename);

            MoqUtils.VerifyLog(_log.Object, LogLevel.Warning, Times.Once());
        }

    }
}
