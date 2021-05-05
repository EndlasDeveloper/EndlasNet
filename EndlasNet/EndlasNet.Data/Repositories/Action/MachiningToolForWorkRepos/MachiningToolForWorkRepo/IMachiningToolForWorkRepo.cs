using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IMachiningToolForWorkRepo
    {
        public MachiningToolRepo MachiningToolRepo { get; set; }
        public UserRepo UserRepo { get; set; }
        public JobRepo JobRepo { get; set; }
        public WorkOrderRepo WorkOrderRepo { get; set; }

        public MachiningToolForJobRepo MachiningToolForJobRepo { get; set; }
        public MachiningToolForWorkOrderRepo MachiningToolForWorkOrderRepo { get; set; }
        public Task AddRow(MachiningToolForWork machiningToolForWork);

        public Task DeleteRow(Guid? id);

        public Task<IEnumerable<MachiningToolForWork>> GetAllRows();

        public Task<MachiningToolForWork> GetRow(Guid? id);


        public Task<bool> RowExists(Guid id);
        Task UpdateRow(MachiningToolForWork machiningToolForWork);
    }
}
