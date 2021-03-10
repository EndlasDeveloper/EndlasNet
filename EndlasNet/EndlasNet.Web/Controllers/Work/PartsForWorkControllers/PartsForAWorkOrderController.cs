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
    public class PartsForAWorkOrderController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private PartForWorkOrderRepo repo;
        public PartsForAWorkOrderController(EndlasNetDbContext context)
        {
            _context = context;
            repo = new PartForWorkOrderRepo(context);
        }

        // GET: PartsForAWorkOrder
        public async Task<IActionResult> Index(Guid id, Guid workId, Guid partInfoId, string sortOrder)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var endlasNetDbContext = await repo.GetBatch(workId.ToString(), partInfoId.ToString());
            foreach (PartForWorkOrder partForWorkOrder in endlasNetDbContext)
            {
                partForWorkOrder.StaticPartInfo = await _context.StaticPartInfo
                    .FirstOrDefaultAsync(s => s.StaticPartInfoId == partForWorkOrder.StaticPartInfoId);
                partForWorkOrder.Work = await _context.Work
                    .FirstOrDefaultAsync(s => s.WorkId == partForWorkOrder.WorkId);
            }
            switch (sortOrder)
            {
                case "suffix_desc":
                    endlasNetDbContext = endlasNetDbContext.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    endlasNetDbContext = endlasNetDbContext.OrderByDescending(a => a.Suffix).ToList();
                    endlasNetDbContext = endlasNetDbContext.Reverse();
                    break;
                default:
                    break;
            }

            return View(endlasNetDbContext);
        }
        // GET: PartsForAWorkOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _context.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            ViewBag.id = id;
            ViewBag.workId = partForWorkOrder.WorkId;
            ViewBag.partInfoId = partForWorkOrder.StaticPartInfoId;

            return View(partForWorkOrder);
        }

        public IActionResult BackToList(Guid id, Guid workId, Guid partInfoId)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;
            return RedirectToAction("Index", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "" });
        }

        // GET: PartsForAWorkOrder/Create
        public IActionResult Create()
        {           
            return View();
        }

        // POST: PartsForAWorkOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId")] PartForWorkOrder partForWorkOrder)
        {
            if (ModelState.IsValid)
            {
                partForWorkOrder.PartForWorkId = Guid.NewGuid();
                partForWorkOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));

                _context.Add(partForWorkOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "PartsForAWorkOrder",
                    new { id = partForWorkOrder.PartForWorkId, workId = partForWorkOrder.WorkId,
                        partInfoId = partForWorkOrder.StaticPartInfoId, sortOrder = "" });

            }
            return View(partForWorkOrder);
        }

        // GET: PartsForAWorkOrder/Edit/5
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
            return View(partForWorkOrder);
        }

        // POST: PartsForAWorkOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId")] PartForWorkOrder partForWorkOrder)
        {
            if (id != partForWorkOrder.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    partForWorkOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Update(partForWorkOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartForWorkOrderExists(partForWorkOrder.PartForWorkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = partForWorkOrder.WorkId,
                    partInfoId = partForWorkOrder.StaticPartInfoId, sortOrder = "suffix_asc" });
            }

            return View(partForWorkOrder);
        }

        // GET: PartsForAWorkOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _context.PartsForWorkOrders
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }

            return View(partForWorkOrder);
        }

        // POST: PartsForAWorkOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForWorkOrder = await _context.PartsForWorkOrders.FindAsync(id);
            _context.PartsForWorkOrders.Remove(partForWorkOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = partForWorkOrder.WorkId,
                partInfoId = partForWorkOrder.StaticPartInfoId, sortOrder = "" });
        }

        public ActionResult RedirectToPartForWorkOrder(Guid id)
        {
            return RedirectToAction("Edit", "PartsForAWorkOrder", new { id = id });
        }

        private bool PartForWorkOrderExists(Guid id)
        {
            return _context.PartsForWorkOrders.Any(e => e.PartForWorkId == id);
        }
    }
}
