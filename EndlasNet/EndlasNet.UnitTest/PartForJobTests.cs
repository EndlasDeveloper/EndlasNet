using NUnit.Framework;
using EndlasNet.Data;
namespace EndlasNet.UnitTest
{
    public class PartForJobTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetPartSuffixTest()
        {

            int numParts = 27;
            for(int i = 0; i < numParts; i++)
            {
                PartForJob partForJob = new PartForJob
                {
                    PartId = new System.Guid(),
                    NumParts = numParts,
                    ConditionDescription = "",
                    ProcessingNotes = ""
                };
                PartSuffixGenerator.SetPartSuffix(partForJob, i);
                Assert.AreEqual(1, partForJob.Suffix.Length);
                Assert.IsTrue(partForJob.Suffix.StartsWith((char)(i + 'A')));
            }
        }
    }
}
