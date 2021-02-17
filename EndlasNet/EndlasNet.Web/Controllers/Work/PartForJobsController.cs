using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
// TODO: set user when creating a part for job
namespace EndlasNet.Web.Controllers
{
    public class PartForJobsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PartForJobsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: PartForJobs
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.PartsForWork.Include(p => p.Work).Include(p => p.Part);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: PartForJobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForWork
                .Include(p => p.Work)
                .Include(p => p.Part).AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForJob == null)
            {
                return NotFound();
            }

            return View(partForJob);
        }

        // GET: PartForJobs/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Jobs, "WorkId", "EndlasNumber");
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "DrawingNumber");
            return View();
        }

        // POST: PartForJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,PartId,JobId")] PartForJob partForJob)
        {
            if (ModelState.IsValid)
            {
                partForJob.PartForWorkId = Guid.NewGuid();
                _context.Add(partForJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "WorkId", "EndlasNumber", partForJob.JobId);
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "DrawingNumber", partForJob.PartId);
            return View(partForJob);
        }

        // GET: PartForJobs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForWork.FindAsync(id);
            if (partForJob == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "WorkId", "EndlasNumber", partForJob.WorkId);
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "DrawingNumber", partForJob.PartId);
            return View(partForJob);
        }

        // POST: PartForJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,PartId,JobId")] PartForJob partForJob)
        {
            if (id != partForJob.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partForJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartForJobExists(partForJob.PartForWorkId))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "WorkId", "EndlasNumber", partForJob.JobId);
            ViewData["PartId"] = new SelectList(_context.Parts, "PartId", "DrawingNumber", partForJob.PartId);
            return View(partForJob);
        }

        // GET: PartForJobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForWork
                .Include(p => p.Work)
                .Include(p => p.Part)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForJob == null)
            {
                return NotFound();
            }

            return View(partForJob);
        }

        // POST: PartForJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForJob = await _context.PartsForWork.FindAsync(id);
            _context.PartsForWork.Remove(partForJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartForJobExists(Guid id)
        {
            return _context.PartsForWork.Any(e => e.PartForWorkId == id);
        }
    }
}
