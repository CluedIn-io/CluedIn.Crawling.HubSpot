using System;
using System.Globalization;
using CluedIn.Core.Utilities;
using Xunit;

namespace Crawling.HubSpot.Unit.Test
{
    public class DateTimeConversionTests
    {
        [Theory, InlineData(1560992400000)]
        public void CheckCanConvertFromUnixEpochToDateTime(long epoch)
        {
            var date = DateUtilities.EpochRef.AddMilliseconds(epoch);
            var iso8601 = date.ToString("O");

            DateTimeOffset dummy;
            Assert.True(DateTimeOffset.TryParse(iso8601, out dummy));
            var result = DateTimeOffset.Parse(iso8601, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);



        }
    }
}
