using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class WorkItem
    {
        [Key]
        public Guid WorkItemId { get; set; }

        [ForeignKey("WorkId")]
        public Guid WorkId { get; set; }
        public Work Work { get; set; }


        public IEnumerable<PartForWork> PartsForWork { get; set; }

    }
}
