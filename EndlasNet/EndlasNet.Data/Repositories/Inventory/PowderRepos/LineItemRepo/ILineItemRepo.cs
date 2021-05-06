using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface ILineItemRepo
    {
        public Task<IEnumerable<LineItem>> GetLineItems(Guid powderOrderId);
        public Task<LineItem> GetRow(Guid? lineItemId);
        public Task<IEnumerable<object>> GetAllRows();
        public Task AddRow(LineItem lineItem);
        public Task UpdateRow(LineItem lineItem);
        public Task DeleteRow(Guid? id);
        public Task<LineItem> GetAllLineItemsNoTracking(Guid? lineItemId);
        public Task<object> GetRowNoTracking(Guid? id);
        public Task<LineItem> GetLineItemInclude(Guid? id);
        public Task<string> GetPurchaseOrderNumber(Guid powderOrderId);
        public Task<bool> RowExists(Guid id);
        public Task<PowderOrder> GetPowderOrder(Guid id);
        public Task<IEnumerable<StaticPowderInfo>> GetAllStaticPowderInfo();
        public Task<StaticPowderInfo> GetStaticPowderInfo(Guid id);
        public Task AddPowderBottles(List<PowderBottle> bottles);
        public Task<IEnumerable<PowderBottle>> GetLineItemPowder(LineItem lineItem);
        public void RemovePowderBottles(List<PowderBottle> bottles);

    }
}
