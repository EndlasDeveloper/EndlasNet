using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachiningToolForJobRepo : IMachiningToolForWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public MachiningToolForJobRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(MachiningToolForWork machiningToolForWork)
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
                    await _db.MachiningToolsForJobsBlanking.AddAsync(forBlanking);
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
                    await _db.MachiningToolsForJobsFinishing.AddAsync(forFinishing);
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
                    await _db.MachiningToolsForJobs.AddAsync(justJob);
                    break;
                default:
                    break;
            }
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Guid? id)
        {
            var machiningToolForJob = await _db.MachiningToolsForJobs.FindAsync(id);
            _db.MachiningToolsForJobs.Remove(machiningToolForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<MachiningToolForJob>> GetAllRows()
        {
            return await _db.MachiningToolsForJobs
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .OrderByDescending(m => m.DateUsed)
                .ToListAsync();
        }

        public async Task<MachiningToolForJob> GetRow(Guid? id)
        {
            return await _db.MachiningToolsForJobs
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task<MachiningToolForJob> GetRowNoTracking(Guid? id)
        {
            return await _db.MachiningToolsForJobs
                .AsNoTracking()
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.MachiningToolsForJobs
                          .AnyAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task UpdateRow(MachiningToolForJob machiningToolForJob)
        {
            var entry = _db.Entry(machiningToolForJob);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();   
        }
    }
}
