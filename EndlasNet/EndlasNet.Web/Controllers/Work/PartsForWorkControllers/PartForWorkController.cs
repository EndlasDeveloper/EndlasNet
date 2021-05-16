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
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            PaginatedList<PartForWork> pagList = null;
            ViewBag.SortOrder = sortOrder;
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.SearchString = searchString;
            ViewBag.PageNumber = pageNumber;

            ViewData["CurrentSort"] = sortOrder;
            ViewBag.SuffixSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";

            ViewBag.WorkTypeJob = String.IsNullOrEmpty(sortOrder) ? "work_type_job" : "";
            ViewBag.WorkTypeWorkOrder = String.IsNullOrEmpty(sortOrder) ? "work_type_work_order" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var partsForWork = await _repo.GetAllPartsForWork();
            List<PartForWork> partForWorkList = partsForWork.ToList();

            // fills in work type at run time
            foreach (PartForWork partForWork in partForWorkList)
            {
                partForWork.WorkType = _repo.GetWorkType(partForWork);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                partForWorkList = partForWorkList.Where(p => p.Work.DueDate
                    .ToString().
                    Contains(searchString)).ToList();
            }


            switch (sortOrder)
            {
                case "suffix_desc":
                    partForWorkList = partForWorkList.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    partForWorkList = partForWorkList.OrderBy(a => a.Suffix).ToList();
                    break;
                case "work_type_job":
                    partForWorkList = partForWorkList.Where(p => p.WorkType == nameof(PartForJob)).ToList();
                    break;
                case "work_type_work_order":
                    partForWorkList = partForWorkList.Where(p => p.WorkType == nameof(PartForWorkOrder)).ToList();
                    break;
                default:
                    partForWorkList = partForWorkList.OrderBy(a => a.Suffix).ToList();
                    break;
            }

            // apply filter

            pagList = PaginatedList<PartForWork>.Create(partForWorkList, pageNumber ?? 1, PaginatedListStaticVariables.PARTS_FOR_WORK_PAGE_SIZE);
            ViewData["PaginatedList"] = pagList;
            return View(pagList);
        }

        private static IEnumerable<PartForWork> FilterWork(string searchString, IEnumerable<PartForWork> partsForWork)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                partsForWork = partsForWork.Where(p => p.Work.DueDate
                    .ToString().
                    Contains(searchString));
            }
            return partsForWork.ToList();
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
