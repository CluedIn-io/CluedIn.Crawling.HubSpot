using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace CluedIn.Crawling.HubSpot.Test.Common
{
    public static class MoqUtils
    {        
        public static void VerifyLog(ILogger logger, LogLevel level, Times times)
        {
            var mock = Mock.Get(logger);
            mock.Verify(
                x => x.Log(
                    level,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                times);            
        }
    }
}
