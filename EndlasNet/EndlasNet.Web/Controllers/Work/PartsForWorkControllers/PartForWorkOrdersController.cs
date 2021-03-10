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

        // GET: PartForWorkOrders/Create
        public IActionResult Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber");
            ViewData["WorkId"] = new SelectList(_context.WorkOrders, "WorkId", "EndlasNumber");
            return View();
        }

        // POST: PartForWorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,StartSuffix,UserId")] PartForWorkOrder partForWorkOrder)
        {
            // gets list of tools that have count > 0
            var resultList = await _context.PartsForWorkOrders
                .Where(p => p.StaticPartInfoId == partForWorkOrder.StaticPartInfoId)
                .ToListAsync();
            var count = resultList.Count;
            int maxIndex = -1;
            foreach (PartForWorkOrder pForWorkOrder in resultList)
            {
                // convert the suffix to base-10 and find the max converted suffix
                // used to track what suffix a part batch should start with
                var temp = PartSuffixGenerator.SuffixToIndex(pForWorkOrder.Suffix);
                if (temp > maxIndex)
                {
                    maxIndex = temp;
                }
            }
            if (ModelState.IsValid)
            {
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await _repo.GetExistingPartBatch(partForWorkOrder);
                // save initial part for work order count
                var initCount = partForWorkOrder.NumParts;
                partForWorkOrder.NumParts += existingBatch.Count;

                // update the number of parts in each PartForJob
                foreach (PartForWorkOrder part in existingBatch)
                {
                    part.NumParts += existingBatch.Count;
                }

                // create each part for the part batch starting at max index
                for (int i = count; i < initCount + count; i++)
                {
                    try
                    {
                        var tempPartForJob = partForWorkOrder;
                        // set suffix
                        tempPartForJob.Suffix = PartSuffixGenerator.IndexToSuffix(i);
                        tempPartForJob.PartForWorkId = Guid.NewGuid();
                        // save user email
                        tempPartForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                        await _repo.AddPartForWorkOrderAsync(tempPartForJob);
                    }
                    catch (Exception ex) { ex.ToString(); continue; }
                }
                // update the number of parts
                var partsForWorkOrders = await _context.PartsForWorkOrders.ToListAsync();
                foreach (PartForWorkOrder part in partsForWorkOrders)
                {
                    part.NumParts = partForWorkOrder.NumParts;
                    await _repo.UpdatePartForWorkOrderAsync(part);
                }
                // success, back to index
                return RedirectToAction(nameof(Index));
            }
            // fail, keep tracking referenced entities and return the part for work order back to the create view
            ViewData["StaticPartInfoId"] = new SelectList(_context.StaticPartInfo, "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(_context.WorkOrders, "WorkId", "EndlasNumber", partForWorkOrder.WorkId);
            return View(partForWorkOrder);
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
