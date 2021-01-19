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
    public class InsertsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public InsertsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: Inserts
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.Inserts.Include(i => i.Vendor);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: Inserts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insert = await _context.Inserts
                .Include(i => i.Vendor)
                .FirstOrDefaultAsync(m => m.InsertId == id);
            if (insert == null)
            {
                return NotFound();
            }

            return View(insert);
        }

        // GET: Inserts/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Inserts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsertId,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,Description,VendorPartNum,ToolTipRadius,PurchaseDescription,VendorId")] Insert insert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", insert.VendorId);
            return View(insert);
        }

        // GET: Inserts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insert = await _context.Inserts.FindAsync(id);
            if (insert == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", insert.VendorId);
            return View(insert);
        }

        // POST: Inserts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsertId,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,Description,VendorPartNum,ToolTipRadius,PurchaseDescription,VendorId")] Insert insert)
        {
            if (id != insert.InsertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsertExists(insert.InsertId))
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", insert.VendorId);
            return View(insert);
        }

        // GET: Inserts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insert = await _context.Inserts
                .Include(i => i.Vendor)
                .FirstOrDefaultAsync(m => m.InsertId == id);
            if (insert == null)
            {
                return NotFound();
            }

            return View(insert);
        }

        // POST: Inserts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insert = await _context.Inserts.FindAsync(id);
            _context.Inserts.Remove(insert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsertExists(int id)
        {
            return _context.Inserts.Any(e => e.InsertId == id);
        }
    }
}
