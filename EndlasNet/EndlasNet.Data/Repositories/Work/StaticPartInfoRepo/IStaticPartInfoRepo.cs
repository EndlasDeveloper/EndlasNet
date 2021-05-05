using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IStaticPartInfoRepo 
    {
        public Task AddRow(StaticPartInfo staticPartInfo);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<StaticPartInfo>> GetAllRows();
        public Task<StaticPartInfo> GetRow(Guid? id);
        public Task<StaticPartInfo> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(StaticPartInfo staticPartInfo);
        public Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
