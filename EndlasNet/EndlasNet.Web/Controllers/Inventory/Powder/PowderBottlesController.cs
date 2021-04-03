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
    public class PowderBottlesController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private readonly PowderBottleRepo _powderRepo;
        private readonly PowderOrderRepo _powderOrderRepo;
        private readonly LineItemRepo _lineItemRepo;
        private readonly StaticPowderInfoRepo _staticPowderInfoRepo;

        public PowderBottlesController(EndlasNetDbContext context)
        {
            _context = context;
            _staticPowderInfoRepo = new StaticPowderInfoRepo(context);
            _powderRepo = new PowderBottleRepo(context);
            _lineItemRepo = new LineItemRepo(context);
            _powderOrderRepo = new PowderOrderRepo(context);
        }

        // GET: PowderBottles
        public async Task<IActionResult> Index(Guid lineItemId)
        {
            var lineItem = (LineItem)await _lineItemRepo.GetRow(lineItemId);
            var powOrder = (PowderOrder)await _powderOrderRepo.GetRow(lineItem.PowderOrderId);
            var staticPow = (StaticPowderInfo)await _staticPowderInfoRepo.GetRow(lineItem.StaticPowderInfoId);

            ViewBag.LineItemVendorDescription = lineItem.VendorDescription;
            ViewBag.PowderOrderNum = powOrder.PurchaseOrderNum;
            ViewBag.PowderName = staticPow.PowderName; 

            return View(await _powderRepo.GetLineItemPowders(lineItemId));
        }



        public async Task<IActionResult> AllPowderIndex()
        {
            return View(await _context.PowderBottles
                .Include(p => p.StaticPowderInfo).ToListAsync());
        }


        // GET: PowderBottles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _powderRepo.GetRowNoTracking(id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // GET: PowderBottles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowderBottles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderBottleId,BottleNumber,InitWeight,Weight,LotNumber,LineItemId,UserId,StaticPowderInfoId")] PowderBottle powder, Guid id)
        {
            if (ModelState.IsValid)
            {
                powder.PowderBottleId = Guid.NewGuid();
                _context.Entry(powder).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(powder).Property("UpdatedDate").CurrentValue = DateTime.Now;
                powder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                powder.Weight = powder.InitWeight;
                await _powderRepo.AddRow((object)powder);
                return RedirectToAction(nameof(Index));
            }

            return View(powder);
        }

        // GET: PowderBottles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.PowderBottles.FindAsync(id);
            if (powder == null)
            {
                return NotFound();
            }
 
            return View(powder);
        }

        // POST: PowderBottles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderBottleId,BottleNumber,InitWeight,Weight,LotNumber,LineItemId,UserId,StaticPowderInfoId")] PowderBottle powder)
        {
            if (id != powder.PowderBottleId)
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
                    if (!PowderExists(powder.PowderBottleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PowderBottles", new { lineItemId = powder.LineItemId });
            }

            return View(powder);
        }



        // GET: PowderBottles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.PowderBottles
                .Include(p => p.LineItem)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PowderBottleId == id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // POST: PowderBottles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powder = await _context.PowderBottles.FindAsync(id);
            _context.PowderBottles.Remove(powder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { lineItemId = powder.LineItemId });
        }

        private bool PowderExists(Guid id)
        {
            return _context.PowderBottles.Any(e => e.PowderBottleId == id);
        }
        private async Task<string> GetPowderName(Guid? staticInfoId)
        {
            var staticPowInfo = await _context.StaticPowderInfo
               .FirstOrDefaultAsync(s => s.StaticPowderInfoId == staticInfoId);
            return staticPowInfo.PowderName;
        }
    }
}
