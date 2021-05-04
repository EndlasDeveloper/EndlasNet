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
    public class PartsForAJobController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private PartForJobRepo repo;
        public PartsForAJobController(EndlasNetDbContext context)
        {
            _context = context;
            repo = new PartForJobRepo(context);
        }

        // GET: PartsForAJob
        public async Task<IActionResult> Index(Guid id, Guid workId, Guid partInfoId, string sortOrder)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var endlasNetDbContext = await repo.GetBatch(workId.ToString(), partInfoId.ToString());
            foreach(PartForJob partForJob in endlasNetDbContext)
            {
                partForJob.StaticPartInfo = await _context.StaticPartInfo
                    .FirstOrDefaultAsync(s => s.StaticPartInfoId == partForJob.StaticPartInfoId);
                partForJob.Work = await _context.Work
                    .FirstOrDefaultAsync(s => s.WorkId == partForJob.WorkId);
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


        // GET: PartsForAJob/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForJob == null)
            {
                return NotFound();
            }
            if(partForJob.ImageBytes != null)
                FileURL.SetImageURL(partForJob);

            ViewBag.id = id;
            ViewBag.workId = partForJob.WorkId;
            ViewBag.partInfoId = partForJob.StaticPartInfoId;

            return View(partForJob);
        }

        public IActionResult BackToList(Guid id, Guid workId, Guid partInfoId)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;
            return RedirectToAction("Index", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "" });
        }

        // GET: PartsForAJob/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartsForAJob/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,ClearImg,ImageName,ImageFile")] PartForJob partForJob)
        {
            if (ModelState.IsValid)
            {
                partForJob.PartForWorkId = Guid.NewGuid();
                partForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                if (partForJob.ImageFile != null)
                    partForJob.ImageBytes = await FileURL.GetFileBytes(partForJob.ImageFile);
                _context.Add(partForJob);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","PartsForAJob", new { id = partForJob.PartForWorkId, workId = partForJob.WorkId, partInfoId = partForJob.StaticPartInfoId, sortOrder = "" });
            }
            return View(partForJob);
        }

        // GET: PartsForAJob/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs.FindAsync(id);
            if (partForJob == null)
            {
                return NotFound();
            }
            if (partForJob.ImageBytes != null)
                FileURL.SetImageURL(partForJob);
            return View(partForJob);
        }

        // POST: PartsForAJob/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,ClearImg,ImageName,ImageFile")] PartForJob partForJob)
        {
            if (id != partForJob.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (partForJob.ImageFile != null && !partForJob.ClearImg)
                    {
                        partForJob.ImageBytes = await FileURL.GetFileBytes(partForJob.ImageFile);
                    }
                    else
                    {
                        partForJob.ImageBytes = null;
                    }
                    partForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    var entry = _context.Entry(partForJob);
                    entry.State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartForJobExists(partForJob.PartForWorkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PartsForAJob", new { id = id, workId = partForJob.WorkId, partInfoId = partForJob.StaticPartInfoId, sortOrder = "suffix_asc" });
            }
            return View(partForJob);
        }

        // GET: PartsForAJob/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs
                .Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForJob == null)
            {
                return NotFound();
            }
            if(partForJob.ImageBytes != null)
                FileURL.SetImageURL(partForJob);

            return View(partForJob);
        }

        // POST: PartsForAJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForJob = await _context.PartsForJobs.FindAsync(id);
            _context.PartsForJobs.Remove(partForJob);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","PartsForAJob",new {id = id, workId = partForJob.WorkId, partInfoId = partForJob.StaticPartInfoId, sortOrder="" });
        }

        public ActionResult RedirectToPartForJob(Guid id)
        {
            return RedirectToAction("Edit", "PartsForAJob", new { id = id });

        }

        private bool PartForJobExists(Guid id)
        {
            return _context.PartsForJobs.Any(e => e.PartForWorkId == id);
        }
    }
}
