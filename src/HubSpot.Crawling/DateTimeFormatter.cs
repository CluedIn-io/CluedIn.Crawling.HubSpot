using System;

namespace CluedIn.Crawling.HubSpot
{
    public static class DateTimeFormatter
    {
        public static string ToIso8601(DateTimeOffset offset)
        {
            return offset.ToString("O");
        }
    }
}
