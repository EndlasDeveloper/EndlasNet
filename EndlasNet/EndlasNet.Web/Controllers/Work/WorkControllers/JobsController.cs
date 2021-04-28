using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;

namespace EndlasNet.Web.Controllers
{
    public class JobsController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private readonly JobRepo _jobRepo;
        private readonly CustomerRepo _customerRepo;
        public JobsController(EndlasNetDbContext context)
        {
            _context = context;
            _jobRepo = new JobRepo(context);
            _customerRepo = new CustomerRepo(context);
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
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName");
            ViewData["QuoteId"] = new SelectList(await _context.Quotes.OrderByDescending(q => q.EndlasNumber).ToListAsync(), "QuoteId", "EndlasNumber");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId,CustomerId")] Job job)
        {
            
            if (ModelState.IsValid)
            {
                job.WorkId = Guid.NewGuid();
                var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.QuoteId == job.QuoteId);
                job.EndlasNumber = quote.EndlasNumber;
                job.UserId = new Guid(HttpContext.Session.GetString("userId"));
                await _jobRepo.AddRow(job);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerAddress", job.CustomerId);
            ViewData["QuoteId"] = new SelectList(await _context.Quotes.OrderByDescending(q => q.EndlasNumber).ToListAsync(), "QuoteId", "EndlasNumber");

            return View(job);
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
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", job.CustomerId);
            ViewData["QuoteId"] = new SelectList(await _context.Quotes.OrderByDescending(q => q.EndlasNumber).ToListAsync(), "QuoteId", "EndlasNumber");

            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId,CustomerId")] Job job)
        {
            if (id != job.WorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    job.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    var quote = await _context.Quotes.FirstOrDefaultAsync(q => q.QuoteId == job.QuoteId);
                    job.EndlasNumber = quote.EndlasNumber;
                    _context.Update(job);
                    await _context.SaveChangesAsync();
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
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", job.CustomerId);
            ViewData["QuoteId"] = new SelectList(await _context.Quotes.OrderByDescending(q => q.EndlasNumber).ToListAsync(), "QuoteId", "EndlasNumber");

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
    }
}
