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
                .Include(p => p.PartForWorkImg)
                .OrderByDescending(p => p.Suffix)
                .ToListAsync();
        }
        public async Task<PartForWorkOrder> GetPartForWorkOrderDetailsAsync(Guid? id)
        {
            return await _db.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .Include(p => p.PartForWorkImg)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task<List<PartForWorkOrder>> GetExistingPartBatch(PartForWorkOrder partForWorkOrder)
        {
            return await _db.PartsForWorkOrders
                   .Where(p => p.WorkId == partForWorkOrder.WorkId)
                   .Where(p => p.StaticPartInfoId == partForWorkOrder.StaticPartInfoId)
                   .Include(p => p.StaticPartInfo)
                   .Include(p => p.Work)
                   .Include(p => p.PartForWorkImg)
                   .OrderByDescending(p => p.Suffix)
                   .ToListAsync();
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersWithParts()
        {
            var workOrders = await _db.WorkOrders
                            .Include(j => j.WorkItems)
                            .ThenInclude(w => w.PartsForWork)
                            .ToListAsync();

            foreach (WorkOrder workOrder in workOrders)
            {
                foreach (WorkItem workItem in workOrder.WorkItems)
                {
                    foreach (PartForWork partForWork in workItem.PartsForWork)
                    {
                        partForWork.StaticPartInfo = await _db.StaticPartInfo
                            .FirstOrDefaultAsync(s => s.StaticPartInfoId == partForWork.StaticPartInfoId);
                    }
                }

            }
            return workOrders;
        }

        public async Task<IEnumerable<WorkOrder>> GetWorkOrdersWithNoParts()
        {
            List<WorkOrder> workOrders = new List<WorkOrder>();
            var list = await _db.WorkOrders.Include(w => w.WorkItems).ThenInclude(w => w.PartsForWork)
                .Where(j => j.WorkItems.Count() > 0)
                .ToListAsync();
            foreach(WorkOrder workOrder in list)
            {
                foreach(WorkItem workItem in workOrder.WorkItems)
                {
                    if(workItem.PartsForWork != null && workItem.PartsForWork.Count() > 0)
                    {
                        workOrders.Insert(0, workOrder);
                    }
                }
            }
            return await _db.WorkOrders.Include(w => w.WorkItems).ThenInclude(w => w.PartsForWork)
                .Where(j => j.WorkItems.Count() > 0)
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
                .Include(p => p.PartForWorkImg)
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
                .Include(p => p.PartForWorkImg)
                .OrderBy(p => p.Suffix)
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
                .Include(p => p.PartForWorkImg)
                .FirstOrDefaultAsync(p => p.PartForWorkId == id);
        }

        public async Task<PartForWorkOrder> GetPartForWorkOrderAsync(Guid? id)
        {
            return await _db.PartsForWorkOrders
                           .Include(p => p.StaticPartInfo)
                           .Include(p => p.User)
                           .Include(p => p.Work)
                           .Include(p => p.PartForWorkImg)
                           .FirstOrDefaultAsync(p => p.PartForWorkId == id);
        }
        public async Task<IEnumerable<PartForWorkImg>> GetAllPartForWorkImgs()
        {
            return await _db.PartForWorkImages.ToListAsync();
        }

        public async Task<PartForWorkImg> GetPartForWorkImg(Guid id)
        {
            return await _db.PartForWorkImages
                .FirstOrDefaultAsync(p => p.PartForWorkImgId == id);
        }
        public async Task UpdatePartForWorkImg(PartForWorkImg partForWorkImg)
        {
            _db.Update(partForWorkImg);
            await _db.SaveChangesAsync();
        }
        public async Task DeletePartForWorkImg(PartForWorkImg partForWorkImg)
        {
            _db.Remove(partForWorkImg);
            await _db.SaveChangesAsync();
        }
        public async Task AddPartForWorkImg(PartForWorkImg partForWorkImg)
        {
            await _db.AddAsync(partForWorkImg);
            await _db.SaveChangesAsync();
        }
    }
}
