using Moq;
using System;
using Xunit;
using EndlasNet.Data;
namespace EndlasNet.IntegrationTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mockContext = new Mock<EndlasNetDbContext>();
         /*   mockContext.Setup(c => c.
            Assert.Equal(4, calculator.Object.Add(2, 2));*/
        }
    }
}
