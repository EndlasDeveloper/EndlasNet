using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;

namespace EndlasNet.Web.Models
{
    public class WorkItemViewModel
    {
        public Guid WorkItemId { get; set; }
        public Guid StaticPartInfoId { get; set; }
        public int NumPartsForWork { get; set; }
        public Guid WorkId { get; set; }
    }
}
