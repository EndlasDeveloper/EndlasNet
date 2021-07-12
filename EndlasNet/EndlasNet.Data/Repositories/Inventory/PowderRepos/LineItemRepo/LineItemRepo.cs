using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class LineItemRepo : ILineItemRepo
    {
        private readonly EndlasNetDbContext _db;
        public LineItemRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<LineItem>> GetLineItems(Guid powderOrderId)
        {
            var lineItems = await _db.LineItems
                .Include(l => l.PowderOrder)
                .Include(l => l.StaticPowderInfo)
                .Where(l => l.PowderOrderId == powderOrderId)
                .OrderByDescending(l => l.StaticPowderInfoId)
                .ToListAsync();

            return lineItems;
        }

        public async Task<LineItem> GetRow(Guid? lineItemId)
        {
            return await _db.LineItems
                .Include(l => l.StaticPowderInfo)
                .Include(l => l.PowderOrder)
                .FirstOrDefaultAsync(l => l.LineItemId == lineItemId);
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.LineItems.ToListAsync();
        }

        public async Task AddRow(LineItem lineItem)
        {
            await _db.LineItems.AddAsync(lineItem);
            await _db.SaveChangesAsync(); 
        }

        public async Task UpdateRow(LineItem lineItem)
        {
            var entry = _db.Entry(lineItem);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
            
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRow(Guid? id)
        {
            var lineItem = await _db.LineItems.FirstOrDefaultAsync(l => l.LineItemId == id);
            _db.LineItems.Remove(lineItem);
            await _db.SaveChangesAsync();
        }

        public async Task<LineItem> GetAllLineItemsNoTracking(Guid? lineItemId)
        {
            return await _db.LineItems
                .Include(l => l.PowderOrder)
                .Include(l => l.StaticPowderInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.LineItemId == lineItemId);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.LineItems
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.LineItemId == id);
        }

        public async Task<LineItem> GetLineItemInclude(Guid? id)
        {
            return await _db.LineItems
              .Include(l => l.PowderOrder)
              .Include(l => l.StaticPowderInfo)
              .FirstOrDefaultAsync(m => m.LineItemId == id);
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.LineItems.AnyAsync(e => e.LineItemId == id);
        }

        public async Task<string> GetPurchaseOrderNumber(Guid powderOrderId)
        {
            var powderOrder = await _db.PowderOrders
                .FirstOrDefaultAsync(p => p.PowderOrderId == powderOrderId);
            return powderOrder.PurchaseOrderNum;
        }

        public async Task<PowderOrder> GetPowderOrder(Guid id)
        {
            return await _db.PowderOrders
                .FirstOrDefaultAsync(p => p.PowderOrderId == id);
        }

        public async Task<IEnumerable<StaticPowderInfo>> GetAllStaticPowderInfo()
        {
            return await _db.StaticPowderInfo
                .OrderByDescending(s => s.EndlasDescription)
                .ToListAsync();
        }

        public async Task<StaticPowderInfo> GetStaticPowderInfo(Guid id)
        {
            return await _db.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == id);
        }

        public async Task AddPowderBottles(List<PowderBottle> bottles)
        {
            foreach(PowderBottle bottle in bottles)
            {
                _db.PowderBottles.Add(bottle);
            }
            await _db.SaveChangesAsync();
        }

        public void RemovePowderBottles(List<PowderBottle> bottles)
        {
            foreach (PowderBottle bottle in bottles)
            {
                _db.PowderBottles.Remove(bottle);
            }
        }

        public async Task<IEnumerable<PowderBottle>> GetLineItemPowder(LineItem lineItem)
        {
            return await _db.PowderBottles
                .Include(l => l.StaticPowderInfo)
                .Include(l => l.LineItem)
                .Where(p => p.LineItemId == lineItem.LineItemId)
                .OrderByDescending(p => p.StaticPowderInfo.EndlasDescription)
                .ToListAsync();
        }
    }
}
