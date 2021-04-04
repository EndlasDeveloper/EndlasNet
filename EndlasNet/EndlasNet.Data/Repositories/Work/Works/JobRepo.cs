using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class JobRepo : IWorkRepo
    {
        public Task AddRow(object obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRow(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetAllRows()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetRow(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetRowNoTracking(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RowExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRow(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
