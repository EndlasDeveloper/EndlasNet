using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderOrderRepo
    {
        private readonly EndlasNetDbContext db;
        public PowderOrderRepo(EndlasNetDbContext db)
        {
            this.db = db;
        }

        public async Task AddRow(object obj)
        {
            try
            {
                var powderOrder = (PowderOrder)obj;
                await db.PowderOrders.AddAsync(powderOrder);
                await db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
            
        }

        public Task DeleteRow(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await db.PowderOrders.ToListAsync();
        }


        public async Task<PowderOrder> GetRow(Guid? powderOrderId)
        {
            return await db.PowderOrders
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

