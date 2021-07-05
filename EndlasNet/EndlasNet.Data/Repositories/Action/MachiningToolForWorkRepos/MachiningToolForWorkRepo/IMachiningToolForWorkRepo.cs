using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IMachiningToolForWorkRepo
    { 
        public Task AddMachiningToolForWork(MachiningToolForWork machiningToolForWork);
        public Task DeleteMachiningToolForWork(Guid? id);
        public Task<IEnumerable<MachiningToolForWork>> GetAllMachiningToolsForWork();
        public Task<MachiningToolForWork> GetMachiningToolForWork(Guid? id);
        public Task<bool> MachiningToolForWorkExists(Guid id);
        public Task UpdateRow(MachiningToolForWork machiningToolForWork);
        public Task UpdateMachiningTool(MachiningTool machiningTool);
        public Task<IEnumerable<Job>> GetAllJobs();
        public Task<IEnumerable<Work>> GetAllWork();
        public Task<IEnumerable<WorkOrder>> GetAllWorkOrders();
        public Task<IEnumerable<MachiningTool>> GetAllMachiningTools();
        public Task<Work> GetWork(Guid id);
        public Task<MachiningTool> GetMachiningTool(Guid id);
        public Task<User> GetUser(Guid id);
        public Task<IEnumerable<MachiningTool>> GetAvailableTools();
    }
}
