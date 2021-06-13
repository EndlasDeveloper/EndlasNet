using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;

namespace EndlasNet.Web.Models
{
    public class WorkItemViewModel
    {
        public Guid WorkItemId { get; set; }
        [Display(Name ="Static part info")]
        public Guid StaticPartInfoId { get; set; }
        [Display(Name ="Number of parts")]
        public int NumPartsForWork { get; set; }
        public Guid WorkId { get; set; }
    }
}
