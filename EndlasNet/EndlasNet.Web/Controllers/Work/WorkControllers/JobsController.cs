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

namespace EndlasNet.Web.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobRepo _jobRepo;
        public JobsController(IJobRepo repo)
        {
            
            _jobRepo = repo;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return View(await _jobRepo.GetAllRows());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = (Job)await _jobRepo.GetRow(id);
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
        public async Task<IActionResult> Create([Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Job job)
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
                var quote = await _jobRepo.GetQuote((Guid)job.QuoteId);
                job.EndlasNumber = quote.EndlasNumber;
                job.UserId = new Guid(HttpContext.Session.GetString("userId"));
                await _jobRepo.AddRow(job);
                return RedirectToAction(nameof(Index));
            }
            await SetViewData();
            return View(job);
        }

        private async Task SetViewData(Job job)
        {
            ViewData["CustomerId"] = new SelectList(await _jobRepo.GetAllCustomers(), "CustomerId", "CustomerName", job.CustomerId);
            ViewData["QuoteId"] = new SelectList(await _jobRepo.GetAllQuotesWithoutJob(), "QuoteId", "EndlasNumber");
        }

        private async Task SetViewData()
        {
            ViewData["CustomerId"] = new SelectList(await _jobRepo.GetAllCustomers(), "CustomerId", "CustomerName");
            ViewData["QuoteId"] = new SelectList(await _jobRepo.GetAllQuotesWithoutJob(), "QuoteId", "EndlasNumber");
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _jobRepo.FindRow(id);
            if (job == null)
            {
                return NotFound();
            }
            await SetViewData(job);
            var currJob = await _jobRepo.GetRow(id);

            var quotes = await _jobRepo.GetAllQuotesWithoutJob();
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
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Job job)
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
                    var quote = await _jobRepo.GetQuote((Guid)job.QuoteId);
                    job.EndlasNumber = quote.EndlasNumber;
                    await _jobRepo.UpdateRow(job);
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

            var job = (Job)await _jobRepo.GetRow(id);
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
            await _jobRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> JobExists(Guid id)
        {
            return await _jobRepo.RowExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadProcessPdf(Guid? myvar)
        {
            if (myvar == null)
            {
                return NotFound();
            }

            var job = await _jobRepo.GetRow(myvar);

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
