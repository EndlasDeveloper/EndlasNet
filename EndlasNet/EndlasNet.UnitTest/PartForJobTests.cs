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
            Assert.IsNull(PartSuffixGenerator.GetPartSuffix(-1));
        }

        [Test]
        public void GetPartSuffixSingleDigit()
        {
            // smallest 1 digit valid edge case
            suffix = PartSuffixGenerator.GetPartSuffix(0);
            Assert.AreEqual("A", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(1);
            Assert.AreEqual("B", suffix);
            // largest 1 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(25);
            Assert.AreEqual("Z", suffix);
        }

        [Test]
        public void GetPartSuffixDoubleDigitTest()
        {       
            // smallest 2 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(26);
            Assert.AreEqual("AA", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(27);
            Assert.AreEqual("AB", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(51);
            Assert.AreEqual("AZ", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(52);
            Assert.AreEqual("BA", suffix);
        }
    }
}
