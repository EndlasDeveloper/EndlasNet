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
    public class PowdersController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private PowderRepo repo;
        public PowdersController(EndlasNetDbContext context)
        {
            _context = context;
            repo = new PowderRepo(context);
        }

        // GET: Powders
        public async Task<IActionResult> Index(Guid lineItemId, string powderName)
        {
            ViewBag.PowderName = powderName;
            return View(await repo.GetLineItemPowders(lineItemId));
        }

        // GET: Powders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.Powders
                .Include(p => p.LineItem)
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PowderId == id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // GET: Powders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Powders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderId,BottleNumber,InitWeight,Weight,BottleCost,LotNumber,LineItemId,UserId")] Powder powder, Guid id)
        {
            if (ModelState.IsValid)
            {
                powder.PowderId = Guid.NewGuid();
                _context.Entry(powder).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(powder).Property("UpdatedDate").CurrentValue = DateTime.Now;
                powder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                powder.Weight = powder.InitWeight;
                _context.Add(powder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(powder);
        }

        // GET: Powders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.Powders.FindAsync(id);
            if (powder == null)
            {
                return NotFound();
            }
 
            return View(powder);
        }

        // POST: Powders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderId,BottleNumber,InitWeight,Weight,BottleCost,LotNumber,LineItemId,UserId")] Powder powder)
        {
            if (id != powder.PowderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(powder).Property("CreatedDate").CurrentValue = DateTime.Now;
                    _context.Entry(powder).Property("UpdatedDate").CurrentValue = DateTime.Now;
                    powder.UserId = new Guid(HttpContext.Session.GetString("userId")); _context.Update(powder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowderExists(powder.PowderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Powders", new { lineItemId = powder.LineItemId });
            }
            return View(powder);
        }

        // GET: Powders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.Powders
                .Include(p => p.LineItem)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PowderId == id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // POST: Powders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powder = await _context.Powders.FindAsync(id);
            _context.Powders.Remove(powder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { lineItemId = powder.LineItemId });
        }

        private bool PowderExists(Guid id)
        {
            return _context.Powders.Any(e => e.PowderId == id);
        }
    }
}
