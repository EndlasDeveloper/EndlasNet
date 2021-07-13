using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using EndlasNet.Web.Models;
namespace EndlasNet.Web.Controllers
{

    public class WorkController : Controller
    {
        private readonly IWorkRepo _repo;
        public WorkController(IWorkRepo repo)
        {   
            _repo = repo;
        }

        // GET: Jobs
        public async Task<IActionResult> Index(WorkType workType)
        {
            ViewBag.WorkType = workType;
            switch (workType)
            {
                case WorkType.Job:
                    return View(await _repo.GetAllJobs());
                case WorkType.WorkOrder:
                    return View(await _repo.GetAllWorkOrders());
                default:
                    return View(await _repo.GetAllWork());
            }    
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(Guid? id, WorkType workType)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.WorkType = workType;
            var job = await _repo.GetJob(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public async Task<IActionResult> Create(WorkType workType)
        {
            ViewBag.WorkType = workType;
            await SetViewData();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,NumWorkItems,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Job job)
        {
            
            if (ModelState.IsValid)
            {
                if(job.QuoteId == null)
                {
                    ViewBag.NoQuoteWarning = true;
                    return View(job);
                }
                job.WorkId = Guid.NewGuid();

                if (job.ProcessSheetNotesFile != null)
                {
                    job.ProcessSheetNotesPdfBytes = await FileURL.GetFileBytes(job.ProcessSheetNotesFile);
                }
                var quote = await _repo.GetQuote((Guid)job.QuoteId);
                job.EndlasNumber = quote.EndlasNumber;
                await _repo.AddJob(job);
                for(int i = 0; i < job.NumWorkItems; i++)
                {
                    WorkItem workItem = new WorkItem { WorkItemId = Guid.NewGuid(), Work = job, WorkId = job.WorkId };
                    await _repo.AddWorkItem(workItem);
                }
                return RedirectToAction(nameof(Index), new { workType = WorkType.Job });
            }
            await SetViewData();
            return View(job);
        }

        private async Task SetViewData(Job job)
        {
            var quotes = await _repo.GetAllQuotesWithoutJob();
            ViewData["CustomerId"] = new SelectList(await _repo.GetAllCustomers(), "CustomerId", "CustomerName", job.CustomerId);
            ViewData["QuoteId"] = new SelectList(GetQuoteViewModelDropDownList(quotes.ToList()), "QuoteId", "DropDownQuoteDisplayStr");
        }

        private async Task SetViewData()
        {
            var quotes = await _repo.GetAllQuotesWithoutJob();
            ViewData["CustomerId"] = new SelectList(await _repo.GetAllCustomers(), "CustomerId", "CustomerName");
            ViewData["QuoteId"] = new SelectList(GetQuoteViewModelDropDownList(quotes.ToList()), "QuoteId", "DropDownQuoteDisplayStr");
        }

        private List<QuoteDropDownViewModel> GetQuoteViewModelDropDownList(List<Quote> quotes)
        {
            List<QuoteDropDownViewModel> vmList = new List<QuoteDropDownViewModel>();
            foreach (Quote quote in quotes)
            {
                vmList.Insert(0, new QuoteDropDownViewModel(quote));
            }
            return vmList;
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id, WorkType workType)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.WorkType = workType;
            Work work = new Work();
            if(workType == WorkType.Job)
            {
                var job = await _repo.GetJob(id);
                await SetViewData((Job)job);
                var currJob = await _repo.GetJob(id);
                var quotes = await _repo.GetAllQuotesWithoutJob();
                var quotesList = quotes.ToList();
                quotesList.Insert(0, currJob.Quote);
                
                ViewData["QuoteId"] = new SelectList(GetQuoteViewModelDropDownList(quotesList), "QuoteId", "DropDownQuoteDisplayStr");
                return View(job);

            }
            else if (workType == WorkType.WorkOrder)
            {
                var workOrder = await _repo.GetWork(id);
                await SetViewData((Job)workOrder);
                return View(workOrder);
            }

            return View(work);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkType workType, [Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Job job)
        {
            if (id != job.WorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (job.ProcessSheetNotesFile != null)
                    {
                        job.ProcessSheetNotesPdfBytes = await FileURL.GetFileBytes(job.ProcessSheetNotesFile);
                    }
                    if (job.ClearPdf)
                    {
                        job.ProcessSheetNotesPdfBytes = null;
                    }
                    var quote = await _repo.GetQuote((Guid)job.QuoteId);
                    job.EndlasNumber = quote.EndlasNumber;
                    await _repo.UpdateJob(job);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await JobExists(job.WorkId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { workType = workType });
            }
            await SetViewData(job);

            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id, WorkType workType)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.WorkType = workType;

            var job = (Job)await _repo.GetJob(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, WorkType workType)
        {

            await _repo.DeleteJob(id);
            return RedirectToAction(nameof(Index), new { workType = workType });
        }

        private async Task<bool> JobExists(Guid id)
        {
            return await _repo.JobExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadProcessPdf(Guid? myvar)
        {
            if (myvar == null)
            {
                return NotFound();
            }

            var job = await _repo.GetJob(myvar);

            var fileName = job.EndlasNumber + "_process_notes.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(job.ProcessSheetNotesPdfBytes);
            if (ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName);
        }

        public IActionResult ManageWorkItems(Guid? workId)
        {
            return RedirectToAction("Index", "WorkItems", new { workId = workId });
        }
    }
}
