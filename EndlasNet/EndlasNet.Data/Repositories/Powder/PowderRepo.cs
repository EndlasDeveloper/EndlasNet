using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderRepo
    {
        private readonly EndlasNetDbContext _db;
        public PowderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<List<Powder>> GetLineItemPowders(Guid lineItemId)
        {
            return await _db.Powders
                .Where(p => p.LineItemId == lineItemId)
                .ToListAsync();
        }
    }
}
