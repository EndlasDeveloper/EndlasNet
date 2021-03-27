using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PartForWorkOrderRepo
    {
        private readonly EndlasNetDbContext _db;

        public PartForWorkOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<List<PartForWorkOrder>> GetAllPartsForWorkOrdersAsync()
        {
            return await _db.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .ToListAsync();
        }
        public async Task<PartForWorkOrder> GetPartForWorkOrderDetailsAsync(Guid? id)
        {
            return await _db.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task<List<PartForWorkOrder>> GetExistingPartBatch(PartForWorkOrder partForWorkOrder)
        {
            return await _db.PartsForWorkOrders
                   .Where(p => p.WorkId == partForWorkOrder.WorkId)
                   .Where(p => p.StaticPartInfoId == partForWorkOrder.StaticPartInfoId)
                   .ToListAsync();
        }

        public async Task AddPartForWorkOrderAsync(PartForWorkOrder partForWorkOrder)
        {
            _db.Add(partForWorkOrder);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePartForWorkOrderConfirmedAsync(Guid id)
        {
            var partForWorkOrder = await _db.PartsForWorkOrders.FindAsync(id);
            _db.PartsForWorkOrders.Remove(partForWorkOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ConfirmPartForWorkOrderExistsAsync(Guid id)
        {
            return await _db.PartsForWorkOrders.AnyAsync(e => e.PartForWorkId == id);
        }

        public async Task<PartForWorkOrder> GetCustomerForDeleteAsync(Guid? id)
        {
            return await _db.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task UpdatePartForWorkOrderAsync(PartForWorkOrder partForWorkOrder)
        {
            _db.Update(partForWorkOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartForWorkOrder>> GetBatch(string workId, string partInfoId)
        {
            var batch = await _db.PartsForWorkOrders.Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .ToListAsync();
            batch = (List<PartForWorkOrder>)batch.AsEnumerable();
            return batch.Where(p => p.WorkId.ToString() == workId).Where(p => p.StaticPartInfoId.ToString() == partInfoId);
        }
    }
}
