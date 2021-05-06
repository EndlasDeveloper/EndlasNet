using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPowderBottleRepo 
    {
        public Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId);
        public Task UpdateRow(PowderBottle powderBottle);
        public Task<List<PowderBottle>> GetNamedPowders(string powderName);
        public Task<PowderBottle> GetRow(Guid? id);
        public Task<IEnumerable<PowderBottle>> GetAllRows();
        public Task AddRow(PowderBottle powderBottle);
        public Task RemoveRow(Guid id);
        public Task DeleteRow(PowderBottle powderBottle);
        public Task<bool> RowExists(Guid id);
        public Task<object> GetRowNoTracking(Guid? id);
        public Task<PowderBottle> FindRow(Guid? id);
        public Task<LineItem> GetLineItem(Guid id);
        public Task<PowderOrder> GetPowderOrder(Guid id);
        public Task<IEnumerable<PowderOrder>> GetAllPowderOrders();
        public Task<StaticPowderInfo> GetStaticPowderInfo(Guid id);
        public Task<List<List<PowderBottle>>> SetCostPerPound(List<PowderOrder> powderOrders);
        public void SetCreatedDate(PowderBottle powderBottle);
        public void SetUpdatedDate(PowderBottle powderBottle);
        public Task<bool> BottleNumberLotNumberExists(PowderBottle powderBottle);
        public Task SaveChanges();
        public void ModifyRow(PowderBottle powder);
    }
}
