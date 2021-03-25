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
    public enum CreateCases
    {
        PopulateWork,
        PopulatePartsForWork,
        PopulatePowder,
        PopulatePowderBottles
    }

    public class PowderForPartsController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private readonly float POWDER_THRESHOLD = 0.001f;
        public PowderForPartsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: PowderForParts
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var powderForParts = await GetPowdersList();
            var partsForWork = await GetPartsForWorkList();
            
            var allPowderForParts = await _context.PowderForParts
                .Include(p => p.PartForWork)
                .Include(p => p.Powder).ToListAsync();

            foreach(PowderForPart powderForPart in allPowderForParts)
            {
                var staticPartInfo = await _context.StaticPartInfo
                    .FirstOrDefaultAsync(s => s.StaticPartInfoId == powderForPart.PartForWork.StaticPartInfoId);

                powderForPart.PartForWork.StaticPartInfo = staticPartInfo;

                var staticPowderInfo = await _context.StaticPowderInfo
                    .FirstOrDefaultAsync(s => s.StaticPowderInfoId == powderForPart.Powder.StaticPowderInfoId);

                powderForPart.Powder.StaticPowderInfo = staticPowderInfo;

                FileURL.SetImageURL(powderForPart.PartForWork.StaticPartInfo);
            }
            switch (sortOrder)
            {
                case "suffix_desc":
                    allPowderForParts = allPowderForParts.OrderByDescending(a => a.PartForWork.Suffix).ToList();
                    break;
                case "suffix_asc":
                    allPowderForParts = allPowderForParts.OrderByDescending(a => a.PartForWork.Suffix).ToList();
                    allPowderForParts.Reverse();
                    break;
                default:
                    break;
            }
            return View(allPowderForParts);
        }

        // GET: PowderForParts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderForPart = await _context.PowderForParts
                .Include(p => p.PartForWork)
                .Include(p => p.Powder)
                .FirstOrDefaultAsync(m => m.PowderForPartId == id);

            if (powderForPart == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _context.StaticPartInfo
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == powderForPart.PartForWork.StaticPartInfoId);

            powderForPart.PartForWork.StaticPartInfo = staticPartInfo;

            FileURL.SetImageURL(powderForPart.PartForWork.StaticPartInfo);

            return View(powderForPart);
        }

        public async Task<List<PartForWork>> GetPartsForWorkList()
        {
            var partsForWork = await _context.PartsForWork.ToListAsync();

            foreach (PartForWork partForWork in partsForWork)
            {
                partForWork.StaticPartInfo = await _context.StaticPartInfo
                    .FirstOrDefaultAsync(s => s.StaticPartInfoId == partForWork.StaticPartInfoId);

                partForWork.DrawingNumberSuffix = partForWork.StaticPartInfo.DrawingNumber 
                    + " - " + partForWork.Suffix;
            }
            return partsForWork;
        }

        public async Task<List<Powder>> GetPowdersList() 
        {
            var powders = await _context.Powders.Where(p => p.Weight > POWDER_THRESHOLD).ToListAsync();

            foreach (Powder powder in powders)
            {
                powder.StaticPowderInfo = await _context.StaticPowderInfo
                    .FirstOrDefaultAsync(s => s.StaticPowderInfoId == powder.StaticPowderInfoId);

                powder.PowderName = powder.StaticPowderInfo.PowderName + " - " + powder.BottleNumber;
            }
            return powders;
        }

        public async Task SetViewData()
        {
            var partsForWork = await GetPartsForWorkList();
            foreach(PartForWork partForWork in partsForWork)
            {
                partForWork.Work = await _context.Work.FirstOrDefaultAsync(p => p.WorkId == partForWork.WorkId);
            }
         
            ViewData["PartForWorkId"] = new SelectList(partsForWork, "PartForWorkId", "DrawingNumberSuffix");
        }

        public async Task SetPowdersForDropdown()
        {
            var powders = await GetPowdersList();
            foreach (Powder powder in powders)
            {
                powder.PowderName = powder.PowderName + " - " + string.Format("{0:0.0000}", powder.Weight) + " lbs";
            }
            ViewData["PowderId"] = new SelectList(powders, "PowderId", "PowderName");
        }




        public async Task SetWorkForCreate(Guid? workId)
        {
            var partsForWork = await _context.PartsForWork.Where(p => p.WorkId == workId).ToListAsync();
            ViewData["PartForWorkId"] = new SelectList(partsForWork, "PartForWorkId", "DrawingNumberSuffix");
        }

        private async Task PopulateWorkForCreate()
        {
            var work = await _context.Work.ToListAsync();
            ViewData["WorkId"] = new SelectList(work, "WorkId", "WorkDescription");
            ViewBag.Init = "true";
        }
        // GET: PowderForParts/Create
        public async Task<IActionResult> CreateInitialGet()
        {

            await PopulateWorkForCreate();
            //await SetPowdersForDropdown();
            //await SetViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult CreateInitialPost([Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId,CustomerId")] Work work)
        {
            return RedirectToAction("CreateWithWorkSet", new { workId = work.WorkId });
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> CreateWithWorkSet(Guid? workId)
        {
            var work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == workId);
            var partsForWork = await _context.PartsForWork
                .Where(p => p.WorkId == workId).ToListAsync();
            foreach(PartForWork partForWork in partsForWork)
            {
                partForWork.Work = await _context.Work.FirstOrDefaultAsync(p => p.WorkId == workId);      
            }
            ViewBag.WorkDescription = work.WorkDescription;
            ViewData["PartForWorkId"] = new SelectList(partsForWork, "PartForWorkId", "Suffix");
            return View();
        }

        // POST: PowderForParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderForPartId,PowderId,PartForWorkId,PowderWeightUsed")] PowderForPart powderForPart)
        {
            powderForPart.PowderForPartId = Guid.NewGuid();

         

            if (ModelState.GetValidationState("PartForWorkId") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
            {
                powderForPart.PartForWork = await _context.PartsForWork
                    .FirstOrDefaultAsync(p => p.WorkId == powderForPart.PartForWork.WorkId);
                if(powderForPart != null)
                {
                    ViewBag.IsWorkSet = "true";
                }
                else
                {
                    ViewBag.IsWorkSet = "false";
                }
                return View(powderForPart);
            }
            if (ModelState.IsValid)
            {
                // find the bottle of powder associated with powderForParts
                var powder = await _context.Powders
                    .FirstOrDefaultAsync(p => p.PowderId == powderForPart.PowderId);


                // make sure there is enough powder to perform putting powder to part
                if (powder.Weight < powderForPart.PowderWeightUsed)
                {
                    ViewBag.HasEnoughPowder = "false";
                    ViewBag.PowderLeft = string.Format("{0:0.0000}", powder.Weight);
                    await SetViewData();
                    return View(powderForPart);
                }
                // subtract off what was used
                powder.Weight -= powderForPart.PowderWeightUsed;

                // if below threshold after subtracting weight, zero out weight
                if (powder.Weight <= POWDER_THRESHOLD)
                {
                    powder.Weight = 0.0f;
                    _context.Update(powder);
                    await _context.SaveChangesAsync();
                }
                // all good, so create new powder for part guid and save
                _context.Add(powderForPart);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            await SetViewData();
            return View(powderForPart);
        }



        // GET: PowderForParts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderForPart = await _context.PowderForParts
                .FindAsync(id);
            if (powderForPart == null)
            {
                return NotFound();
            }
            await SetViewData();
            return View(powderForPart);
        }

        // POST: PowderForParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderForPartId,PowderId,PartForWorkId,PowderWeightUsed")] PowderForPart powderForPart)
        {
            if (id != powderForPart.PowderForPartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(powderForPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowderForPartExists(powderForPart.PowderForPartId))
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
            await SetViewData();
            return View(powderForPart);
        }

        // GET: PowderForParts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderForPart = await _context.PowderForParts
                .Include(p => p.PartForWork)
                .Include(p => p.Powder)
                .FirstOrDefaultAsync(m => m.PowderForPartId == id);
            if (powderForPart == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _context.StaticPartInfo
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == powderForPart.PartForWork.StaticPartInfoId);
            powderForPart.PartForWork.StaticPartInfo = staticPartInfo;
            FileURL.SetImageURL(powderForPart.PartForWork.StaticPartInfo);
            return View(powderForPart);
        }

        // POST: PowderForParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powderForPart = await _context.PowderForParts.FindAsync(id);
            _context.PowderForParts.Remove(powderForPart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PowderForPartExists(Guid id)
        {
            return _context.PowderForParts.Any(e => e.PowderForPartId == id);
        }
    }
}
