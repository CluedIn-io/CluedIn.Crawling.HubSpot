using System;

namespace CluedIn.Crawling.HubSpot.Infrastructure.Exceptions
{
    public class ThrottlingException : Exception
    {
        public int DailyRemaining { get; set; }
        public int RateLimitIntervalMilliseconds { get; set; }
        public int RateLimitRemaining { get; set; }
    }
}
