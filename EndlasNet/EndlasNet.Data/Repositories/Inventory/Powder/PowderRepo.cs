using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderRepo : IRepository, IPowderRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<PowderBottle> GetPowder(Guid id)
        {
            return await _db.PowderBottles.FirstOrDefaultAsync(p => p.PowderBottleId == id);
        }

        public async Task<List<PowderBottle>> GetPowder(string powderName)
        {
            return await _db.PowderBottles.Where(p => p.StaticPowderInfo.PowderName == powderName).ToListAsync();
        }

        public async Task AddPowder(PowderBottle powder)
        {
            await _db.PowderBottles.AddAsync(powder);
            await _db.SaveChangesAsync();
        }

        public async Task<List<PowderBottle>> GetLineItemPowders(Guid lineItemId)
        {
            return await _db.PowderBottles
                .Where(p => p.LineItemId == lineItemId)
                .ToListAsync();
        }

        public async Task<List<PowderBottle>> GetAllPowdersAsync()
        {
            return await _db.PowderBottles.ToListAsync();
        }

        public Task<object> GetRow(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetAllRows()
        {
            throw new NotImplementedException();
        }

        public Task AddRow(object obj)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRow(object obj)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRow(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RowExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetRowNoTracking(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}
