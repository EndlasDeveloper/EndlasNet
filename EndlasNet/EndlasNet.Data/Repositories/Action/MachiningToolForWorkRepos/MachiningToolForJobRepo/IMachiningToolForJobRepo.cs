using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IMachiningToolForJobRepo
    {
        public Task AddRow(MachiningToolForWork machiningToolForWork);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<MachiningToolForJob>> GetAllRows();
        public Task<MachiningToolForJob> GetRow(Guid? id);
        public Task<MachiningToolForJob> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(MachiningToolForJob machiningToolForJob);
    }
}
