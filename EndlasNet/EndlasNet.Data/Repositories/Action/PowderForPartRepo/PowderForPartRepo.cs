using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderForPartRepo : IPowderForPartRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderForPartRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(PowderForPart powderForPart)
        {
            await _db.PowderForParts.AddAsync(powderForPart);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Guid? id)
        {
            var powderForPart = await _db.PowderForParts.FindAsync(id);
            _db.PowderForParts.Remove(powderForPart);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PowderForPart>> GetAllRows()
        {
            return await _db.PowderForParts
                .Include(p => p.PartForWork).ThenInclude(p => p.WorkItem).ThenInclude(w => w.StaticPartInfo)
                .Include(p => p.PowderBottle).ThenInclude(p => p.StaticPowderInfo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Work>> GetAllWorkWithBottles()
        {
            var list = await _db.Work.Include(w => w.WorkItems)
                .ToListAsync();
            for(int i = 0; i < list.Count; i++)
            {
                foreach(WorkItem workItem in list[i].WorkItems)
                {
                    workItem.PartsForWork = await _db.PartsForWork.Where(p => p.WorkItemId == workItem.WorkItemId).ToListAsync();
                }
            }
            return list;
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsFromWork(Guid workId)
        {
            return await _db.WorkItems
                .Include(w => w.StaticPartInfo)
                .Where(w => w.WorkId == workId)
                .ToListAsync();
        }


        public async Task<IEnumerable<PowderBottle>> GetBottlesWithPowder(float threshold)
        {
            return await _db.PowderBottles
                .Where(p => p.Weight > threshold)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetPartsForWork()
        {
            return await _db.PartsForWork
                .OrderByDescending(p => p.Suffix)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetPartsForWorkSingle(Guid workId)
        {
            var work = await _db.Work.Include(w => w.WorkItems).FirstOrDefaultAsync(w => w.WorkId == workId);
            var workItems = work.WorkItems.ToList();
            List<PartForWork> list = new List<PartForWork>();
            for (int i = 0; i < workItems.Count(); i++)
            {
                workItems[i] = await _db.WorkItems.Include(w => w.PartsForWork).FirstOrDefaultAsync(w => w.WorkItemId == workItems[i].WorkItemId);
                
            }
            foreach(WorkItem workItem in workItems)
            {
                foreach(PartForWork part in workItem.PartsForWork)
                {
                    list.Insert(0, part);
                }
            }
            return list;
        }

        public async Task<PowderBottle> GetPowderBottle(Guid id)
        {
            return await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<PowderForPart> GetPowderForPartWithBottles(Guid id)
        {
            return await _db.PowderForParts
                .Include(p => p.PowderBottle)
                .FirstOrDefaultAsync(p => p.PowderForPartId == id);
        }

        public async Task<PowderForPart> GetRow(Guid? id)
        {
            return await _db.PowderForParts
                .Include(p => p.PowderBottle).ThenInclude(p => p.StaticPowderInfo)
                .Include(p => p.PartForWork).ThenInclude(p => p.WorkItem).ThenInclude(w => w.StaticPartInfo)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PowderForPartId == id);
        }

        public async Task<PowderForPart> GetRowNoTracking(Guid? id)
        {
            return await _db.PowderForParts
                .AsNoTracking()
                .Include(p => p.PowderBottle)
                .Include(p => p.PartForWork)
                .Include(p => p.PowderBottle.StaticPowderInfo)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PowderForPartId == id);
        }

        public async Task<StaticPartInfo> GetStaticPartInfo(Guid id)
        {
            return await _db.StaticPartInfo
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public async Task<StaticPowderInfo> GetStaticPowderInfo(Guid id)
        {
            return await _db.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == id);
        }

        public async Task<Work> GetWork(Guid id)
        {
            var work = await _db.Work.Include(w => w.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .FirstOrDefaultAsync(w => w.WorkId == id);
            var workItems = work.WorkItems.ToList();
            for(int i = 0; i < work.WorkItems.Count(); i++)
            {
                workItems[i] = await _db.WorkItems.Include(w => w.PartsForWork).FirstOrDefaultAsync(w => w.WorkItemId == workItems[i].WorkItemId);
            }
            work.WorkItems = workItems;
            return work;
        }

        public async Task<IEnumerable<PowderBottle>> GetWorkItemBottles(Guid workItemId)
        {
            var workItem = await _db.WorkItems.Include(w => w.PartsForWork).FirstOrDefaultAsync(w => w.WorkItemId == workItemId);
            var list = workItem.PartsForWork.ToList();
            
            return (IEnumerable<PowderBottle>)workItem.PartsForWork.ToList();
        }

        public bool PowderForPartExists(Guid id)
        {
            return _db.PowderForParts.Any(e => e.PowderForPartId == id);
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.PowderForParts
                           .AnyAsync(m => m.PowderForPartId == id);
        }

        public async Task UpdatePowderBottle(PowderBottle powderBottle)
        {
            var entry = _db.Entry(powderBottle);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRow(PowderForPart powderForPart)
        {    
            var entry = _db.Entry(powderForPart);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();  
        }
    }
}
