using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.HubSpot.Infrastructure.Exceptions
{
    public class ThrottlingException : Exception
    {
        public int DailyRemaining { get; set; }
        public int RateLimitIntervalMilliseconds { get; set; }
        public int RateLimitRemaining { get; set; }
    }
}
