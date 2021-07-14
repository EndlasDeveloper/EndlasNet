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
        public static WorkOrder CastWorkToWorkOrder(Work work)
        {
            return new WorkOrder
            {
                WorkId = work.WorkId,
                Customer = work.Customer,
                CustomerId = work.CustomerId,
                DueDate = work.DueDate,
                WorkDescription = work.WorkDescription,
                ClearPdf = work.ClearPdf,
                EndlasNumber = work.EndlasNumber,
                MachiningToolsForWork = work.MachiningToolsForWork,
                NumWorkItems = work.NumWorkItems,
                ProcessSheetNotesFile = work.ProcessSheetNotesFile,
                ProcessSheetNotesPdfBytes = work.ProcessSheetNotesPdfBytes,
                Status = work.Status,
                WorkItems = work.WorkItems,
                WorkType = work.WorkType
            };
        }
    }
}
