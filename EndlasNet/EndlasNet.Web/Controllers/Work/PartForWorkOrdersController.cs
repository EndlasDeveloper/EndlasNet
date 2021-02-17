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
    public class PartForWorkOrdersController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PartForWorkOrdersController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: PartForWorkOrders
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.PartsForWorkOrders.Include(p => p.PartInfo).Include(p => p.User).Include(p => p.Work);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: PartForWorkOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _context.PartsForWorkOrders
                .Include(p => p.PartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }

            return View(partForWorkOrder);
        }

        // GET: PartForWorkOrders/Create
        public IActionResult Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString");
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber");
            return View();
        }

        // POST: PartForWorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId")] PartForWorkOrder partForWorkOrder)
        {
            if (ModelState.IsValid)
            {
                partForWorkOrder.PartId = Guid.NewGuid();
                _context.Add(partForWorkOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", partForWorkOrder.UserId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForWorkOrder.WorkId);
            return View(partForWorkOrder);
        }

        // GET: PartForWorkOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _context.PartsForWorkOrders.FindAsync(id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", partForWorkOrder.UserId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForWorkOrder.WorkId);
            return View(partForWorkOrder);
        }

        // POST: PartForWorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId")] PartForWorkOrder partForWorkOrder)
        {
            if (id != partForWorkOrder.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partForWorkOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartForWorkOrderExists(partForWorkOrder.PartId))
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
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "AuthString", partForWorkOrder.UserId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForWorkOrder.WorkId);
            return View(partForWorkOrder);
        }

        // GET: PartForWorkOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _context.PartsForWorkOrders
                .Include(p => p.PartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }

            return View(partForWorkOrder);
        }

        // POST: PartForWorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForWorkOrder = await _context.PartsForWorkOrders.FindAsync(id);
            _context.PartsForWorkOrders.Remove(partForWorkOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartForWorkOrderExists(Guid id)
        {
            return _context.PartsForWorkOrders.Any(e => e.PartId == id);
        }
    }
}
