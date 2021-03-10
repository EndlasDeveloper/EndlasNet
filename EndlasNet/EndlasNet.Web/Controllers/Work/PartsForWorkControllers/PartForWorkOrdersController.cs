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
    public class PartForWorkOrdersController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private PartForWorkOrderRepo _repo;

        public PartForWorkOrdersController(EndlasNetDbContext context)
        {
            _context = context;
            _repo = new PartForWorkOrderRepo(context);
        }

        // GET: PartForWorkOrders
        public async Task<IActionResult> Index()
        {
            var parts = await _repo.GetAllPartsForWorkOrdersAsync();
            // minimize part list to batched row representation
            var minimizedPartList = await PartForWorkUtil.MinimizeWorkOrderPartList(parts, _repo);

            // set thumbnail image url's
            foreach (PartForWorkOrder partForJob in minimizedPartList)
            {
                ImageURL.SetImageURL(partForJob.StaticPartInfo);
            }
            return View(minimizedPartList);
        }

        // GET: PartForWorkOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _repo.GetPartForWorkOrderDetailsAsync(id);
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
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber");
            return View();
        }

        // POST: PartForWorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,StartSuffix,UserId")] PartForWorkOrder partForWorkOrder)
        {
            var resultList = await _context.PartsForWorkOrders
                .Where(p => p.StaticPartInfoId == partForWorkOrder.StaticPartInfoId)
                .ToListAsync();
            var count = resultList.Count;
            int max = -1;
            foreach (PartForWorkOrder pForWorkOrder in resultList)
            {
                var temp = PartSuffixGenerator.SuffixToIndex(pForWorkOrder.Suffix);
                if (temp > max)
                {
                    max = temp;
                }
            }
            if (ModelState.IsValid)
            {
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await _repo.GetExistingPartBatch(partForWorkOrder);
                var initCount = partForWorkOrder.NumParts;
                partForWorkOrder.NumParts += existingBatch.Count;

                // update the number of parts in each PartForJob
                foreach (PartForWorkOrder part in existingBatch)
                {
                    part.NumParts += existingBatch.Count;
                }

                // create each part for the part batch
                for (int i = count; i < initCount + count; i++)
                {
                    try
                    {
                        var tempPartForJob = partForWorkOrder;
                        tempPartForJob.Suffix = PartSuffixGenerator.IndexToSuffix(i);
                        tempPartForJob.PartForWorkId = Guid.NewGuid();
                        tempPartForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                        await _repo.AddPartForWorkOrderAsync(tempPartForJob);
                    }
                    catch (Exception ex) { ex.ToString(); continue; }
                }
                var partsForWorkOrders = await _context.PartsForWorkOrders.ToListAsync();
                foreach (PartForWorkOrder part in partsForWorkOrders)
                {
                    part.NumParts = partForWorkOrder.NumParts;
                    await _repo.UpdatePartForWorkOrderAsync(part);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForWorkOrder.WorkId);
            return View(partForWorkOrder);
        }


        // POST: PartForWorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,UserId")] PartForWorkOrder partForWorkOrder)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
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

        public ActionResult ViewList(Guid? id, Guid workId, Guid partInfoId)
        {
            return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "suffix_asc" });
        }

        private bool PartForWorkOrderExists(Guid id)
        {
            return _context.PartsForWorkOrders.Any(e => e.PartForWorkId == id);
        }
    }
}
