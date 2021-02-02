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
    public class EnvironmentalSnapshotsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public EnvironmentalSnapshotsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: EnvironmentalSnapshots
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnvironmentalSnapshots.ToListAsync());
        }

        // GET: EnvironmentalSnapshots/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalSnapshot = await _context.EnvironmentalSnapshots
                .FirstOrDefaultAsync(m => m.EnvSnapshotId == id);
            if (environmentalSnapshot == null)
            {
                return NotFound();
            }

            return View(environmentalSnapshot);
        }

        // GET: EnvironmentalSnapshots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnvironmentalSnapshots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnvSnapshotId,DateTimeCollected,Temperature,Humidity")] EnvironmentalSnapshot environmentalSnapshot)
        {
            if (ModelState.IsValid)
            {
                environmentalSnapshot.EnvSnapshotId = Guid.NewGuid();
                _context.Add(environmentalSnapshot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(environmentalSnapshot);
        }

        // GET: EnvironmentalSnapshots/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalSnapshot = await _context.EnvironmentalSnapshots.FindAsync(id);
            if (environmentalSnapshot == null)
            {
                return NotFound();
            }
            return View(environmentalSnapshot);
        }

        // POST: EnvironmentalSnapshots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EnvSnapshotId,DateTimeCollected,Temperature,Humidity")] EnvironmentalSnapshot environmentalSnapshot)
        {
            if (id != environmentalSnapshot.EnvSnapshotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environmentalSnapshot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentalSnapshotExists(environmentalSnapshot.EnvSnapshotId))
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
            return View(environmentalSnapshot);
        }

        // GET: EnvironmentalSnapshots/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalSnapshot = await _context.EnvironmentalSnapshots
                .FirstOrDefaultAsync(m => m.EnvSnapshotId == id);
            if (environmentalSnapshot == null)
            {
                return NotFound();
            }

            return View(environmentalSnapshot);
        }

        // POST: EnvironmentalSnapshots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var environmentalSnapshot = await _context.EnvironmentalSnapshots.FindAsync(id);
            _context.EnvironmentalSnapshots.Remove(environmentalSnapshot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentalSnapshotExists(Guid id)
        {
            return _context.EnvironmentalSnapshots.Any(e => e.EnvSnapshotId == id);
        }
    }
}
