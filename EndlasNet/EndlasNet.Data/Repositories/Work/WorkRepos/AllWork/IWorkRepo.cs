using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IWorkRepo
    {
        public Task<IEnumerable<Work>> GetAllWork();
        public Task<Work> GetWork(Guid? id);
        public Task DeleteWork(Guid? id);
        public string GetWorkType(Work work);

    }
}
