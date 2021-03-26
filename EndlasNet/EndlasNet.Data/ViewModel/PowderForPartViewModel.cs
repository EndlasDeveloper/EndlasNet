using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class CheckBoxInfo 
    {
        [Key]
        public Guid CheckBoxInfoId { get; set; }

        [ForeignKey("PowderForPartViewModelId")]
        public Guid PowderForPartViewModelId { get; set; }
        public virtual PowderForPartViewModel PowderForPartViewModel { get; set; }

        public bool IsChecked { get; set; } = false;
        
        [ForeignKey("PartForWorkId")]
        public Guid? PartForWorkId { get; set; }
        
        public virtual PartForWork PartForWork { get; set; }
    }

    public class PowderForPartViewModel
    {
        [Key]
        public Guid PowderForPartViewModelId { get; set; }
        [NotMapped]
        public List<Work> AllWork { get; set; }

        [ForeignKey("WorkId")]
        public Guid? WorkId { get; set; }
        public virtual Work Work { get; set; }
       

        public IEnumerable<CheckBoxInfo> CheckBoxList { get; set; }

        public float? Weight { get; set; }
    }
}
