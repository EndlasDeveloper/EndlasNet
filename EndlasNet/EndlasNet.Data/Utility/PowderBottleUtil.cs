using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public static class PowderBottleUtil
    {
        public static float GetCostPerPound(float lineItemCostPerBottle, float feePerBottle, float numberOfPounds)
        {
            return (lineItemCostPerBottle + feePerBottle) / numberOfPounds;
        }

        public static float? GetFeePerBottle(float shippingCost, float taxCost, int numberOfBottles)
        {
            return (shippingCost + taxCost) / numberOfBottles;
        }
    }
}
