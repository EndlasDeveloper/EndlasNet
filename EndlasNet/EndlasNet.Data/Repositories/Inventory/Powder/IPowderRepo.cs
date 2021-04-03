﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPowderRepo
    {
        public Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId);

    }
}
