using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class LineItemRepo
    {
        private readonly EndlasNetDbContext _db;
        public LineItemRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<LineItem>> GetLineItems(Guid powderOrderId)
        {
             return await _db.LineItems
                .Where(l => l.PowderOrderId == powderOrderId)
                .ToListAsync();
        }

    }
}
