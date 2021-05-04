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
    public class PartForWorkController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private PartForJobRepo jobRepo;
        private PartForWorkOrderRepo workOrderRepo;
        public PartForWorkController(EndlasNetDbContext context)
        {
            _context = context;
            jobRepo = new PartForJobRepo(context);
            workOrderRepo = new PartForWorkOrderRepo(context);
        }

        // GET: PartForWork
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            ViewBag.WorkTypeJob = String.IsNullOrEmpty(sortOrder) ? "work_type_job" : "";
            ViewBag.WorkTypeWorkOrder = String.IsNullOrEmpty(sortOrder) ? "work_type_work_order" : "";

            var partsForWork = await _context.PartsForWork.ToListAsync();

            foreach(PartForWork partForWork in partsForWork)
            {
                partForWork.WorkType = _context.Entry(partForWork).Property("Discriminator").CurrentValue.ToString();
            }

            switch (sortOrder)
            {
                case "suffix_desc":
                    partsForWork = partsForWork.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    partsForWork = partsForWork.OrderByDescending(a => a.Suffix).ToList();
                    partsForWork.Reverse();
                    break;
                case "work_type_job":
                    var partsForJob = await _context.PartsForJobs.ToListAsync();
                    partsForWork.Clear();
                    foreach(PartForJob partForJob in partsForJob)
                    {
                        partsForWork.Insert(0, partForJob);
                    }
                    break;
                case "work_type_work_order":
                    var partsForWorkOrders = await _context.PartsForWorkOrders.ToListAsync();
                    partsForWork.Clear();
                    foreach (PartForWorkOrder partForWorkOrder in partsForWorkOrders)
                    {
                        partsForWork.Insert(0, partForWorkOrder);
                    }
                    break;
                default:
                    break;
            }

            return View(partsForWork);
        }

        // GET: PartForWork/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWork = await _context.PartsForWork
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForWork == null)
            {
                return NotFound();
            }

            if (partForWork.ImageBytes != null)
                partForWork.ImageUrl = FileURL.GetImageURL(partForWork.ImageBytes);
            return View(partForWork);
        }

        // GET: PartForWork/Create
        public IActionResult Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber");
            return View();
        }

        // POST: PartForWork/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,ImageName,ImageFile,ClearImg,ImageBytes")] PartForWork partForWork)
        {
            if (ModelState.IsValid)
            {
                partForWork.PartForWorkId = Guid.NewGuid();
                partForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                if (partForWork.ImageFile != null)
                    partForWork.ImageBytes= await FileURL.GetFileBytes(partForWork.ImageFile);

                _context.Add(partForWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWork.StaticPartInfoId);
            return View(partForWork);
        }

        // GET: PartForWork/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWork = await _context.PartsForWork.FindAsync(id);
            if (partForWork == null)
            {
                return NotFound();
            }

            if (partForWork.ImageBytes != null)
                partForWork.ImageUrl = FileURL.GetImageURL(partForWork.ImageBytes);
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWork.StaticPartInfoId);
            return View(partForWork);
        }

        // POST: PartForWork/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,ImageName,ImageFile,ClearImg,ImageBytes")] PartForWork partForWork)
        {
            if (id != partForWork.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (partForWork.ImageFile != null)
                        partForWork.ImageBytes = await FileURL.GetFileBytes(partForWork.ImageFile);
                    if (partForWork.ClearImg)
                        partForWork.ImageBytes = null;
                        partForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Update(partForWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartForWorkExists(partForWork.PartForWorkId))
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
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWork.StaticPartInfoId);
            return View(partForWork);
        }

        // GET: PartForWork/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWork = await _context.PartsForWork
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForWork == null)
            {
                return NotFound();
            }

            if (partForWork.ImageBytes != null)
                partForWork.ImageUrl = FileURL.GetImageURL(partForWork.ImageBytes);
            return View(partForWork);
        }

        // POST: PartForWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForWork = await _context.PartsForWork.FindAsync(id);
            _context.PartsForWork.Remove(partForWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartForWorkExists(Guid id)
        {
            return _context.PartsForWork.Any(e => e.PartForWorkId == id);
        }
    }
}
