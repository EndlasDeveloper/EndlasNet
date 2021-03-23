﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class LineItemRepo : ILineItemRepo
    {
        private readonly EndlasNetDbContext _db;
        public LineItemRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<LineItem>> GetLineItems(Guid powderOrderId)
        {
            var lineItems = await _db.LineItems
                .Include(l => l.PowderOrder)
                .Include(l => l.StaticPowderInfo)
                .Where(l => l.PowderOrderId == powderOrderId).ToListAsync();

            return lineItems;
        }

        public async Task<LineItem> GetLineItem(Guid lineItemId)
        {
            return await _db.LineItems
                .FirstOrDefaultAsync(l => l.LineItemId == lineItemId);
        }

    }
}
