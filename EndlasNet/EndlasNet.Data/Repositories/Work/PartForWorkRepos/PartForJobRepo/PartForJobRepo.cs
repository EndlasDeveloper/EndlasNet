﻿using Microsoft.EntityFrameworkCore;
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
               .Include(p => p.StaticPartInfo)
               .Include(p => p.User)
               .Include(p => p.Work)
               .OrderByDescending(p => p.Suffix)
               .ToListAsync();
        }
        public async Task<PartForJob> GetPartForJobDetailsAsync(Guid? id)
        {
            return await _db.PartsForJobs
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task<PartForJob> GetPartForJob(Guid? id)
        {
            return await _db.PartsForJobs
            .Include(p => p.StaticPartInfo)
            .Include(p => p.User)
            .Include(p => p.Work)
            .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task<List<PartForJob>> GetExistingPartBatch(PartForJob partForJob)
        {
            return await _db.PartsForJobs
                   .Where(p => p.WorkId == partForJob.WorkId)
                   .Where(p => p.StaticPartInfoId == partForJob.StaticPartInfoId)
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

        public async Task<PartForJob> DeleteCustomerAsync(Guid? id)
        {
            return await _db.PartsForJobs
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public async Task UpdatePartForJobAsync(PartForJob partForJob)
        {
            _db.Update(partForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartForJob>> GetBatch(string workId, string partInfoId)
        {
            var batch = await _db.PartsForJobs
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .ToListAsync();

            batch = (List<PartForJob>)batch.AsEnumerable();

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

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await _db.Jobs
                .OrderByDescending(j => j.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartForJob>> GetPartsForJobsWithPartInfo(Guid staticPartInfoId)
        {
            return await _db.PartsForJobs
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
    }
}
