using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderBottleRepo : IPowderBottleRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderBottleRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<PowderBottle> GetPowder(Guid id)
        {
            return await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task UpdateRow(PowderBottle powderBottle)
        {
            var entry = _db.Entry(powderBottle);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<List<PowderBottle>> GetNamedPowders(string powderName)
        {
            return await _db.PowderBottles
                .Where(p => p.StaticPowderInfo.PowderName == powderName)
                .OrderByDescending(p => p.Weight)
                .ToListAsync();
        }

        public async Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId)
        {
            return await _db.PowderBottles
                .Include(l => l.StaticPowderInfo)
                .Include(l => l.LineItem)
                .Where(p => p.LineItemId == lineItemId)
                .OrderByDescending(p => p.StaticPowderInfo.PowderName)
                .ToListAsync();
        }

        public async Task<List<PowderBottle>> GetAllPowdersAsync()
        {
            return await _db.PowderBottles
                .Include(p => p.StaticPowderInfo)
                .Include(p => p.LineItem)
                .Include(p => p.LineItem.PowderOrder)
                .OrderByDescending(p => p.StaticPowderInfo.PowderName)
                .ToListAsync();
        }

        public async Task<PowderBottle> GetRow(Guid? id)
        {
            return await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<IEnumerable<PowderBottle>> GetAllRows()
        {
            return await _db.PowderBottles
                .OrderByDescending(p => p.StaticPowderInfo.PowderName)
                .ToListAsync();
        }

        public async Task AddRow(PowderBottle powderBottle)
        {
            await _db.PowderBottles.AddAsync(powderBottle);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRow(Guid id)
        {
            _db.PowderBottles
                .Remove(await _db.PowderBottles.FirstOrDefaultAsync(p => p.PowderBottleId == id));
        }

        public async Task DeleteRow(Guid? id)
        {
            var powder = await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
            _db.PowderBottles.Remove(powder);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.PowderBottles
                .AnyAsync(p => p.PowderBottleId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.PowderBottles
                .AsNoTracking().
                FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<PowderBottle> FindRow(Guid? id)
        {
            return await _db.PowderBottles.FindAsync(id);
        }

        public object OrderByDescending()
        {
            throw new NotImplementedException();
        }
    }
}
