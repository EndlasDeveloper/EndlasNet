using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PowderOrderRepo
    {
        private readonly EndlasNetDbContext db;
        public PowderOrderRepo(EndlasNetDbContext db)
        {
            this.db = db;
        }

        public async Task Add(PowderOrder powderOrder)
        {
            await db.PowderOrders.AddAsync(powderOrder);
            await db.SaveChangesAsync();
        }

       
    }
}

