using NUnit.Framework;
using EndlasNet.Data;
namespace EndlasNet.UnitTest
{
    public class ShaHashTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ComputeSha256HashTest()
        {
            // ARRANGE
            var testAuthStr = UnitTestUtil.getRandomString(8);
            var differentAuthStr = UnitTestUtil.getRandomString(8);

            // ACT
            var hashedAuthStr = Security.ComputeSha256Hash(testAuthStr);
            var hashedDiffAuthStr = Security.ComputeSha256Hash(differentAuthStr);
            var hashedAuthStr2 = Security.ComputeSha256Hash(testAuthStr);

            // ASSERT
            // make sure the same auth str gives same hash
            Assert.AreEqual(hashedAuthStr, hashedAuthStr2);
            // make sure different auth strs give different hash
            Assert.AreNotEqual(hashedAuthStr, hashedDiffAuthStr);
        }
    }
}