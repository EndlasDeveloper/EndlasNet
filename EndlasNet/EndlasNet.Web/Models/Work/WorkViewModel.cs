using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class WorkViewModel
    {
        public WorkViewModel(Work work, WorkType workType)
        {
            WorkId = work.WorkId;
            Work = work;
            WorkType = workType;
        }
        public WorkViewModel()
        {

        }
        public WorkType WorkType;
        public Guid WorkId;
        public Work Work;
    }
}
