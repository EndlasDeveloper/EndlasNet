using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using EndlasNet.Web.Models;

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
        public async Task<IActionResult> Index(string sortOrder, string startDate, string endDate,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            PaginatedList<AllPartForWorkViewModel> pagList = null;
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
            ViewData["CurrentStartDate"] = startDate;
            ViewData["CurrentEndDate"] = endDate;
            
            // get all parts for work from repo
            var partsForWork = await _repo.GetAllPartsForWork();
            List<PartForWork> partsForWorkList = partsForWork.ToList();
            List<AllPartForWorkViewModel> vmList = new List<AllPartForWorkViewModel>();

            // fills in work type at run time
            foreach (PartForWork partForWork in partsForWorkList)
            {
                partForWork.WorkType = _repo.GetWorkType(partForWork);
                string month = partForWork.Work.DueDate.Month.ToString();
                if(month.Length == 1)
                {
                    month = "0" + month;
                }
                string day = partForWork.Work.DueDate.Day.ToString();
                if(day.Length == 1)
                {
                    day = "0" + day;
                }
                string year = partForWork.Work.DueDate.Year.ToString();
                var dueDate = year + "/" + month + "/" + day;
                var displayDueDate = month + "/" + day + "/" + year;
                vmList.Insert(0, new AllPartForWorkViewModel { PartForWork = partForWork, WorkDueDate = dueDate, DisplayDueDate = displayDueDate});
            }
            // filter the list by date range
            if (!String.IsNullOrEmpty(startDate) || !String.IsNullOrEmpty(endDate))
            {
                vmList = FilterListByDateRange(startDate, endDate, vmList);
            }
            // then filter the list by searchFilter
            if (!String.IsNullOrEmpty(searchString))
            {
                vmList = FilterListBySearchString(searchString, vmList);
            }

            // sort the list
            switch (sortOrder)
            {
                case "suffix_desc":
                    vmList = vmList.OrderByDescending(vm => vm.PartForWork.Suffix).ToList();
                    break;
                case "suffix_asc":
                    vmList = vmList.OrderBy(vm => vm.PartForWork.Suffix).ToList();
                    break;
                case "work_type_job":
                    vmList = vmList.Where(vm => vm.PartForWork.WorkType == nameof(PartForJob)).ToList();
                    break;
                case "work_type_work_order":
                    vmList = vmList.Where(vm => vm.PartForWork.WorkType == nameof(PartForWorkOrder)).ToList();
                    break;
                default:
                    vmList = vmList.OrderBy(vm => vm.PartForWork.Suffix).ToList();
                    break;
            }
            // paginate the list
            pagList = PaginatedList<AllPartForWorkViewModel>.Create(vmList, pageNumber ?? 1, PaginatedListStaticVariables.PARTS_FOR_WORK_PAGE_SIZE);
            ViewData["PaginatedList"] = pagList;
            return View(pagList);
        }

        private List<AllPartForWorkViewModel> FilterListBySearchString(string searchString, List<AllPartForWorkViewModel> vmList)
        {
            vmList = vmList.Where(vm => vm.PartForWork.Work.Customer != null).ToList();
            vmList = vmList.Where(vm => vm.PartForWork.Work.Customer.CustomerName.Contains(searchString)).ToList();
            return vmList;
        }

        private List<AllPartForWorkViewModel> FilterListByDateRange(string startDate, string endDate, List<AllPartForWorkViewModel> vmList)
        {
            List<AllPartForWorkViewModel> start = null;
            List<AllPartForWorkViewModel> end = null;

            if (!String.IsNullOrEmpty(startDate))
            {       
                startDate = startDate.Replace("-", "/");
                start = vmList.Where(vm => string.Compare(vm.WorkDueDate, startDate, true) >= 0).ToList();
                if (String.IsNullOrEmpty(endDate))
                    return start;
            }
            if (!String.IsNullOrEmpty(endDate))
            {
                endDate = endDate.Replace("-", "/");
                end = vmList.Where(vm => string.Compare(vm.WorkDueDate, endDate, true) <= 0).ToList();
                if (String.IsNullOrEmpty(startDate))
                    return end;
            }

            List<AllPartForWorkViewModel> returnList = new List<AllPartForWorkViewModel>();
            foreach(AllPartForWorkViewModel startVm in start)
            {
                foreach(AllPartForWorkViewModel endVm in end)
                {
                    if(startVm.PartForWork.PartForWorkId == endVm.PartForWork.PartForWorkId)
                    {
                        returnList.Insert(0, startVm);
                    }
                }
            }
            return returnList;
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
