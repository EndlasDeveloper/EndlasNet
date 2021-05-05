﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class JobRepo : IJobRepo
    {
        private readonly EndlasNetDbContext _db;
        public JobRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(Job job)
        {
            await _db.Jobs.AddAsync(job);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Guid? id)
        {
            var job = await _db.Jobs
                .FirstOrDefaultAsync(j => j.WorkId == id);
            _db.Remove(job);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Job>> GetAllRows()
        {
            return await _db.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Quote)
                .OrderByDescending(j => j.EndlasNumber)
                .ToListAsync();
        }

        public async Task<Job> GetRow(Guid? id)
        {
            return await _db.Jobs
                .Include(j => j.Customer)
                .Include(j => j.User)
                .Include(j => j.Quote)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public async Task<Job> GetRowNoTracking(Guid? id)
        {
            return await _db.Jobs
                .AsNoTracking()
                .Include(j => j.Customer)
                .Include(j => j.User)
                .Include(j => j.Quote)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.Jobs
                        .AnyAsync(j => j.WorkId == id);
        }

        public async Task UpdateRow(Job job)
        {
            var entry = _db.Entry(job);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Job> FindRow(Guid? id)
        {
            return await _db.Jobs.FindAsync(id);
        }
        public async Task<IEnumerable<Work>> GetAllWork()
        {
            return await _db.Work
                .Include(w => w.User)
                .Include(w => w.Quote)
                .Include(w => w.Customer)
                .ToListAsync();
        }

        public async Task<Work> GetWork(Guid? id)
        {
            return await _db.Work
                .Include(w => w.User)
                .Include(w => w.Quote)
                .Include(w => w.Customer)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task DeleteWork(Guid? id)
        {
            var work = await _db.Work
                .FirstOrDefaultAsync(j => j.WorkId == id);
            _db.Remove(work);
            await _db.SaveChangesAsync();
        }

        public string GetWorkType(Work work)
        {
            return _db.Entry(work).Property("Discriminator").CurrentValue.ToString();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _db.Customers
                .OrderByDescending(c => c.CustomerName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quote>> GetAllQuotes()
        {
            return await _db.Quotes
                .OrderByDescending(q => q.EndlasNumber)
                .ToListAsync();
        }

        public async Task<Quote> GetQuote(Guid id)
        {
            return await _db.Quotes
                .FirstOrDefaultAsync(q => q.QuoteId == id);
        }
    }
}