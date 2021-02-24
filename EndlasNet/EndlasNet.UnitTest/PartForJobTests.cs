using NUnit.Framework;
using EndlasNet.Data;
namespace EndlasNet.UnitTest
{
    public class PartForJobTests
    {
        private string suffix;

        [Test]
        public void GetPartSuffixInvalidTest()
        {
            // null when invalid integer as param
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
            Assert.AreEqual("BA", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(27);
            Assert.AreEqual("BB", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(51);
            Assert.AreEqual("BZ", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(52);
            Assert.AreEqual("CA", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(53);
            Assert.AreEqual("CB", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(77);
            Assert.AreEqual("CZ", suffix);
            // larges 2 digit suffix
            suffix = PartSuffixGenerator.GetPartSuffix(675);
            Assert.AreEqual("ZZ", suffix);
        }

        [Test]
        public void GetPartSuffixTripleDigitTest()
        {
            // smallest 3 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(676);
            Assert.AreEqual("BAA", suffix);
            suffix = PartSuffixGenerator.GetPartSuffix(677);
            Assert.AreEqual("BAB", suffix);
            // largest 3 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(17575);
            Assert.AreEqual("ZZZ", suffix);
        }

        [Test]
        public void GetPartSuffixQuadDigitTest()
        {
            // smallest 4 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(17576);
            Assert.AreEqual("BAAA", suffix);
            // largest 4 digit edge case
            suffix = PartSuffixGenerator.GetPartSuffix(456975);
            Assert.AreEqual("ZZZZ", suffix);
        }
    }
}
