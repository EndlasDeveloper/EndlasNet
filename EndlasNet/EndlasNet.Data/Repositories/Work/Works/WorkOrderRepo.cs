using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkOrderRepo : IWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public WorkOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(object obj)
        {
            try
            {
                await _db.WorkOrders.AddAsync((WorkOrder)obj);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var workOrder = await _db.WorkOrders
                .FirstOrDefaultAsync(j => j.WorkId == id);
            _db.Remove(workOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.WorkOrders.ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.WorkOrders
                .Include(j => j.Customer)
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.WorkOrders
                .AsNoTracking()
                .Include(j => j.Customer)
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.WorkOrders
                        .AnyAsync(j => j.WorkId == id);
        }

        public async Task UpdateRow(object obj)
        {
            try
            {
                var entry = _db.Entry((User)obj);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }
    }
}
