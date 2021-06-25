using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PartForJobRepo : IPartForJobRepo
    {
        private readonly EndlasNetDbContext _db;

        public PartForJobRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<List<PartForJob>> GetAllPartsForJobs()
        {
           return await _db.PartsForJobs
               .Include(p => p.WorkItem)
               .OrderByDescending(p => p.Suffix)
               .ToListAsync();
        }
        public async Task<PartForJob> GetPartForJobDetailsAsync(Guid? id)
        {
            return await _db.PartsForJobs
                .Include(p => p.WorkItem).ThenInclude(w => w.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task<PartForJob> GetPartForJob(Guid? id)
        {
            return await _db.PartsForJobs
            .Include(p => p.WorkItem)
            .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task<List<PartForJob>> GetExistingPartBatch(PartForJob partForJob)
        {
            return await _db.PartsForJobs
                   .OrderByDescending(p => p.Suffix)
                   .ToListAsync();
        }

        public async Task AddPartForJobAsync(PartForJob partForJob)
        {
            _db.Add(partForJob);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePartForJobConfirmedAsync(Guid id)
        {
            var partForJob = await _db.PartsForJobs.FindAsync(id);
            _db.PartsForJobs.Remove(partForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ConfirmPartForJobExistsAsync(Guid id)
        {
            return await _db.PartsForJobs.AnyAsync(e => e.PartForWorkId == id);
        }

        public async Task<PartForJob> DeletePartForJobAsync(Guid? id)
        {
            return await _db.PartsForJobs
                .Include(p => p.WorkItem)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task UpdatePartForJobAsync(PartForJob partForJob)
        {
            _db.Update(partForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartForJob>> GetBatch(string workItemId, string partInfoId)
        {
            var batch = await _db.PartsForJobs
                .Include(p => p.WorkItem)
                .ToListAsync();

            batch = (List<PartForJob>)batch.AsEnumerable();

            return batch.Where(p => p.WorkItem.ToString() == workItemId)
                .OrderByDescending(p => p.Suffix);
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo()
        {
            return await _db.StaticPartInfo
                .OrderByDescending(s => s.DrawingNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfoWithoutJob()
        {
            return await _db.StaticPartInfo
                .Include(s => s.WorkItems)
                .Where(s => s.WorkItems.Count() == 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await _db.Jobs
                .OrderByDescending(j => j.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsWithNoParts()
        {
            var list = await _db.Jobs
                .Include(j => j.WorkItems).ThenInclude(w => w.PartsForWork).ToListAsync();
 
 /*           foreach(Job job in list)
            {
                
                foreach(WorkItem workItem in job.WorkItems)
                {
                    workItem.PartsForWork = await _db.PartsForWork.Where(p => p.WorkItemId == workItem.WorkItemId).ToListAsync();
                }
            }*/
            List<Job> finalJobList = new List<Job>();
            foreach(Job job in list)
            {
                if(job.WorkItems != null)
                {
                    var workItems = job.WorkItems;

                    var selectWorkItems = workItems.Where(w => w.PartsForWork.Count() == 0).ToList();
                    if (selectWorkItems.Count() > 0)
                    {
                        finalJobList.Insert(0, job);
                    }
                }
               
            }
            return finalJobList;
        }

        public async Task<IEnumerable<PartForJob>> GetPartsForJobsWithPartInfo(Guid staticPartInfoId)
        {
            return await _db.PartsForJobs
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

        public async Task<IEnumerable<Job>> GetJobsWithParts()
        {
            var jobs = await _db.Jobs
                .Include(j => j.WorkItems)
                .ThenInclude(w => w.PartsForWork)
                .ToListAsync();

            foreach (Job job in jobs)
            {
                foreach(WorkItem workItem in job.WorkItems)
                {
                    foreach (PartForWork partForWork in workItem.PartsForWork)
                    {
                       
                    }
                }
             
            }
            return jobs;
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

        public async Task<IEnumerable<PartForWork>> GetWorkItemBatch(Guid workItemId)
        {
            return await _db.PartsForWork
                .Include(p => p.WorkItem)
                .Include(p => p.WorkItem).ThenInclude(w => w.StaticPartInfo)
                .Include(p => p.WorkItem).ThenInclude(w => w.Work)
                .Where(p => p.WorkItemId == workItemId)
                .ToListAsync();
        }

        public async Task<WorkItem> GetWorkItem(Guid? workItemId)
        {
            return await _db.WorkItems.FirstOrDefaultAsync(w => w.WorkItemId == workItemId);
        }
    }
}
