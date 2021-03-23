using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderRepo : IPowderRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<Powder> GetPowder(Guid id)
        {
            return await _db.Powders.FirstOrDefaultAsync(p => p.PowderId == id);
        }

        public async Task<List<Powder>> GetPowder(string powderName)
        {
            return await _db.Powders.Where(p => p.StaticPowderInfo.PowderName == powderName).ToListAsync();
        }

        public async Task AddPowder(Powder powder)
        {
            await _db.Powders.AddAsync(powder);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Powder>> GetLineItemPowders(Guid lineItemId)
        {
            return await _db.Powders
                .Where(p => p.LineItemId == lineItemId)
                .ToListAsync();
        }

        public async Task<List<Powder>> GetAllPowdersAsync()
        {
            return await _db.Powders.ToListAsync();
        }
    }
}
