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
                .OrderByDescending(p => p.PowderBottle.StaticPowderInfo.PowderName)
                .ToListAsync();
        }

        public async Task<PowderForPart> GetRow(Guid? id)
        {
            return await _db.PowderForParts
                            .Include(p => p.PowderBottle)
                            .Include(p => p.PartForWork)
                            .FirstOrDefaultAsync(p => p.PowderForPartId == id);
        }

        public async Task<PowderForPart> GetRowNoTracking(Guid? id)
        {
            return await _db.PowderForParts
                          .AsNoTracking()
                          .Include(p => p.PowderBottle)
                          .Include(p => p.PartForWork)
                          .FirstOrDefaultAsync(p => p.PowderForPartId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.PowderForParts
                           .AnyAsync(m => m.PowderForPartId == id);
        }

        public async Task UpdateRow(PowderForPart powderForPart)
        {    
            var entry = _db.Entry(powderForPart);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();  
        }
    }
}
