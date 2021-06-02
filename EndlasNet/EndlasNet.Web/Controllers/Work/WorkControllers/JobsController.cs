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
    public class JobsController : Controller
    {
        private readonly IJobRepo _repo;
        public JobsController(IJobRepo repo)
        {
            
            _repo = repo;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllRows());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = (Job)await _repo.GetRow(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public async Task<IActionResult> Create()
        {
            await SetViewData();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Job job)
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
                job.UserId = new Guid(HttpContext.Session.GetString("userId"));
                await _repo.AddRow(job);
                return RedirectToAction(nameof(Index));
            }
            await SetViewData();
            return View(job);
        }

        private async Task SetViewData(Job job)
        {
            ViewData["CustomerId"] = new SelectList(await _repo.GetAllCustomers(), "CustomerId", "CustomerName", job.CustomerId);
            ViewData["QuoteId"] = new SelectList(await _repo.GetAllQuotesWithoutJob(), "QuoteId", "EndlasNumber");
        }

        private async Task SetViewData()
        {
            var quotes = await _repo.GetAllQuotesWithoutJob();
            List<QuoteDropDownViewModel> vmList = new List<QuoteDropDownViewModel>();
            foreach(Quote quote in quotes)
            {
                vmList.Insert(0, new QuoteDropDownViewModel { QuoteId = quote.QuoteId, DropDownQuoteDisplayStr = quote.EndlasNumber + "-" + quote.ShortDescription });
            }
            ViewData["CustomerId"] = new SelectList(await _repo.GetAllCustomers(), "CustomerId", "CustomerName");
            ViewData["QuoteId"] = new SelectList(vmList, "QuoteId", "DropDownQuoteDisplayStr");
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _repo.FindRow(id);
            if (job == null)
            {
                return NotFound();
            }
            await SetViewData(job);
            var currJob = await _repo.GetRow(id);

            var quotes = await _repo.GetAllQuotesWithoutJob();
            var quotesList = quotes.ToList();
            quotesList.Insert(0, currJob.Quote);
            
            ViewData["QuoteId"] = new SelectList(quotesList, "QuoteId", "EndlasNumber");

            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Job job)
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
                    job.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    var quote = await _repo.GetQuote((Guid)job.QuoteId);
                    job.EndlasNumber = quote.EndlasNumber;
                    await _repo.UpdateRow(job);
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
                return RedirectToAction(nameof(Index));
            }
            await SetViewData(job);

            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = (Job)await _repo.GetRow(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> JobExists(Guid id)
        {
            return await _repo.RowExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadProcessPdf(Guid? myvar)
        {
            if (myvar == null)
            {
                return NotFound();
            }

            var job = await _repo.GetRow(myvar);

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
    }
}
