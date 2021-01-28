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
            var endlasNetDbContext = _context.Inserts.Include(i => i.User).Include(i => i.Vendor);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: Inserts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insert = await _context.Inserts
                .Include(i => i.User)
                .Include(i => i.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
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
        public async Task<IActionResult> Create([Bind("ToolTipRadius,MachiningToolId,VendorDescription,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,UserId,VendorId")] Insert insert)
        {
            if (ModelState.IsValid)
            {
                insert.MachiningToolId = Guid.NewGuid();
                _context.Entry(insert).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(insert).Property("UpdatedDate").CurrentValue = DateTime.Now;
                insert.UserId = new Guid(HttpContext.Session.GetString("userId"));

                _context.Add(insert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", insert.VendorId);

            return View(insert);
        }

        // GET: Inserts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("ToolTipRadius,MachiningToolId,VendorDescription,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,UserId,VendorId")] Insert insert)
        {
            if (id != insert.MachiningToolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    insert.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Entry(insert).Property("UpdatedDate").CurrentValue = DateTime.Now;
                    _context.Update(insert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsertExists(insert.MachiningToolId))
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insert = await _context.Inserts
                .Include(i => i.User)
                .Include(i => i.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
            if (insert == null)
            {
                return NotFound();
            }

            return View(insert);
        }

        // POST: Inserts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var insert = await _context.Inserts.FindAsync(id);
            _context.Inserts.Remove(insert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsertExists(Guid id)
        {
            return _context.Inserts.Any(e => e.MachiningToolId == id);
        }
    }
}
