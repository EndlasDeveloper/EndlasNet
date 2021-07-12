using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderBottleRepo : IPowderBottleRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderBottleRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task UpdateRow(PowderBottle powderBottle)
        {
            var entry = _db.Entry(powderBottle);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<List<PowderBottle>> GetNamedPowders(string powderName)
        {
            return await _db.PowderBottles
                .Where(p => p.StaticPowderInfo.EndlasDescription == powderName)
                .OrderByDescending(p => p.Weight)
                .ToListAsync();
        }

        public async Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId)
        {
            return await _db.PowderBottles
                .Include(l => l.StaticPowderInfo)
                .Include(l => l.LineItem)
                .Where(p => p.LineItemId == lineItemId)
                .OrderByDescending(p => p.StaticPowderInfo.EndlasDescription)
                .ToListAsync();
        }

        public async Task<PowderBottle> GetRow(Guid? id)
        {
            return await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<IEnumerable<PowderBottle>> GetAllRows()
        {
            return await _db.PowderBottles
               .Include(p => p.StaticPowderInfo)
               .Include(p => p.LineItem)
               .Include(p => p.LineItem.PowderOrder)
               .OrderByDescending(p => p.StaticPowderInfo.EndlasDescription)
               .ToListAsync();
        }

        public async Task AddRow(PowderBottle powderBottle)
        {
            await _db.PowderBottles.AddAsync(powderBottle);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRow(Guid id)
        {
            _db.PowderBottles
                .Remove(await _db.PowderBottles.FirstOrDefaultAsync(p => p.PowderBottleId == id));
        }

        public async Task DeleteRow(PowderBottle powderBottle)
        {
            _db.PowderBottles.Remove(powderBottle);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.PowderBottles
                .AnyAsync(p => p.PowderBottleId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.PowderBottles
                .AsNoTracking().
                FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<PowderBottle> FindRow(Guid? id)
        {
            return await _db.PowderBottles.FindAsync(id);
        }

        public async Task<LineItem> GetLineItem(Guid id)
        {
            return await _db.LineItems
                .FirstOrDefaultAsync(l => l.LineItemId == id);
        }

        public async Task<PowderOrder> GetPowderOrder(Guid id)
        {
            return await _db.PowderOrders
                .FirstOrDefaultAsync(p => p.PowderOrderId == id);
        }

        public async Task<StaticPowderInfo> GetStaticPowderInfo(Guid id)
        {
            return await _db.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == id);
        }

        public async Task<IEnumerable<PowderOrder>> GetAllPowderOrders()
        {
            return await _db.PowderOrders
                .Include(p => p.Vendor)
                .Include(p => p.LineItems).ThenInclude(l => l.PowderBottles)
                .OrderBy(p => p.PurchaseOrderNum)
                .ToListAsync();
        }

        public async Task<List<List<PowderBottle>>> SetCostPerPound(List<PowderOrder> powderOrders)
        {
            List<List<PowderBottle>> lineItemBottles = new List<List<PowderBottle>>();
            foreach (PowderOrder order in powderOrders)
            {
                foreach (LineItem item in order.LineItems)
                {
                    lineItemBottles.Insert(0, await _db.PowderBottles.Include(p => p.StaticPowderInfo).Where(p => p.LineItemId == item.LineItemId).ToListAsync());
                    var bottleFee = PowderBottleUtil.GetFeePerBottle((float)order.ShippingCost, (float)order.TaxCost, lineItemBottles[0].Count());
                    foreach (PowderBottle b in lineItemBottles[0])
                    {
                        b.CostPerPound = PowderBottleUtil.GetCostPerPound(item.LineItemCost, (float)bottleFee, item.Weight);
                    }
                }
            }
            return lineItemBottles;
        }

        public void SetCreatedDate(PowderBottle powderBottle)
        {
            _db.Entry(powderBottle).Property("CreatedDate").CurrentValue = DateTime.Now;
        }

        public void SetUpdatedDate(PowderBottle powderBottle)
        {
            _db.Entry(powderBottle).Property("UpdatedDate").CurrentValue = DateTime.Now;
        }

        public async Task<bool> BottleNumberLotNumberExists(PowderBottle powderBottle)
        {
            var bottles = await _db.PowderBottles
                .Where(p => p.BottleNumber == powderBottle.BottleNumber)
                .Where(p => p.LotNumber == powderBottle.LotNumber)
                .Where(p => p.BottleNumber != null)
                .ToListAsync();
            return bottles.Count() > 1;
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }

        public void ModifyRow(PowderBottle powderBottle)
        {
            var entry = _db.Entry(powderBottle);
            entry.State = EntityState.Modified;
        }
    }
}
