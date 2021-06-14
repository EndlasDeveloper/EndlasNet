using EndlasNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Models
{
    public class PartsForWorkMinimizedViewModel
    {
        public Guid WorkId { get; set; }
        public Guid PartForWorkId { get;set; }
        public StaticPartInfo StaticPartInfo { get; set; }
        public string JobNumber { get; set; }
        public string DrawingNumber{ get; set; }
        public int PartCount { get; set; }

    }
}
