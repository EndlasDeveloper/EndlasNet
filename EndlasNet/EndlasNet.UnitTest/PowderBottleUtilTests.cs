using NUnit.Framework;
using EndlasNet.Data;

namespace EndlasNet.UnitTest
{
    class PowderBottleUtilTests
    {

        [Test]
        public void CalculatePowderCostPerPoundTest()
        {
            // ARRANGE
            var lineItemCost = 100.0f;
            var shippingCost = 12.50f;
            var taxCost = 27.50f;
            var numberOfBottles = 2;
            var numberOfPounds = 10.0f;
            // ACT
            var feePerBottle = PowderBottleUtil.GetFeePerBottle(shippingCost, taxCost, numberOfBottles);
            Assert.AreEqual(20.0f, feePerBottle);

            var cost = PowderBottleUtil.GetCostPerPound(lineItemCost, (float)feePerBottle, numberOfPounds);
            // ASSERT
            Assert.AreEqual(12.0, cost);
        }
    }
}
