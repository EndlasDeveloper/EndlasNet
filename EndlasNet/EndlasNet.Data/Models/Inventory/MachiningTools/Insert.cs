using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Insert : MachiningTool
    {
       
        [Range(0f, 10.0f)]
        public float ToolTipRadius { get; set; } // measured in inches
    }
}
