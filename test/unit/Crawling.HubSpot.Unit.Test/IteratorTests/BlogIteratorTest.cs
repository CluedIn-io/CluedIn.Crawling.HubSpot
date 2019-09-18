using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CluedIn.Core.Logging;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Infrastructure.Exceptions;
using CluedIn.Crawling.HubSpot.Iterators;
using Moq;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Crawling.HubSpot.Unit.Test.IteratorTests
{
    public class BlogIteratorTest : BaseIteratorTest
    {
        private readonly BlogsIterator _sut;
        private readonly Mock<ILogger> _loggerMock;

        public BlogIteratorTest()
        {
            _loggerMock = new Mock<ILogger>();
            _sut = new BlogsIterator(Client.Object, JobData, _loggerMock.Object);
        }

        [Fact]
        public void IterateHandlesEmptyData()
        {
            Client.Setup(n => n.GetBlogsAsync(DateTimeOffset.Now, 20, 0)).ReturnsAsync(new BlogPostResponse());

            var result = _sut.Iterate();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void IterateBreaksIfCollectionNullOrEmpty()
        {
            Client.Setup(n => n.GetBlogsAsync(DateTimeOffset.Now, 20, 0)).ReturnsAsync(new BlogPostResponse
            {
                objects = null
            });

            var result = _sut.Iterate();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void IteratePagingCorrectly()
        {
            var greaterThanEpoch = DateTimeOffset.Now;

            _sut.JobData.LastCrawlFinishTime = greaterThanEpoch;

            Client.Setup(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0)).Returns(Task.FromResult(new BlogPostResponse
            {
                objects = new List<BlogPost>
                {
                    new BlogPost(),
                    new BlogPost()
                },
                offset = 2
            }));
            Client.Setup(n => n.GetBlogsAsync(greaterThanEpoch, 2, 2)).ReturnsAsync(new BlogPostResponse
            {
                objects = new List<BlogPost>
                {
                    new BlogPost()
                }
            });
            
            var result = _sut.Iterate(2).ToList();

            Client.Verify(n => n.GetBlogsAsync(greaterThanEpoch, 2, It.IsAny<int>()), Times.Exactly(2));
        }


        [Fact]
        public void HandlesDailyThrottlingCorrectly()
        {
            var greaterThanEpoch = DateTimeOffset.Now;

            _sut.JobData.LastCrawlFinishTime = greaterThanEpoch;

            Client.Setup(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0)).Throws(new ThrottlingException
            {
                DailyRemaining = 0
            });

            _sut.Iterate(2);

            Client.Verify(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0), Times.Once);
        }

        [Fact]
        public void HandlesThrottlingRetriesCorrectly()
        {
            var greaterThanEpoch = DateTimeOffset.Now;

            _sut.JobData.LastCrawlFinishTime = greaterThanEpoch;
            _sut.MaxRetries = 2;

            Client.Setup(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0)).Throws(new ThrottlingException
            {
                DailyRemaining = 1000,
                RateLimitRemaining = 0,
                RateLimitIntervalMilliseconds = 1000
            });
            
            _sut.Iterate(2);

            Client.Verify(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0), Times.Exactly(3));
        }

        [Fact]
        public void VerifyExceptionsAreCaughtAndReturnEmptyEnumerable()
        {
            var greaterThanEpoch = DateTimeOffset.Now;

            _sut.JobData.LastCrawlFinishTime = greaterThanEpoch;

            Client.Setup(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0)).Throws(new Exception());

            var result = _sut.Iterate(2);

            Client.Verify(n => n.GetBlogsAsync(greaterThanEpoch, 2, 0), Times.Once);

            Assert.Empty(result);
        }
    }
}
