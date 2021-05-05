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

        private IPartForWorkRepo _repo;
        public PartForWorkController(IPartForWorkRepo repo)
        {
            _repo = repo;
        }

        // GET: PartForWork
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            ViewBag.WorkTypeJob = String.IsNullOrEmpty(sortOrder) ? "work_type_job" : "";
            ViewBag.WorkTypeWorkOrder = String.IsNullOrEmpty(sortOrder) ? "work_type_work_order" : "";

            var partsForWork = await _repo.GetAllPartsForWork();
            List<PartForWork> partsForWorkList = partsForWork.ToList();
            foreach(PartForWork partForWork in partsForWorkList)
            {
                partForWork.WorkType = _repo.GetWorkType(partForWork);
            }

            switch (sortOrder)
            {
                case "suffix_desc":
                    partsForWorkList = partsForWorkList.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    partsForWorkList = partsForWorkList.OrderByDescending(a => a.Suffix).ToList();
                    partsForWorkList.Reverse();
                    break;
                case "work_type_job":
                    var partsForJobs = await _repo.GetAllPartsForJobs();
                    partsForWorkList.Clear();
                    foreach(PartForJob partForJob in partsForWork)
                    {
                        partsForWorkList.Insert(0, partForJob);
                    }
                    break;
                case "work_type_work_order":
                    var partsForWorkOrders = await _repo.GetPartForWorkOrders();
                    partsForWorkList.Clear();
                    foreach (PartForWorkOrder partForWorkOrder in partsForWorkOrders)
                    {
                        partsForWorkList.Insert(0, partForWorkOrder);
                    }
                    break;
                default:
                    break;
            }

            return View(partsForWorkList);
        }

        // GET: PartForWork/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWork = await _repo.GetPartForWork(id);
            if (partForWork == null)
            {
                return NotFound();
            }

            if (partForWork.ImageBytes != null)
                partForWork.ImageUrl = FileURL.GetImageURL(partForWork.ImageBytes);
            return View(partForWork);
        }

        // GET: PartForWork/Create
        public async Task<IActionResult> Create()
        {
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber");
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

                await _repo.AddPartForWork(partForWork);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber", partForWork.StaticPartInfoId);
            return View(partForWork);
        }

        // GET: PartForWork/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWork = await _repo.GetPartForWork(id);
            if (partForWork == null)
            {
                return NotFound();
            }

            if (partForWork.ImageBytes != null)
                partForWork.ImageUrl = FileURL.GetImageURL(partForWork.ImageBytes);
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber", partForWork.StaticPartInfoId);
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
                    await _repo.UpdatePartForWork(partForWork);
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
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber", partForWork.StaticPartInfoId);
            return View(partForWork);
        }

        // GET: PartForWork/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWork = await _repo.GetPartForWork(id);
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
            var partForWork = await _repo.GetPartForWork(id);
            await _repo.DeletePartForWork(partForWork);
            return RedirectToAction(nameof(Index));
        }

        private bool PartForWorkExists(Guid id)
        {
            return _repo.PartForWorkExists(id);
        }
    }
}
