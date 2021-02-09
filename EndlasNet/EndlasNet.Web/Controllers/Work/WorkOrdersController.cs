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
    public class WorkOrdersController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public WorkOrdersController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.WorkOrders.Include(w => w.User);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .Include(w => w.User).AsNoTracking()
                .FirstOrDefaultAsync(m => m.WorkId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                workOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                _context.Entry(workOrder).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(workOrder).Property("UpdatedDate").CurrentValue = DateTime.Now;

                workOrder.WorkId = Guid.NewGuid();
                _context.Add(workOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    workOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Entry(workOrder).Property("UpdatedDate").CurrentValue = DateTime.Now;

                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.WorkId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", workOrder.UserId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WorkId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            _context.WorkOrders.Remove(workOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(Guid id)
        {
            return _context.WorkOrders.Any(e => e.WorkId == id);
        }
    }
}
