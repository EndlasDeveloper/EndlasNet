using NUnit.Framework;
using EndlasNet.Data;
namespace EndlasNet.UnitTest
{
    public class PartForWorkTests
    {
        private string suffix;

        [Test]
        public void GetPartSuffixInvalidTest()
        {
            // null when invalid integer as param
            Assert.IsNull(PartSuffixGenerator.IndexToSuffix(-1));
        }

        [Test]
        public void GetPartSuffixSingleDigit()
        {
            // smallest 1 digit valid edge case
            suffix = PartSuffixGenerator.IndexToSuffix(0);
            Assert.AreEqual("A", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(1);
            Assert.AreEqual("B", suffix);
            // largest 1 digit edge case
            suffix = PartSuffixGenerator.IndexToSuffix(25);
            Assert.AreEqual("Z", suffix);
        }

        [Test]
        public void GetPartSuffixDoubleDigitTest()
        {       
            // smallest 2 digit edge case
            suffix = PartSuffixGenerator.IndexToSuffix(26);
            Assert.AreEqual("BA", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(27);
            Assert.AreEqual("BB", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(51);
            Assert.AreEqual("BZ", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(52);
            Assert.AreEqual("CA", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(53);
            Assert.AreEqual("CB", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(77);
            Assert.AreEqual("CZ", suffix);
            // larges 2 digit suffix
            suffix = PartSuffixGenerator.IndexToSuffix(675);
            Assert.AreEqual("ZZ", suffix);
        }

        [Test]
        public void GetPartSuffixTripleDigitTest()
        {
            // smallest 3 digit edge case
            suffix = PartSuffixGenerator.IndexToSuffix(676);
            Assert.AreEqual("BAA", suffix);
            suffix = PartSuffixGenerator.IndexToSuffix(677);
            Assert.AreEqual("BAB", suffix);
            // largest 3 digit edge case
            suffix = PartSuffixGenerator.IndexToSuffix(17575);
            Assert.AreEqual("ZZZ", suffix);
        }

        [Test]
        public void GetPartSuffixQuadDigitTest()
        {
            // smallest 4 digit edge case
            suffix = PartSuffixGenerator.IndexToSuffix(17576);
            Assert.AreEqual("BAAA", suffix);
            // largest 4 digit edge case
            suffix = PartSuffixGenerator.IndexToSuffix(456975);
            Assert.AreEqual("ZZZZ", suffix);
        }

        [Test]
        public void SuffixToIndexSingleCharTest()
        {
            var result = PartSuffixGenerator.SuffixToIndex("A");
            Assert.AreEqual(0, result);
            result = PartSuffixGenerator.SuffixToIndex("B");
            Assert.AreEqual(1, result);
            result = PartSuffixGenerator.SuffixToIndex("Z");
            Assert.AreEqual(25, result);
        }

        [Test]
        public void SuffixToIndexDoubleCharTest()
        {
            var result = PartSuffixGenerator.SuffixToIndex("BA");
            Assert.AreEqual(26, result);
            result = PartSuffixGenerator.SuffixToIndex("BB");
            Assert.AreEqual(27, result);
            result = PartSuffixGenerator.SuffixToIndex("ZZ");
            Assert.AreEqual(675, result);
        }
    }
}
