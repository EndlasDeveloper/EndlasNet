using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public enum Works { Job, WorkOrder }

    public class WorkRepoFactory
    {
        private Works _workType;
        private readonly IPartForJobRepo _repo;
        public WorkRepoFactory(IPartForJobRepo repo, Works workType)
        {
            _workType = workType;
            _repo = repo;
        }

        public async Task<IEnumerable<Work>> GetWorkWithParts()
        {
            if (_workType == Works.Job)
            {
                return await _repo.GetJobsWithParts();
            }
            else if (_workType == Works.WorkOrder)
            {
                return await _repo.GetWorkOrdersWithParts();
            }
            else
                return null;
        }


    }
}
