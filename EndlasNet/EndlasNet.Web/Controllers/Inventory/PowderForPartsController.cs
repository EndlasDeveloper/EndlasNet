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
    public class PowderForPartsController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private readonly float POWDER_THRESHOLD = 0.001f;
        public PowderForPartsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: PowderForParts
        public async Task<IActionResult> Index()
        {
            var powderForParts = await GetPowdersList();
            var partsForWork = await GetPartsForWorkList();
            
            var endlasNetDbContext = await _context.PowderForParts
                .Include(p => p.PartForWork)
                .Include(p => p.Powder).ToListAsync();

            foreach(PowderForPart powderForPart in endlasNetDbContext)
            {
                var staticPartInfo = await _context.StaticPartInfo
                    .FirstOrDefaultAsync(s => s.StaticPartInfoId == powderForPart.PartForWork.StaticPartInfoId);

                powderForPart.PartForWork.StaticPartInfo = staticPartInfo;

                var staticPowderInfo = await _context.StaticPowderInfo
                    .FirstOrDefaultAsync(s => s.StaticPowderInfoId == powderForPart.Powder.StaticPowderInfoId);

                powderForPart.Powder.StaticPowderInfo = staticPowderInfo;

                FileURL.SetImageURL(powderForPart.PartForWork.StaticPartInfo);
            }

            return View(endlasNetDbContext);
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
            var powders = await GetPowdersList();
            foreach (Powder powder in powders)
            {           
                powder.PowderName = powder.PowderName + " - " + string.Format("{0:0.0000}", powder.Weight) + " lbs";
            }
            ViewData["PartForWorkId"] = new SelectList(partsForWork, "PartForWorkId", "DrawingNumberSuffix");
            ViewData["PowderId"] = new SelectList(powders, "PowderId", "PowderName");
        }

        // GET: PowderForParts/Create
        public async Task<IActionResult> Create()
        {
            await SetViewData();
            return View();
        }

        // POST: PowderForParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderForPartId,PowderId,PartForWorkId,PowderWeightUsed")] PowderForPart powderForPart)
        {
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
                powderForPart.PowderForPartId = Guid.NewGuid();
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
