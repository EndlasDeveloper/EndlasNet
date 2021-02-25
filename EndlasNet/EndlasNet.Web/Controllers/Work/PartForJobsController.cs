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
    public class PartForJobsController : Controller
    {
        private readonly EndlasNetDbContext _context;
         
        public PartForJobsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.PartInfoDescSortParm = String.IsNullOrEmpty(sortOrder) ? "part_info_desc" : "";
            ViewBag.PartInfoAscSortParm = String.IsNullOrEmpty(sortOrder) ? "part_info_asc" : "";

            ViewBag.JobDescSortParm = String.IsNullOrEmpty(sortOrder) ? "job_desc" : "";
            ViewBag.JobAscSortParm = String.IsNullOrEmpty(sortOrder) ? "job_asc" : "";

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var parts = await _context.PartsForJobs.Include(p => p.PartInfo).Include(p => p.User).Include(p => p.Work).ToListAsync();
            var minimizedPartList = new List<PartForJob>();
            var used = new List<KeyValuePair<Guid, Guid>>();
            var workIdArr = new string[parts.Count];
            var staticPartIdArr = new string[parts.Count];
            foreach(PartForJob part in parts)
            {
                KeyValuePair<Guid, Guid> temp = new KeyValuePair<Guid, Guid>(part.WorkId, part.StaticPartInfoId);
                bool flag = false;
                for(int i = 0; i < minimizedPartList.Count; i++)
                {
                    if (minimizedPartList[i].WorkId.Equals(temp.Key))
                        if (minimizedPartList[i].StaticPartInfoId.Equals(temp.Value))
                            flag = true;
                }
                if (!flag)
                    minimizedPartList.Add(part);
              
            }

            switch (sortOrder)
            {
                case "suffix_desc":
                    minimizedPartList = minimizedPartList.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    minimizedPartList = minimizedPartList.OrderByDescending(a => a.Suffix).ToList();
                    minimizedPartList.Reverse();
                    break;
                case "job_desc":
                    minimizedPartList = minimizedPartList.OrderByDescending(a => a.Work.EndlasNumber).ToList();
                    break;
                case "job_asc":
                    minimizedPartList = minimizedPartList.OrderByDescending(a => a.Work.EndlasNumber).ToList();
                    parts.Reverse();
                    break;
                case "part_info_desc":
                    minimizedPartList = minimizedPartList.OrderByDescending(a => a.PartInfo.DrawingNumber).ToList();
                    break;
                case "part_info_asc":
                    minimizedPartList = minimizedPartList.OrderByDescending(a => a.PartInfo.DrawingNumber).ToList();
                    minimizedPartList.Reverse();
                    break;
                default:
                    break;
            }
            ViewBag.ListCount = minimizedPartList.Count.ToString();
            return View(minimizedPartList);
        }



        // GET: PartForJobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs
                .Include(p => p.PartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForJob == null)
            {
                return NotFound();
            }

            return View(partForJob);
        }

        // GET: PartForJobs/Create
        public IActionResult Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber");
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber");
            return View();
        }

        // POST: PartForJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,UserId")] PartForJob partForJob)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < partForJob.NumParts; i++)
                {
                    var tempPartForJob = partForJob;
                    tempPartForJob.Suffix = PartSuffixGenerator.GetPartSuffix(i);                              
                    tempPartForJob.PartForWorkId = Guid.NewGuid();
                    tempPartForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Add(tempPartForJob);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForJob.WorkId);
            return View(partForJob);
        }

      
        // GET: PartForJobs/Edit/5
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
            
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForJob.WorkId);
            return View(partForJob);
        }

        // POST: PartForJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,ConditionDescription,InitWeight,NumParts,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,UserId")] PartForJob partForJob)
        {
            if (id != partForJob.PartForWorkId)
            {
                return NotFound();
            }
            partForJob.NumParts = 1;
            if (ModelState.IsValid)
            {
                try
                {
                    partForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Update(partForJob);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber", partForJob.WorkId);
            return View(partForJob);
        }

        // GET: PartForJobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _context.PartsForJobs
                .Include(p => p.PartInfo)
                .Include(p => p.User)
                .Include(p => p.Work)
                .FirstOrDefaultAsync(m => m.PartForWorkId == id);
            if (partForJob == null)
            {
                return NotFound();
            }

            return View(partForJob);
        }

        // POST: PartForJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForJob = await _context.PartsForJobs.FindAsync(id);
            _context.PartsForJobs.Remove(partForJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartForJobExists(Guid id)
        {
            return _context.PartsForJobs.Any(e => e.PartForWorkId == id);
        }
    }
}
