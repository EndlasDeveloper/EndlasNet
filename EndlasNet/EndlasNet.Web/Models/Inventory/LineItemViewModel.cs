using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class LineItemViewModel
    {
        public Guid LineItemId { get; set; }
        public LineItem LineItem { get; set; }
        public bool ClearCertPdf { get; set; }
    }
}
