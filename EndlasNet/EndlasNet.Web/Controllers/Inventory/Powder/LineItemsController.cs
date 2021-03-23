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
        private readonly LineItemRepo _lineItemRepo;
        private readonly PowderOrderRepo _powderOrderRepo;
        private readonly PowderRepo _powderRepo;
        public LineItemsController(EndlasNetDbContext context)
        {
            _context = context;
            _lineItemRepo = new LineItemRepo(context);
            _powderOrderRepo = new PowderOrderRepo(context);
            _powderRepo = new PowderRepo(context);
        }

        // GET: LineItems

        public async Task<IActionResult> Index(Guid powderOrderId)
        {
            var powderOrder = await _powderOrderRepo.GetPowderOrder(powderOrderId);
            ViewBag.PurchaseOrderNum = powderOrder.PurchaseOrderNum;
            return View(await _lineItemRepo.GetLineItems(powderOrderId));  
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
                .Include(l => l.StaticPowderInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }
           
            return View(lineItem);
        }

        public IActionResult ManagePowders(Guid lineItemId, string powderName)
        {
            return RedirectToAction("Index", "Powders", new { lineItemId = lineItemId, powderName = powderName});
        }


        public IActionResult Create(Guid powderOrderId)
        {
            ViewBag.PowderOrderId = powderOrderId;
            ViewData["StaticPowderInfoId"] = new SelectList(_context.StaticPowderInfo, "StaticPowderInfoId", "PowderName");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Initialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItems
                .Include(l => l.PowderOrder)
                .Include(l => l.StaticPowderInfo)
                .FirstOrDefaultAsync(m => m.LineItemId == id);


            ViewData["StaticPowderInfoId"] = new SelectList(_context.StaticPowderInfo, "StaticPowderInfoId", "PowderName");
            return View(lineItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initialize([Bind("LineItemId,StaticPowderInfoId,VendorDescription,Weight,LineItemCost,ParticleSizeMin,ParticleSizeMax,PowderOrderId,NumBottles")] LineItem lineItem)
        {
            lineItem.IsInitialized = true;
            lineItem.StaticPowderInfo = await _context.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == lineItem.StaticPowderInfoId);
            _context.Update(lineItem);
            await _context.SaveChangesAsync();

            for (int i = 0; i < lineItem.NumBottles; i++)
            {
                var newPowder = new Powder
                {
                    PowderId = Guid.NewGuid(),
                    BottleNumber = "",
                    LotNumber = "",
                    InitWeight = 0,
                    Weight = 0,
                    UserId = new Guid(HttpContext.Session.GetString("userId")),
                    LineItem = lineItem,
                    LineItemId = lineItem.LineItemId,
                    StaticPowderInfo = lineItem.StaticPowderInfo,
                    StaticPowderInfoId = lineItem.StaticPowderInfo.StaticPowderInfoId
                };
                _context.Add(newPowder);
            }
            await _context.SaveChangesAsync();
            lineItem.PowderOrder = await _context.PowderOrders
                .FirstOrDefaultAsync(p => p.PowderOrderId == lineItem.PowderOrderId);
            return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
        }
        
        // POST: LineItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineItemId,StaticPowderInfoId,VendorDescription,Weight,LineItemCost,ParticleSizeMin,ParticleSizeMax,PowderOrderId,NumBottles")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                lineItem.LineItemId = Guid.NewGuid();
                lineItem.StaticPowderInfo = await _context.StaticPowderInfo
                    .FirstOrDefaultAsync(s => s.StaticPowderInfoId == lineItem.StaticPowderInfoId);
                lineItem.StaticPowderInfoId = lineItem.StaticPowderInfo.StaticPowderInfoId;
                _context.Add(lineItem);
                await _context.SaveChangesAsync();

                for (int i = 0; i < lineItem.NumBottles; i++)
                {
                    var newPowder = new Powder
                    {
                        PowderId = Guid.NewGuid(),
                        BottleNumber = "",
                        LotNumber = "",
                        InitWeight = 0,
                        Weight = 0,
                        UserId = new Guid(HttpContext.Session.GetString("userId")),
                        LineItem = lineItem,
                        LineItemId = lineItem.LineItemId,
                        StaticPowderInfo = lineItem.StaticPowderInfo,
                        StaticPowderInfoId = lineItem.StaticPowderInfo.StaticPowderInfoId
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

            var lineItem = await _context.LineItems
                .Include(l => l.PowderOrder)
                .Include(l => l.StaticPowderInfo)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }
            ViewData["StaticPowderInfoId"] = new SelectList(_context.StaticPowderInfo, "StaticPowderInfoId", "PowderName");
            return View(lineItem);
        }

        // POST: LineItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LineItemId,PowderName,VendorDescription,Weight,LineItemCost,ParticleSizeMin,ParticleSizeMax,NumBottles,PowderOrderId,StaticPowderInfoId,NumBottles")] LineItem lineItem)
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
                return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
            }
            return View(lineItem);
        }

        // GET: LineItems/Delete/5
        public async Task<IActionResult> Uninitialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _context.LineItems
                .Include(l => l.PowderOrder)
                .Include(l => l.StaticPowderInfo)
                .FirstOrDefaultAsync(m => m.LineItemId == id);
            if (lineItem == null)
            {
                return NotFound();
            }
          
            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Uninitialize")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UninitializeConfirmed(Guid id)
        {
            var lineItem = await _context.LineItems.FindAsync(id);
            var powders = await _powderRepo.GetLineItemPowders(id);

            // delete lineItem's powders
            foreach(Powder powder in powders)
            {
                _context.Powders.Remove(powder);
            }
            lineItem.IsInitialized = false;
            _context.LineItems.Update(lineItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
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
