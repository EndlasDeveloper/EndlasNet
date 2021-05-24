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
        private IPartForWorkOrderRepo _repo;
        public PartsForAWorkOrderController(IPartForWorkOrderRepo repo)
        {
            _repo = repo;
        }

        // GET: PartsForAWorkOrder
        public async Task<IActionResult> Index(Guid id, Guid workId, Guid partInfoId, string sortOrder)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var endlasNetDbContext = await _repo.GetBatch(workId.ToString(), partInfoId.ToString());
            foreach (PartForWorkOrder partForWorkOrder in endlasNetDbContext)
            {
                partForWorkOrder.StaticPartInfo = await _repo.GetStaticPartInfo(partForWorkOrder.StaticPartInfoId);
                partForWorkOrder.Work = await _repo.GetWork(partForWorkOrder.WorkId);
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

            var partForWorkOrder = await _repo.GetPartForWorkOrderDetailsAsync(id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            ViewBag.id = id;
            ViewBag.workId = partForWorkOrder.WorkId;
            ViewBag.partInfoId = partForWorkOrder.StaticPartInfoId;
            if (partForWorkOrder.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForWorkOrder.PartForWorkImg);

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
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,ClearImg,ImageName,ImageFile,ImageBytes")] PartForWorkOrder partForWorkOrder)
        {
            if (ModelState.IsValid)
            {
                partForWorkOrder.PartForWorkId = Guid.NewGuid();
                partForWorkOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                if (partForWorkOrder.ImageFile != null)
                    partForWorkOrder.PartForWorkImg.ImageBytes = await FileURL.GetFileBytes(partForWorkOrder.ImageFile);
                await _repo.AddPartForWorkOrderAsync(partForWorkOrder);
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

            var partForWorkOrder = await _repo.GetPartForWorkOrderAsync((Guid)id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            if (partForWorkOrder.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForWorkOrder.PartForWorkImg);
            return View(partForWorkOrder);
        }

        // POST: PartsForAWorkOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,ClearImg,ImageName,ImageFile,ImageBytes")] PartForWorkOrder partForWorkOrder)
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
                    if (partForWorkOrder.ImageFile != null && !partForWorkOrder.ClearImg)
                    {
                        partForWorkOrder.PartForWorkImg.ImageBytes = await FileURL.GetFileBytes(partForWorkOrder.ImageFile);
                    }
                    else if (partForWorkOrder.ClearImg)
                    {
                        partForWorkOrder.PartForWorkImg.ImageBytes = null;
                    }
                    await _repo.UpdatePartForWorkOrderAsync(partForWorkOrder);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await PartForWorkOrderExists(partForWorkOrder.PartForWorkId)))
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

            var partForWorkOrder = await _repo.GetPartForWorkOrderAsync((Guid)id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            if (partForWorkOrder.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForWorkOrder.PartForWorkImg);
            return View(partForWorkOrder);
        }

        // POST: PartsForAWorkOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForWorkOrder = await _repo.GetPartForWorkOrder(id);
            await _repo.DeletePartForWorkOrderAsync(partForWorkOrder);
            return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = partForWorkOrder.WorkId,
                partInfoId = partForWorkOrder.StaticPartInfoId, sortOrder = "" });
        }

        public ActionResult RedirectToPartForWorkOrder(Guid id)
        {
            return RedirectToAction("Edit", "PartsForAWorkOrder", new { id = id });
        }

        private async Task<bool> PartForWorkOrderExists(Guid id)
        {
            return await _repo.ConfirmPartForWorkOrderExistsAsync(id);
        }
    }
}
