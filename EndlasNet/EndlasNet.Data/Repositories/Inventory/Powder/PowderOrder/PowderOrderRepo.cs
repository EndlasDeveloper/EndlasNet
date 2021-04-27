using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderOrderRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(PowderOrder powderOrder)
        {
            await _db.PowderOrders.AddAsync(powderOrder);
            await _db.SaveChangesAsync();          
        }

        public async Task DeleteRow(Guid? id)
        {
            var powderOrder = await _db.PowderOrders.FindAsync(id);
            _db.PowderOrders.Remove(powderOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PowderOrder>> GetAllRows()
        {
            return await _db.PowderOrders
                .Include(p => p.Vendor)
                .Include(p => p.LineItems)
                .OrderByDescending(p => p.PurchaseOrderNum)
                .ToListAsync();
        }


        public async Task<PowderOrder> GetRow(Guid? powderOrderId)
        {
            return await _db.PowderOrders
                           .FirstOrDefaultAsync(p => p.PowderOrderId == powderOrderId);
        }

        public Task<object> GetRowNoTracking(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool RowExists(Guid id)
        {
            return _db.PowderOrders.Any(e => e.PowderOrderId == id);
        }

        public async Task UpdateRow(PowderOrder powderOrder)
        {
            var entry = _db.Entry(powderOrder);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}

