using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Part
    {
        public Guid PartId { get; set; }
        public string DrawingNumber { get; set; }
        public string ConditionDescription { get; set; }
        public float InitWeight { get; set; }
        public float Weight{ get; set; }
        public float CladdedWeight { get; set; }
        public string ProcessingNotes { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<PartForJob> PartsForJobs { get; set; }
    }
}
