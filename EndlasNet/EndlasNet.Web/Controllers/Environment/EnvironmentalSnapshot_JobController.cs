using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;

namespace EndlasNet.Web.Controllers
{
    public class EnvironmentalSnapshot_JobController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public EnvironmentalSnapshot_JobController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: EnvironmentalSnapshot_Job
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.EnvironmentalSnapshot_Jobs.Include(e => e.EnvironmentalSnapshot).Include(e => e.Job);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: EnvironmentalSnapshot_Job/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalSnapshot_Job = await _context.EnvironmentalSnapshot_Jobs
                .Include(e => e.EnvironmentalSnapshot)
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.EnvSnapshotId == id);
            if (environmentalSnapshot_Job == null)
            {
                return NotFound();
            }

            return View(environmentalSnapshot_Job);
        }

        // GET: EnvironmentalSnapshot_Job/Create
        public IActionResult Create()
        {
            ViewData["EnvSnapshotId"] = new SelectList(_context.EnvironmentalSnapshots, "EnvSnapshotId", "DateTimeCollected");
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobDescription");
            return View();
        }

        // POST: EnvironmentalSnapshot_Job/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnvSnapshotId,JobId")] EnvironmentalSnapshot_Job environmentalSnapshot_Job)
        {
            if (ModelState.IsValid)
            {
                environmentalSnapshot_Job.EnvSnapshotId = Guid.NewGuid();
                _context.Add(environmentalSnapshot_Job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnvSnapshotId"] = new SelectList(_context.EnvironmentalSnapshots, "EnvSnapshotId", "DateTimeCollected", environmentalSnapshot_Job.EnvSnapshotId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobDescription", environmentalSnapshot_Job.JobId);
            return View(environmentalSnapshot_Job);
        }

        // GET: EnvironmentalSnapshot_Job/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalSnapshot_Job = await _context.EnvironmentalSnapshot_Jobs.FindAsync(id);
            if (environmentalSnapshot_Job == null)
            {
                return NotFound();
            }
            ViewData["EnvSnapshotId"] = new SelectList(_context.EnvironmentalSnapshots, "EnvSnapshotId", "DateTimeCollected", environmentalSnapshot_Job.EnvSnapshotId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobDescription", environmentalSnapshot_Job.JobId);
            return View(environmentalSnapshot_Job);
        }

        // POST: EnvironmentalSnapshot_Job/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EnvSnapshotId,JobId")] EnvironmentalSnapshot_Job environmentalSnapshot_Job)
        {
            if (id != environmentalSnapshot_Job.EnvSnapshotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environmentalSnapshot_Job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentalSnapshot_JobExists(environmentalSnapshot_Job.EnvSnapshotId))
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
            ViewData["EnvSnapshotId"] = new SelectList(_context.EnvironmentalSnapshots, "EnvSnapshotId", "DateTimeCollected", environmentalSnapshot_Job.EnvSnapshotId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobDescription", environmentalSnapshot_Job.JobId);
            return View(environmentalSnapshot_Job);
        }

        // GET: EnvironmentalSnapshot_Job/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalSnapshot_Job = await _context.EnvironmentalSnapshot_Jobs
                .Include(e => e.EnvironmentalSnapshot)
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.EnvSnapshotId == id);
            if (environmentalSnapshot_Job == null)
            {
                return NotFound();
            }

            return View(environmentalSnapshot_Job);
        }

        // POST: EnvironmentalSnapshot_Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var environmentalSnapshot_Job = await _context.EnvironmentalSnapshot_Jobs.FindAsync(id);
            _context.EnvironmentalSnapshot_Jobs.Remove(environmentalSnapshot_Job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentalSnapshot_JobExists(Guid id)
        {
            return _context.EnvironmentalSnapshot_Jobs.Any(e => e.EnvSnapshotId == id);
        }
    }
}
