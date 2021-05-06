using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkOrderRepo : IMachiningToolForWorkOrderRepo
    {
        private readonly EndlasNetDbContext _db;

        public MachiningToolForWorkOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(MachiningToolForWork machiningToolForWork)
        {
            try
            {
                switch (machiningToolForWork.MachiningType)
                {
                    case MachiningTypes.Blanking:
                        var forBlanking = new MachiningToolForWorkOrderBlanking
                        {
                            MachiningToolForWorkId = machiningToolForWork.MachiningToolForWorkId,
                            MachiningTool = machiningToolForWork.MachiningTool,
                            MachiningToolId = machiningToolForWork.MachiningToolId,
                            MachiningType = machiningToolForWork.MachiningType,
                            Comment = machiningToolForWork.Comment,
                            WorkId = machiningToolForWork.WorkId,
                            Work = machiningToolForWork.Work,
                            DateUsed = machiningToolForWork.DateUsed,
                            UserId = machiningToolForWork.UserId,
                            User = machiningToolForWork.User
                        };
                        await _db.MachiningToolsForWorkOrdersBlanking.AddAsync(forBlanking);
                        break;
                    case MachiningTypes.Finishing:
                        var forFinishing = new MachiningToolForWorkOrderFinishing
                        {
                            MachiningToolForWorkId = machiningToolForWork.MachiningToolForWorkId,
                            MachiningTool = machiningToolForWork.MachiningTool,
                            MachiningToolId = machiningToolForWork.MachiningToolId,
                            MachiningType = machiningToolForWork.MachiningType,
                            Comment = machiningToolForWork.Comment,
                            WorkId = machiningToolForWork.WorkId,
                            Work = machiningToolForWork.Work,
                            DateUsed = machiningToolForWork.DateUsed,
                            UserId = machiningToolForWork.UserId,
                            User = machiningToolForWork.User
                        };
                        await _db.MachiningToolsForWorkOrdersFinishing.AddAsync(forFinishing);
                        break;
                    case MachiningTypes.None:
                        var justWorkOrder = new MachiningToolForWorkOrder
                        {
                            MachiningToolForWorkId = machiningToolForWork.MachiningToolForWorkId,
                            MachiningTool = machiningToolForWork.MachiningTool,
                            MachiningToolId = machiningToolForWork.MachiningToolId,
                            MachiningType = machiningToolForWork.MachiningType,
                            Comment = machiningToolForWork.Comment,
                            WorkId = machiningToolForWork.WorkId,
                            Work = machiningToolForWork.Work,
                            DateUsed = machiningToolForWork.DateUsed,
                            UserId = machiningToolForWork.UserId,
                            User = machiningToolForWork.User
                        };
                        await _db.MachiningToolsForWorkOrders.AddAsync(justWorkOrder);
                        break;
                    default:
                        break;
                }
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var machiningToolForJob = await _db.MachiningToolsForWorkOrders.FindAsync(id);
            _db.MachiningToolsForWorkOrders.Remove(machiningToolForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<MachiningToolForWork>> GetAllRows()
        {
            return await _db.MachiningToolsForWorkOrders
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .OrderByDescending(m => m.DateUsed)
                .ToListAsync();
        }

        public async Task<MachiningToolForWork> GetRow(Guid? id)
        {
            return await _db.MachiningToolsForWorkOrders
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task<MachiningToolForWork> GetRowNoTracking(Guid? id)
        {
            return await _db.MachiningToolsForWorkOrders
                .AsNoTracking()
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }


        public async Task<bool> RowExists(Guid id)
        {
            return await _db.MachiningToolsForWorkOrders
                          .AnyAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task UpdateRow(MachiningToolForWork machiningToolForWork)
        {
            var entry = _db.Entry(machiningToolForWork);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
