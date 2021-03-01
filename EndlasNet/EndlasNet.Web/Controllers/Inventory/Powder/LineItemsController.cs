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
    public class LineItemsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public LineItemsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: LineItems

        public async Task<IActionResult> Index(Guid id)
        {
            var endlasNetDbContext = _context.LineItems
                .Include(l => l.PowderOrder)
                .Where(l => l.PowderOrderId == id);
            if (await endlasNetDbContext.CountAsync() != 0)
                return View(await endlasNetDbContext.ToListAsync());
            else
                return View();
        }

        // GET: LineItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItems
                .Include(l => l.PowderOrder)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        [HttpGet]
        public IActionResult Create(Guid powderOrderId)
        {
            ViewBag.PowderOrderId = powderOrderId;
            return View();
        }



        // POST: LineItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineItemId,PowderName,VendorDescription,ParticleSize,PowderOrderId,NumBottles")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                lineItem.LineItemId = Guid.NewGuid();
                _context.Add(lineItem);
                await _context.SaveChangesAsync();

                for (int i = 0; i < lineItem.NumBottles; i++)
                {
                    var newPowder = new Powder
                    {
                        PowderId = Guid.NewGuid(),
                        BottleNumber = "",
                        BottleCost = 0.0f,
                        LotNumber = "",
                        InitWeight = 0,
                        Weight = 0,
                        UserId = new Guid(HttpContext.Session.GetString("userId")),
                        LineItem = lineItem,
                        LineItemId = lineItem.LineItemId
                    };
                    _context.Add(newPowder);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "PowderOrders");
            }
            return View(lineItem);
        }

        // GET: LineItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItems.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }
            return View(lineItem);
        }

        // POST: LineItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LineItemId,PowderName,VendorDescription,ParticleSize,NumBottles,PowderOrderId")] LineItem lineItem)
        {
            if (id != lineItem.LineItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineItemExists(lineItem.LineItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "LineItems", new { id= lineItem.PowderOrderId });
            }
            return View(lineItem);
        }

        // GET: LineItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItems
                .Include(l => l.PowderOrder)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lineItem = await _context.LineItems.FindAsync(id);
            _context.LineItems.Remove(lineItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ViewList(Guid lineItemId, Guid powderOrderId)
        {
            return RedirectToAction("Index", "Powders", new {powderOrderId = powderOrderId, lineItemId = lineItemId});
        }
        private bool LineItemExists(Guid id)
        {
            return _context.LineItems.Any(e => e.LineItemId == id);
        }
    }
}
