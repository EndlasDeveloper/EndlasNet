using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IMachiningToolForWorkOrderRepo
    {
        public Task AddRow(MachiningToolForWork machiningToolForWork);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<MachiningToolForWork>> GetAllRows();
        public Task<MachiningToolForWork> GetRow(Guid? id);
        public Task<MachiningToolForWork> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(MachiningToolForWork machiningToolForWork);
    }
}
