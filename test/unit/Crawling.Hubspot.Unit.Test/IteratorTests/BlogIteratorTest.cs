using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CluedIn.Crawling.HubSpot.Core.Models;
using CluedIn.Crawling.HubSpot.Iterators;
using Moq;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Crawling.HubSpot.Unit.Test.IteratorTests
{
    public class BlogIteratorTest : BaseIteratorTest
    {
        private BlogsIterator _sut;
        
        public BlogIteratorTest()
        {
            _sut = new BlogsIterator(Client.Object, JobData);
        }

        [Fact]
        public void IterateHandlesEmptyData()
        {
            Client.Setup(n => n.GetBlogsAsync(DateTimeOffset.Now, 20, 0)).ReturnsAsync(new BlogPostResponse());

            var result = _sut.Iterate();

            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
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
            Assert.Equal(0, result.Count());
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
