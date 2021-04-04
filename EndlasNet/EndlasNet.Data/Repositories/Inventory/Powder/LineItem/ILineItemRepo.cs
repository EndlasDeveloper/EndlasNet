using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface ILineItemRepo : IRepository
    {
        public Task<IEnumerable<LineItem>> GetLineItems(Guid powderOrderId);
    }
}
