using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPowderBottleRepo 
    {
        public Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId);

    }
}
