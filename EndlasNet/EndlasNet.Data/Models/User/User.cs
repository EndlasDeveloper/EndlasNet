using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EndlasNet.Data
{

    public class User
    {
        public Guid UserId { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Endlas email")]
        public string EndlasEmail { get; set; }

        [StringLength(250, MinimumLength = 8)]
        [Display(Name = "Password")]
        public string AuthString { get; set; }

        public IEnumerable<MachiningToolForJob> MachiningToolForJobs{ get; set; }
        public IEnumerable<MachiningToolForWorkOrder> MachiningToolForWorkOrders { get; set; }
        public IEnumerable<StaticPartInfo> StaticPartInfo { get; set; }
        public IEnumerable<MachiningTool> MachiningTools { get; set; }
        public IEnumerable<Powder> Powders { get; set; }
        public IEnumerable<Work> Work { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
    }
}
