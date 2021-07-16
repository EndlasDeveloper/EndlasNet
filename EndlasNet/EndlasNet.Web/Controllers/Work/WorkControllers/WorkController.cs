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
            Work work = null;
            ViewBag.WorkType = workType;

            switch (workType)
            {
                case WorkType.Job:
                    work = await _repo.GetJob(id);
                    break;
                case WorkType.WorkOrder:
                    work = await _repo.GetWorkOrder(id);
                    break;
                case WorkType.Work:
                    work = await _repo.GetWork(id);
                    break;
                default:
                    break;
            }
         
            return View(new WorkViewModel(work, workType));
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
        public async Task<IActionResult> Create(WorkType workType, [Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,NumWorkItems,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Work work)
        {
            ViewBag.WorkType = workType;
            var vm = new WorkViewModel();
            var workList = await _repo.GetWorkWithEndlasNumber(work.EndlasNumber);
            var quotes = await _repo.GetQuotesWithEndlasNumber(work.EndlasNumber);
            if (workList.Any() || quotes.Count() > 1)
            {
                ViewBag.EndlasNumberConflict = true;
                ViewData["CustomerId"] = new SelectList(await _repo.GetAllCustomers(), "CustomerId", "CustomerName");
                return View(work);
            }
            if (ModelState.IsValid)
            {
                if(work.QuoteId == null && workType == WorkType.Job)
                {
                    ViewBag.NoQuoteWarning = true;
                    return View(work);
                }
                work.WorkId = Guid.NewGuid();

                if (work.ProcessSheetNotesFile != null)
                {
                    work.ProcessSheetNotesPdfBytes = await FileURL.GetFileBytes(work.ProcessSheetNotesFile);
                }
              
                if(workType == WorkType.Job)
                {
                    var quote = await _repo.GetQuote((Guid)work.QuoteId);
                    work.EndlasNumber = quote.EndlasNumber;
                    await _repo.AddJob(Job.CastWorkToJob(work));
                }
                else if(workType == WorkType.WorkOrder)
                {
                    await _repo.AddWorkOrder(WorkOrder.CastWorkToWorkOrder(work));
                }

                for (int i = 0; i < work.NumWorkItems; i++)
                {
                    WorkItem workItem = new WorkItem { WorkItemId = Guid.NewGuid(), WorkId = work.WorkId };
                    await _repo.AddWorkItem(workItem);
                }
                return RedirectToAction(nameof(Index), new { workType =  workType});
            }
            await SetViewData();
            vm = new WorkViewModel(work, workType);
            return View(vm);
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

            switch (workType)
            {
                case WorkType.Job:
                    Job job = await _repo.GetJob(id);
                    await SetViewData(job);
                    var currJob = await _repo.GetJob(id);
                    var quotes = await _repo.GetAllQuotesWithoutJob();
                    var quotesList = quotes.ToList();
                    quotesList.Insert(0, currJob.Quote);

                    ViewData["QuoteId"] = new SelectList(GetQuoteViewModelDropDownList(quotesList), "QuoteId", "DropDownQuoteDisplayStr");
                    return View(job);
                    
                case WorkType.WorkOrder:
                    WorkOrder workOrder = await _repo.GetWorkOrder(id);
                    return View(workOrder);
                case WorkType.Work:
                    Work work = await _repo.GetWork(id);
                    return View(work);
                default:
                    Work defaultWork = await _repo.GetWork(id);
                    return View(defaultWork);
                    
            }
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkType workType, [Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Work work)
        {
            ViewBag.WorkType = workType;

            if (id != work.WorkId)
            {
                return NotFound();
            }
            ViewBag.WorkType = workType;

            var workList = await _repo.GetWorkWithEndlasNumber(work.EndlasNumber);
            var quotes = await _repo.GetQuotesWithEndlasNumber(work.EndlasNumber);

            if(workList.Any() || (workType == WorkType.Job && quotes.Count() > 1))
            {
                ViewBag.EndlasNumberConflict = "true";
               
                return View(work);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (work.ProcessSheetNotesFile != null)
                    {
                        work.ProcessSheetNotesPdfBytes = await FileURL.GetFileBytes(work.ProcessSheetNotesFile);
                    }
                    if (work.ClearPdf)
                    {
                        work.ProcessSheetNotesPdfBytes = null;
                    }

                    switch (workType)
                    {
                        case WorkType.Job:
                            var quote = await _repo.GetQuote((Guid)work.QuoteId);
                            work.EndlasNumber = quote.EndlasNumber;
                            await _repo.UpdateJob(Job.CastWorkToJob(work));
                            break;
                        case WorkType.WorkOrder:
                            await _repo.UpdateWorkOrder(WorkOrder.CastWorkToWorkOrder(work));
                            break;
                        case WorkType.Work:
                            await _repo.UpdateWork(work);
                            break;
                        default:
                            break;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await JobExists(work.WorkId)))
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
            await SetViewData(Job.CastWorkToJob(work));
            return View(work);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id, WorkType workType)
        {
            if (id == null)
            {
                return NotFound();
            }
            Work work = null;
            ViewBag.WorkType = workType;

            switch (workType)
            {
                case WorkType.Job:
                    work = await _repo.GetJob(id);
                    break;
                case WorkType.WorkOrder:
                    work = await _repo.GetWorkOrder(id);
                    break;
                case WorkType.Work:
                    work = await _repo.GetWork(id);
                    break;
                default:
                    break;
            }

            return View(new WorkViewModel(work, workType));
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, WorkType workType)
        {
            switch (workType)
            {
                case WorkType.Job:
                    await _repo.DeleteJob(id);
                    break;
                case WorkType.WorkOrder:
                    await _repo.DeleteWorkOrder(id);
                    break;
                case WorkType.Work:
                    await _repo.DeleteWork(id);
                    break;
                default:
                    break;
            }
            return RedirectToAction(nameof(Index), new { workType = workType });
        }

        private async Task<bool> JobExists(Guid id)
        {
            return await _repo.JobExists(id);
        }

        private async Task<bool> WorkExist(Guid id)
        {
            return await _repo.WorkOrderExists(id);
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
