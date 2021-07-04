using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkItemRepo : IWorkItemRepo
    {
        private readonly EndlasNetDbContext _db;

        public WorkItemRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddWorkItem(WorkItem workItem)
        {
            await _db.WorkItems.AddAsync(workItem);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWorkItem(Guid? id)
        {
            var workItem = await _db.WorkItems.FirstOrDefaultAsync(w => w.WorkItemId == id);
            _db.WorkItems.Remove(workItem);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllPartInfo()
        {
            return await _db.StaticPartInfo.ToListAsync();
        }

        public async Task<IEnumerable<WorkItem>> GetAllWorkItems()
        {
            return await _db.WorkItems
                .Include(w => w.Work)
                .Include(w => w.StaticPartInfo)
                .Include(w => w.PartsForWork)
                .ToListAsync();
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfoWithoutJob()
        {
            return await _db.StaticPartInfo
                .Include(s => s.WorkItems)
                .Where(s => s.WorkItems.Count() == 0).ToListAsync();
        }

        public async Task<WorkItem> GetWorkItem(Guid? workItemId)
        {
            return await _db.WorkItems
                .Include(w => w.Work)
                .Include(w => w.StaticPartInfo)
                .Include(w => w.PartsForWork)
                .FirstOrDefaultAsync(w => w.WorkItemId == workItemId);
        }

        public async Task<Work> GetWork(Guid workId)
        {
            return await _db.Work
                .Include(w => w.WorkItems).ThenInclude(w => w.PartsForWork)
                .Include(w => w.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .FirstOrDefaultAsync(w => w.WorkId == workId);
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsForWork(Guid workId)
        {
            return await _db.WorkItems
                .Include(w => w.PartsForWork)
                .Include(w => w.StaticPartInfo)
                .Where(w => w.WorkId == workId)
                .ToListAsync();
        }
        public async Task UpdateWorkItem(WorkItem workItem)
        {
            var entry = _db.Entry(workItem);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<StaticPartInfo> GetStaticPartInfo(Guid id)
        {
            return await _db.StaticPartInfo.FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public async Task<IEnumerable<PartForWork>> GetPartsForJobsWithPartInfo(Guid? staticPartInfoId)
        {
            return await _db.PartsForWork
                .Include(p => p.WorkItem).ThenInclude(w => w.StaticPartInfo)
                .Where(p => p.WorkItem.StaticPartInfo.StaticPartInfoId == staticPartInfoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetExistingPartBatch(Guid workItemId)
        {
            return await _db.PartsForWork
                .Where(p => p.WorkItemId == workItemId)
                .OrderByDescending(p => p.Suffix)
                .ToListAsync();
        }
        public async Task AddPartForJob(PartForJob partForJob)
        {
            _db.Add(partForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetPartsForAWork(Guid workItemId)
        {
            return await _db.PartsForWork.Include(p => p.WorkItem).Where(p => p.WorkItem.WorkItemId == workItemId).ToListAsync();
        }

        public async Task DeletePartBatch(List<PartForWork> parts)
        {
            for(int i = 0; i < parts.Count(); i++)
            {
                _db.PartsForWork.Remove(parts[i]);
                await _db.SaveChangesAsync();
            }

        }
    }
}
