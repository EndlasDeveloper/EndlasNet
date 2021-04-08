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
            this._db = db;
        }

        public async Task AddRow(object obj)
        {
            try
            {
                var powderOrder = (PowderOrder)obj;
                await _db.PowderOrders.AddAsync(powderOrder);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
            
        }

        public async Task DeleteRow(Guid? id)
        {
            var powderOrder = await _db.PowderOrders
              .FirstOrDefaultAsync(p => p.PowderOrderId == id);
            _db.PowderOrders.Remove(powderOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.PowderOrders
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

        public Task<bool> RowExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRow(object obj)
        {
            throw new NotImplementedException();
        }
    }
}

