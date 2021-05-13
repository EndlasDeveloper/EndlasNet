using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PartForWorkOrderRepo : IPartForWorkOrderRepo
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
                .OrderByDescending(p => p.Suffix)
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
                   .OrderByDescending(p => p.Suffix)
                   .ToListAsync();
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersWithParts()
        {
            var workOrders = await _db.WorkOrders
                .Include(j => j.PartsForWork)
                .ToListAsync();

            foreach (WorkOrder order in workOrders)
            {
                foreach (PartForWork partForWork in order.PartsForWork)
                {
                    partForWork.StaticPartInfo = await _db.StaticPartInfo
                        .FirstOrDefaultAsync(s => s.StaticPartInfoId == partForWork.StaticPartInfoId);
                }
            }
            return workOrders;
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersWithNoParts()
        {
            return await _db.WorkOrders
                .Where(j => j.PartsForWork.Count() == 0)
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

        public async Task<PartForWorkOrder> GetWorkOrderForDeleteAsync(Guid? id)
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
            var batch = await _db.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .ToListAsync();

            batch = (List<PartForWorkOrder>)batch.AsEnumerable();

            return batch.Where(p => p.WorkId.ToString() == workId)
                .Where(p => p.StaticPartInfoId.ToString() == partInfoId)
                .OrderByDescending(p => p.Suffix);
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo()
        {
            return await _db.StaticPartInfo
                .OrderByDescending(s => s.DrawingNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<WorkOrder>> GetAllWorkOrders()
        {
            return await _db.WorkOrders
                .OrderByDescending(j => j.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartForWorkOrder>> GetPartsForWorkOrdersWithPartInfo(Guid staticPartInfoId)
        {
            return await _db.PartsForWorkOrders
                          .Where(p => p.StaticPartInfoId == staticPartInfoId)
                          .ToListAsync();
        }
        public async Task<StaticPartInfo> GetStaticPartInfo(Guid id)
        {
            return await _db.StaticPartInfo
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public async Task<Work> GetWork(Guid id)
        {
            return await _db.Work
                .FirstOrDefaultAsync(s => s.WorkId == id);
        }

        public async Task DeletePartForWorkOrderAsync(PartForWorkOrder partForWorkOrder)
        {
            _db.Remove(partForWorkOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<PartForWorkOrder> GetPartForWorkOrder(Guid id)
        {
            return await _db.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.Work)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.WorkId == id);
        }

        public async Task<PartForWorkOrder> GetPartForWorkOrderAsync(Guid? id)
        {
            return await _db.PartsForWorkOrders
                           .Include(p => p.StaticPartInfo)
                           .Include(p => p.User)
                           .Include(p => p.Work)
                           .FirstOrDefaultAsync(p => p.PartForWorkId == id);
        }
    }
}
