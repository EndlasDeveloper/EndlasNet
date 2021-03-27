using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IRepository
    {
        public Task<Object> GetRow(Guid? id);
        public Task<IEnumerable<Object>> GetAllRows();
        public Task AddRow(Object obj);
        public Task UpdateRow(Object obj);
        public Task RemoveRow(Guid id);
        public Task DeleteRow(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task<Object> GetRowNoTracking(Guid? id);
    }
}
