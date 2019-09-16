using System;
using System.Reflection;
using CluedIn.Core;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Logging;
using CluedIn.Core.Resources;
using CluedIn.Crawling.HubSpot.Infrastructure;
using Moq;
using RestSharp;
using Xunit;

namespace Provider.HubSpot.Unit.Test.HubSpotImageFetcher
{
    public class HubSpotImageFetcherTests
    {
        private readonly IHubSpotFileFetcher _sut;
        private readonly Mock<IRestClient> _restClient;
        private readonly Mock<ILogger> _log;

        public HubSpotImageFetcherTests()
        {
            _restClient = new Mock<IRestClient>();
            _log = new Mock<ILogger>();

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

            _restClient.Setup(n => n.DownloadData(new RestRequest(url))).Returns(fileData);

            var result = _sut.FetchAsRawDataPart(url, type, filename);

            Assert.IsType<RawDataPart>(result);
        }

        [Theory,
         InlineData("HubSpotImageFetcher.CluedIn.png", "https://some-url.com", "/RawData/PreviewImage")]
        public void CheckExceptionsAreLoggedFromFetchAsRawDataPart(string filename, string url, string type)
        {
            var request = new RestRequest(url);
            _restClient.Setup(n => n.DownloadData(request)).Throws(new Exception("Invalid URL"));
            _log.Setup(n => n.Warn(It.IsAny<Func<string>>(), It.IsAny<Exception>()));
            
            _sut.FetchAsRawDataPart(request, type, filename);

            _log.Verify(n => n.Warn(It.IsAny<Func<string>>(), It.IsAny<Exception>()), Times.Once);
        }

    }
}
