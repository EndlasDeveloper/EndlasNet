using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPowderOrderRepo
    {
        public Task AddRow(PowderOrder powderOrder);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<PowderOrder>> GetAllRows();
        public Task<PowderOrder> GetRow(Guid? powderOrderId);
        public Task<object> GetRowNoTracking(Guid? id);

        public bool RowExists(Guid id);
        public Task UpdateRow(PowderOrder powderOrder);
        public Task<IEnumerable<Vendor>> GetAllVendors();
        public Task AddLineItems(List<LineItem> lineItems);
    }
}
