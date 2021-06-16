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
                .Include(p => p.PowderBottle)
                .Include(p => p.PowderBottle.StaticPowderInfo)
                .Include(p => p.PartForWork)
                .Include(p => p.User)
                .OrderByDescending(p => p.PowderBottle.StaticPowderInfo.PowderName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Work>> GetAllWorkWithBottles()
        {
            return await _db.Work
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
            return await _db.PartsForWork
                .Where(p => p.WorkId == workId).ToListAsync();
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
                .Include(p => p.PowderBottle)
                .Include(p => p.PowderBottle.StaticPowderInfo)
                .Include(p => p.PartForWork)
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
            return await _db.Work
                .FirstOrDefaultAsync(w => w.WorkId == id);
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
