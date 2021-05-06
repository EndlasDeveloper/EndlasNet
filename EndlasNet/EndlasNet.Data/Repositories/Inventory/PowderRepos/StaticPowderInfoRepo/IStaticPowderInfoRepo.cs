using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IStaticPowderInfoRepo
    {
        public Task AddRow(StaticPowderInfo staticPowder);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<StaticPowderInfo>> GetAllRows();
        public Task<StaticPowderInfo> GetRow(Guid? id);
        public Task<StaticPowderInfo> GetStaticPowderInfo(Guid? staticPowderInfoId);
        public Task RemoveRow(Guid id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(StaticPowderInfo staticPowder);
    }
}
