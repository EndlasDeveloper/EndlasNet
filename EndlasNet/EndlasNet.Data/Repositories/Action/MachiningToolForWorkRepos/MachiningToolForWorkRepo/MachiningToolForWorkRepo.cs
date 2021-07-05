using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkRepo : IMachiningToolForWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public MachiningToolForWorkRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(MachiningToolForJob machiningToolForWork)
        {
            switch (machiningToolForWork.MachiningType)
            {
                case MachiningTypes.Blanking:
                    MachiningToolForJobBlanking forBlanking = new MachiningToolForJobBlanking
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
                    //await _db.MachiningToolsForJobsBlanking.AddAsync(forBlanking);
                    break;
                case MachiningTypes.Finishing:
                    MachiningToolForJobFinishing forFinishing = new MachiningToolForJobFinishing
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
                    //await _db.MachiningToolsForJobsFinishing.AddAsync(forFinishing);
                    break;
                case MachiningTypes.None:
                    MachiningToolForJob justJob = new MachiningToolForJob
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
                    //await _db.MachiningToolsForJobs.AddAsync(justJob);
                    break;
                default:
                    break;
            }
            await _db.SaveChangesAsync();
        }

        public async Task AddMachiningToolForWork(MachiningToolForWork toolForWork)
        {
            _db.MachiningToolsForWork.Add(toolForWork);
            await _db.SaveChangesAsync();
        }

        public async Task AddRow(MachiningToolForWorkOrder machiningToolForWork)
        {
            _db.MachiningToolsForWork.Add(machiningToolForWork);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRow(MachiningToolForWork machiningToolForWork)
        {
            var entry = _db.Entry(machiningToolForWork);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteMachiningToolForWork(Guid? id)
        {
            var machiningToolForWork = await _db.MachiningToolsForWork.FindAsync(id);
            _db.MachiningToolsForWork.Remove(machiningToolForWork);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<MachiningToolForWork>> GetAllMachiningToolsForWork()
        {
            var rows = await _db.MachiningToolsForWork
                .Include(m => m.MachiningTool)
                .OrderByDescending(m => m.DateUsed)
                .ToListAsync();
            foreach(MachiningToolForWork machiningToolForWork in rows)
            {
                machiningToolForWork.User = await _db.Users.FirstOrDefaultAsync(u => u.UserId == machiningToolForWork.UserId);
                machiningToolForWork.Work = await _db.Work.FirstOrDefaultAsync(w => w.WorkId == machiningToolForWork.WorkId);
            }
            return rows;
        }

        public async Task<MachiningToolForWork> GetMachiningToolForWork(Guid? id)
        {
            var row = await _db.MachiningToolsForWork
                .Include(m => m.MachiningTool)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
            row.Work = await _db.Work.FirstOrDefaultAsync(w => w.WorkId == row.WorkId);
            row.User = await _db.Users.FirstOrDefaultAsync(u => u.UserId == row.UserId);
            return row;
        }

        public async Task<MachiningToolForWork> GetMachiningToolForWorkNoTracking(Guid? id)
        {
            return await _db.MachiningToolsForWork
                .AsNoTracking()
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }



        public async Task<bool> MachiningToolForWorkExists(Guid id)
        {
            return await _db.MachiningToolsForWork
                          .AnyAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await _db.Jobs
                .OrderByDescending(j => j.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<MachiningTool>> GetAllMachiningTools()
        {
            return await _db.MachiningTools
                .OrderByDescending(m => m.PurchaseOrderNum)
                .ToListAsync();
        }

        public async Task<Work> GetWork(Guid id)
        {
            return await _db.Work.FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<MachiningTool> GetMachiningTool(Guid id)
        {
            return await _db.MachiningTools
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.UserId == id);
        }


/*        public async Task<MachiningToolForWorkOrder> GetWorkOrder(Guid id)
        {
            return await _db.MachiningToolsForWorkOrders
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }*/

        public async Task<IEnumerable<WorkOrder>> GetAllWorkOrders()
        {
            return await _db.WorkOrders
                .OrderByDescending(w => w.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<MachiningTool>> GetAvailableTools()
        {
            return await _db.MachiningTools
                .Where(m => m.ToolCount > 0)
                .OrderByDescending(m => m.ToolType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Work>> GetAllWork()
        {
            return await _db.Work
                .OrderByDescending(w => w.EndlasNumber)
                .ToListAsync();
        }

        public async Task UpdateMachiningTool(MachiningTool machiningTool)
        {
            var entry = _db.Entry(machiningTool);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


    }
}
