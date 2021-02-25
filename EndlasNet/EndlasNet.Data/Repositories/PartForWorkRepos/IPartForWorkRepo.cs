using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPartForWorkRepo
    {
        public Task<IEnumerable<PartForJob>> GetBatch(string workId, string partInfoId);
    }
}
