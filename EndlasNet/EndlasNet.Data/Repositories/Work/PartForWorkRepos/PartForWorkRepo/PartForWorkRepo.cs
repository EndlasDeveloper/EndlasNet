﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PartForWorkRepo : IPartForWorkRepo
    {
        private readonly EndlasNetDbContext _db;
        public PartForWorkRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddPartForWork(PartForWork partForWork)
        {
            await _db.PartsForWork.AddAsync(partForWork);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePartForWork(PartForWork partForWork)
        {
            _db.Remove(partForWork);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartForJob>> GetAllPartsForJobs()
        {
            return await _db.PartsForJobs
                .OrderByDescending(p => p.Suffix
                ).ToListAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetAllPartsForWork()
        {
            return await _db.PartsForWork
                .OrderByDescending(p => p.Suffix)
                .ToListAsync();
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo()
        {
            return await _db.StaticPartInfo
                .OrderByDescending(s => s.DrawingNumber)
                .ToListAsync();
        }

        public async Task<PartForWork> GetPartForWork(Guid? id)
        {
            return await _db.PartsForWork
               .Include(p => p.StaticPartInfo)
               .Include(p => p.User)
               .Include(p => p.Work)
               .FirstOrDefaultAsync(m => m.PartForWorkId == id);
        }

        public Task<IEnumerable<PartForWorkOrder>> GetPartForWorkOrders()
        {
            throw new NotImplementedException();
        }

        public string GetWorkType(PartForWork partForWork)
        {
            return _db.Entry(partForWork).Property("Discriminator").CurrentValue.ToString();
        }

        public bool PartForWorkExists(Guid id)
        {
            return _db.PartsForWork.Any(e => e.PartForWorkId == id);
        }

        public async Task UpdatePartForWork(PartForWork partForWork)
        {
            _db.Update(partForWork);
            await _db.SaveChangesAsync();
        }
    }
}