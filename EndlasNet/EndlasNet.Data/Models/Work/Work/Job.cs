using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Job : Work
    {
        public Job() { }
        public static Job CastWorkToJob(Work work)
        {
            return new Job
            {
                WorkId = work.WorkId,
                Customer = work.Customer,
                CustomerId = work.CustomerId,
                DueDate = work.DueDate,
                PoDate = work.PoDate,
                WorkDescription = work.WorkDescription,
                ClearPdf = work.ClearPdf,
                EndlasNumber = work.EndlasNumber,
                MachiningToolsForWork = work.MachiningToolsForWork,
                NumWorkItems = work.NumWorkItems,
                ProcessSheetNotesFile = work.ProcessSheetNotesFile,
                ProcessSheetNotesPdfBytes = work.ProcessSheetNotesPdfBytes,
                PurchaseOrderNum = work.PurchaseOrderNum,
                Quote = work.Quote,
                QuoteId = work.QuoteId,
                Status = work.Status,
                WorkItems = work.WorkItems,
                WorkType = work.WorkType
            };
        }
    }
}

