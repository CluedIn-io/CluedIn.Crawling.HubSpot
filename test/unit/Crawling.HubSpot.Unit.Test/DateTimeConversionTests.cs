using System;
using System.Globalization;
using CluedIn.Core.Utilities;
using CluedIn.Crawling.HubSpot;
using Xunit;

namespace Crawling.HubSpot.Unit.Test
{
    public class DateTimeConversionTests
    {
        [Theory, InlineData(1560992400000)]
        public void CheckCanConvertFromUnixEpochToDateTime(long epoch)
        {
            var date = DateUtilities.EpochRef.AddMilliseconds(epoch);
            var iso8601 = DateTimeFormatter.ToIso8601(date);

            DateTimeOffset dummy;
            Assert.True(DateTimeOffset.TryParse(iso8601, out dummy));
            var result = DateTimeOffset.Parse(iso8601, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            // No assertion on result as 


        }
    }
}
