using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPartForWorkRepo
    {
        public Task<IEnumerable<PartForWork>> GetBatch(string workId, string partInfoId);
    }
}
