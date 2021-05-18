using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class WorkOrder : Work
    {
        public WorkOrder()
        {
            PurchaseOrderNum = null;
        }
    }
}
