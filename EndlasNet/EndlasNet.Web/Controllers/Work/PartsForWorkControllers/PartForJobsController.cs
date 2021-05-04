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
        private PartForJobRepo _repo;
        private readonly EndlasNetDbContext _context;

        public PartForJobsController(EndlasNetDbContext context)
        {
            _context = context;
            _repo = new PartForJobRepo(context);
        }

        // GET: PartForJob
        public async Task<IActionResult> Index()
        {
            var parts = await _repo.GetAllPartsForJobsAsync();
            // minimize part list to batched row representation
            var minimizedPartList = await PartForWorkUtil.MinimizeJobPartList(parts, _repo);

            // set thumbnail image url's
            foreach (PartForJob partForJob in minimizedPartList)
            {
                FileURL.SetImageURL(partForJob.StaticPartInfo);
            }
            return View(minimizedPartList);
        }

        // GET: PartForJobs/Create
        public IActionResult Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber");
            ViewData["WorkId"] = new SelectList(_context.Jobs, "WorkId", "EndlasNumber");
            return View();
        }

        // POST: PartForJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,StartSuffix,UserId,ImageName,ImageFile")] PartForJob partForJob)
        {
            var resultList = await _context.PartsForJobs
                .Where(p => p.StaticPartInfoId == partForJob.StaticPartInfoId)
                .ToListAsync();
            var count = resultList.Count;
            int max = -1;
            foreach(PartForJob pForJob in resultList)
            {
                var temp = PartSuffixGenerator.SuffixToIndex(pForJob.Suffix);
                if (temp > max)
                    max = temp;
            }
            if (ModelState.IsValid)
            {
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await _repo.GetExistingPartBatch(partForJob);
                var initCount = partForJob.NumParts;
                partForJob.NumParts += existingBatch.Count;

                // update the number of parts in each PartForJob
                foreach (PartForJob part in existingBatch)
                {
                    part.NumParts += existingBatch.Count;
                }

                // create each part for the part batch
                for (int i = count; i < initCount + count; i++)
                {
                    try
                    {
                        var tempPartForJob = partForJob;
                        tempPartForJob.Suffix = PartSuffixGenerator.IndexToSuffix(i);
                        tempPartForJob.PartForWorkId = Guid.NewGuid();
                        tempPartForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                        if (partForJob.ImageFile != null)
                            partForJob.ImageBytes = await FileURL.GetFileBytes(partForJob.ImageFile);
                        await _repo .AddPartForJobAsync(tempPartForJob);
                    } catch(Exception ex) { ex.ToString(); continue; }
                }
                var partsForJobs = await _context.PartsForJobs.ToListAsync();
                foreach(PartForJob part in partsForJobs)
                {
                    part.NumParts = partForJob.NumParts;
                    await _repo.UpdatePartForJobAsync(part);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(_context.Jobs, "WorkId", "EndlasNumber", partForJob.WorkId);
            return View(partForJob);
        }

        public ActionResult ViewList(Guid? id, Guid workId, Guid partInfoId)
        {
            return RedirectToAction("Index", "PartsForAJob", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "suffix_asc" });
        }

        private async Task<bool> PartForJobExists(Guid id)
        {
            return await _repo.ConfirmPartForJobExistsAsync(id);
        }
    }
}
