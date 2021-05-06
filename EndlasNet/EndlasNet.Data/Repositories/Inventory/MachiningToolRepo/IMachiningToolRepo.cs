using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IMachiningToolRepo
    {
        public Task AddRow(MachiningTool machiningTool);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<MachiningTool>> GetAllRows();
        public Task<MachiningTool> GetRow(Guid? id);
        public Task<MachiningTool> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(MachiningTool machiningTool);
        public Task<MachiningTool> FindRow(Guid? id);
        public Task<List<MachiningTool>> GetAvailableTools();
        public Task<IEnumerable<Vendor>> GetAllVendors();
    }
}
