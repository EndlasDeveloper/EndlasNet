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

        public async Task AddRow(object obj)
        {
            try
            {
                var powderForPart = (PowderForPart)obj;
                await _db.PowderForParts.AddAsync(powderForPart);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var powderForPart = await _db.PowderForParts.FindAsync(id);
            _db.PowderForParts.Remove(powderForPart);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            var rows = _db.PowderForParts
                .Include(p => p.PowderBottle)
                .Include(p => p.PowderBottle.StaticPowderInfo)
                .Include(p => p.PartForWork);
            return await rows.OrderBy(p => p.PowderBottle.StaticPowderInfo.PowderName).ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.PowderForParts
                            .Include(p => p.PowderBottle)
                            .Include(p => p.PartForWork)
                            .FirstOrDefaultAsync(p => p.PowderForPartId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
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

        public async Task UpdateRow(object obj)
        {
            try
            {
                var powderForPart = (PowderForPart)obj;
                var entry = _db.Entry(powderForPart);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }
    }
}
