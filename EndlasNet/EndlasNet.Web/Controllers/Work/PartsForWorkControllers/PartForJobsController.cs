using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace EndlasNet.Web.Controllers
{
    public class PartForJobsController : Controller
    {
        private PartForJobRepo repo;
        private readonly EndlasNetDbContext _context;
        public PartForJobsController(EndlasNetDbContext context)
        {
            _context = context;
            repo = new PartForJobRepo(context);

        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            var parts = await repo.GetAllPartsForJobsAsync();
            // minimize part list to batched row representation
            var minimizedPartList = await MinimizePartList(parts);

            // set thumbnail image url's
            foreach (PartForJob partForJob in minimizedPartList)
            {
                ImageURL.SetImageURL(partForJob.StaticPartInfo);
            }
            return View(minimizedPartList);
        }

        private async Task<List<PartForJob>> MinimizePartList(List<PartForJob> parts)
        {
            List<PartForJob> minimizedPartList = new List<PartForJob>();
            foreach (PartForJob part in parts)
            {
                KeyValuePair<Guid, Guid> temp = new KeyValuePair<Guid, Guid>(part.WorkId, part.StaticPartInfoId);
                bool flag = false;
                for (int i = 0; i < minimizedPartList.Count; i++)
                {
                    if (minimizedPartList[i].WorkId.Equals(temp.Key))
                        if (minimizedPartList[i].StaticPartInfoId.Equals(temp.Value))
                        {
                            var list = await repo.GetBatch(part.WorkId.ToString(), part.StaticPartInfoId.ToString());
                            minimizedPartList[i].NumParts = list.Count();
                            flag = true;
                        }                        
                }
                if (!flag)
                    minimizedPartList.Add(part);
            }
            return minimizedPartList;
        }

        // GET: PartForJobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partForJob = await repo.GetPartForJobDetailsAsync(id);
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
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,StartSuffix,UserId")] PartForJob partForJob)
        {
            if (ModelState.IsValid)
            {
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await repo.GetExistingPartBatch(partForJob);
                var initCount = partForJob.NumParts;
                partForJob.NumParts += existingBatch.Count;

                // update the number of parts in each PartForJob
                foreach (PartForJob part in existingBatch)
                {
                    part.NumParts += existingBatch.Count;
                }

                // create each part for the part batch
                for (int i = existingBatch.Count; i < initCount + existingBatch.Count; i++)
                {
                    try
                    {
                        var tempPartForJob = partForJob;
                        tempPartForJob.Suffix = PartSuffixGenerator.GetPartSuffix(i);
                        tempPartForJob.PartForWorkId = Guid.NewGuid();
                        tempPartForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                        await repo .AddPartForJobAsync(tempPartForJob);
                    } catch(Exception ex) { ex.ToString(); continue; }
                }
                var partsForJobs = await _context.PartsForJobs.ToListAsync();
                foreach(PartForJob part in partsForJobs)
                {
                    part.NumParts = partForJob.NumParts;
                    await repo.UpdatePartForJobAsync(part);
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

            var partForJob = await repo.DeleteCustomerAsync(id);

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
            await repo.DeletePartForJobConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ViewList(Guid? id, Guid workId, Guid partInfoId)
        {
            return RedirectToAction("Index", "PartsForAJob", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "suffix_asc" });
        }

        private async Task<bool> PartForJobExists(Guid id)
        {
            return await repo.ConfirmPartForJobExistsAsync(id);
        }
    }
}
