using AutoFixture.Xunit2;
using CluedIn.Core.Resources;
using CluedIn.Crawling.HubSpot.Core;
using Xunit;

namespace Provider.HubSpot.Unit.Test.Resources
{
    public class ResourceHelperTests
    {
        [Theory,
         InlineAutoData(HubSpotConstants.IconResourceName)]
        public void CheckResourceHelperReturnsIconAsBase64(string file)
        {
            var result = ResourceHelper.GetFile(file, typeof(CluedIn.Provider.HubSpot.HubSpotProvider).Assembly);

            Assert.NotEqual(0, result.Length);
        }
    }
}
