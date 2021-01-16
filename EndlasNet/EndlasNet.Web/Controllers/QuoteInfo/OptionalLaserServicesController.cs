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
    public class OptionalLaserServicesController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public OptionalLaserServicesController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: OptionalLaserServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.OptionalLaserServices.ToListAsync());
        }

        // GET: OptionalLaserServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionalLaserService = await _context.OptionalLaserServices
                .FirstOrDefaultAsync(m => m.OptionalLaserServicesId == id);
            if (optionalLaserService == null)
            {
                return NotFound();
            }

            return View(optionalLaserService);
        }

        // GET: OptionalLaserServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OptionalLaserServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OptionalLaserServicesId,HeatTreatedBlankWt,HeatTreatedPricePerLb,MinHeatTreatmentPrice")] OptionalLaserService optionalLaserService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(optionalLaserService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(optionalLaserService);
        }

        // GET: OptionalLaserServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionalLaserService = await _context.OptionalLaserServices.FindAsync(id);
            if (optionalLaserService == null)
            {
                return NotFound();
            }
            return View(optionalLaserService);
        }

        // POST: OptionalLaserServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OptionalLaserServicesId,HeatTreatedBlankWt,HeatTreatedPricePerLb,MinHeatTreatmentPrice")] OptionalLaserService optionalLaserService)
        {
            if (id != optionalLaserService.OptionalLaserServicesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(optionalLaserService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionalLaserServiceExists(optionalLaserService.OptionalLaserServicesId))
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
            return View(optionalLaserService);
        }

        // GET: OptionalLaserServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var optionalLaserService = await _context.OptionalLaserServices
                .FirstOrDefaultAsync(m => m.OptionalLaserServicesId == id);
            if (optionalLaserService == null)
            {
                return NotFound();
            }

            return View(optionalLaserService);
        }

        // POST: OptionalLaserServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var optionalLaserService = await _context.OptionalLaserServices.FindAsync(id);
            _context.OptionalLaserServices.Remove(optionalLaserService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionalLaserServiceExists(int id)
        {
            return _context.OptionalLaserServices.Any(e => e.OptionalLaserServicesId == id);
        }
    }
}
