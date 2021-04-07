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

        public async Task<object> GetPowder(Guid id)
        {
            return await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<List<PowderBottle>> GetNamedPowders(string powderName)
        {
            return await _db.PowderBottles
                .Where(p => p.StaticPowderInfo.PowderName == powderName)
                .ToListAsync();
        }

        public async Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId)
        {
            return await _db.PowderBottles
                .Include(l => l.StaticPowderInfo)
                .Include(l => l.LineItem)
                .Where(p => p.LineItemId == lineItemId)
                .ToListAsync();
        }

        public async Task<List<PowderBottle>> GetAllPowdersAsync()
        {
            return await _db.PowderBottles
                .Include(p => p.StaticPowderInfo)
                .ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.PowderBottles
                .FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.PowderBottles.ToListAsync();
        }

        public async Task AddRow(object obj)
        {
            try
            {
                var powderBottle = (PowderBottle)obj;
                await _db.PowderBottles.AddAsync(powderBottle);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public Task UpdateRow(object obj)
        {
            throw new NotImplementedException();
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

        public async Task<object> FindRow(Guid? id)
        {
            return await _db.PowderBottles.FindAsync(id);
        }
    }
}
