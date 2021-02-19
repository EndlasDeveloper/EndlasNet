using NUnit.Framework;
using EndlasNet.Data;
namespace EndlasNet.UnitTest
{
    public class PartForJobTests
    {
        private string suffix;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetPartSuffixInvalidTest()
        {
            Assert.IsEmpty(PartSuffixGenerator.GetPartSuffix(-1));
            Assert.IsEmpty(PartSuffixGenerator.GetPartSuffix(0));
        }

        [Test]
        public void GetPartSuffixSingleDigit()
        {
            // smallest 1 digit valid edge case
            suffix = PartSuffixGenerator.GetPartSuffix(1);
            Assert.AreEqual("A", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(2);
            Assert.AreEqual("B", suffix);
            // largest 1 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(26);
            Assert.AreEqual("Z", suffix);
        }

        [Test]
        public void GetPartSuffixDoubleDigitTest()
        {
           
            // smallest 2 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(27);
            Assert.AreEqual("AA", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(28);
            Assert.AreEqual("AB", suffix);
/*            suffix = PartSuffixGenerator.GetPartSuffix(52);
            Assert.AreEqual("AZ", suffix);*/
        }
    }
}
