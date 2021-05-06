using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IMachiningToolForWorkRepo
    { 
        public Task AddRow(MachiningToolForWork machiningToolForWork);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<MachiningToolForWork>> GetAllRows();
        public Task<MachiningToolForWork> GetRow(Guid? id);
        public Task<MachiningToolForJob> GetJob(Guid id);
        public Task<MachiningToolForWorkOrder> GetWorkOrder(Guid id);
        public Task<bool> RowExists(Guid id);
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
