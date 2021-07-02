using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using EndlasNet.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EndlasNet.Web.Controllers
{
    public class PowderForPartsController : Controller
    {
        private readonly float WEIGHT_THRESHOLD = 0.001f;

        private readonly IPowderForPartRepo _repo;
        public PowderForPartsController(IPowderForPartRepo repo)
        {
            _repo = repo;
        }

        private void SetIndexViewData(string sortOrder)
        {
            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            ViewBag.PowderBottleDescSortParm = String.IsNullOrEmpty(sortOrder) ? "powder_bottle_desc" : "";
            ViewBag.PowderBottleAscSortParm = String.IsNullOrEmpty(sortOrder) ? "powder_bottle_asc" : "";

            ViewBag.PartDrawingDescSortParm = String.IsNullOrEmpty(sortOrder) ? "part_drawing_desc" : "";
            ViewBag.PartDrawingAscSortParm = String.IsNullOrEmpty(sortOrder) ? "part_drawing_asc" : "";
        }

        private IEnumerable<PowderForPart> SortIndexPowderForParts(IEnumerable<PowderForPart> powderForParts, string sortOrder)
        {
            switch (sortOrder)
            {
                case "suffix_desc":
                    powderForParts = powderForParts.OrderByDescending(p => p.PartForWork.Suffix);
                    break;
                case "suffix_asc":
                    powderForParts = powderForParts.OrderBy(p => p.PartForWork.Suffix);
                    break;
                case "powder_bottle_desc":
                    powderForParts = powderForParts.OrderByDescending(p => p.PowderBottle.PowderName);
                    break;
                case "powder_bottle_asc":
                    powderForParts = powderForParts.OrderByDescending(p => p.PowderBottle.PowderName);
                    powderForParts = powderForParts.Reverse();
                    break;
                case "part_drawing_desc":
                    break;
                case "part_drawing_asc":
                    powderForParts = powderForParts.Reverse();
                    break;
                default:
                    break;
            }
            return powderForParts;
        }
        // GET: PowderForParts
        public async Task<IActionResult> Index(string sortOrder)
        {

            SetIndexViewData(sortOrder);


            var powderForParts = await _repo.GetAllRows();

            powderForParts = SortIndexPowderForParts(powderForParts, sortOrder);


            return View(powderForParts);
        }

        // GET: PowderForParts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderForPart = await _repo.GetRow(id);

            if (powderForPart == null)
            {
                return NotFound();
            }

            return View(powderForPart);
        }

        // GET: PowderForParts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderForPart = await _repo.GetRow(id);
            if (powderForPart == null)
            {
                return NotFound();
            }


            return View(powderForPart);
        }

        // POST: PowderForParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powderForPart = await _repo.GetPowderForPartWithBottles(id);

            var bottle = powderForPart.PowderBottle;
            bottle.Weight += powderForPart.PowderWeightUsed;
            await _repo.UpdatePowderBottle(bottle);
            await _repo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PowderForPartExists(Guid id)
        {
            return _repo.PowderForPartExists(id);
        }

        private async Task PopulateWorkForCreate()
        {
            var works = await _repo.GetAllWorkWithBottles();
            var wl = works.ToList();
            List<Work> workList = new List<Work>();
            foreach(Work work in works)
            {
                var list = work.WorkItems.ToList();
                var wList = list.Where(w => w.PartsForWork.Count() == 0).ToList();
                foreach(WorkItem workItem in wList)
                {
                    workList.Insert(0, work);
                }
            }
            ViewData["WorkId"] = new SelectList(works, "WorkId", "WorkDescription");
            ViewBag.Init = "true";
        }

        [HttpGet]
        public async Task<IActionResult> CreateGetWork()
        {
            await PopulateWorkForCreate();
            return View();
        }

        [HttpPost]
        public IActionResult CreateGetWork([Bind("WorkId,Work,PowderBottleId,PowderWeightUsed,CheckBoxes")] PowderForPartViewModel vm)
        {
            return RedirectToAction("CreateGetWorkItems", new { workId = vm.WorkId, hasEnoughPowder = true, powderLeft = 0,
                selectedCheckboxes = true, powderWeightUsed = 0, dateUsed = DateTime.Now});
        }
        [HttpGet]
        public async Task<IActionResult> CreateGetWorkItems(Guid workId)
        {
            await PopulateWorkItemsForCreate(workId);
            var work = await _repo.GetWork(workId);
            ViewBag.WorkDescription = work.WorkDescription;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateGetWorkItems([Bind("WorkId,Work,WorkItemId,WorkItem,PowderBottleId,PowderWeightUsed,CheckBoxes")] PowderForPartViewModel vm)
        {

            return RedirectToAction("CreateGetWorkItems", new
            {
                workItemId = vm.WorkItemId,
                workId = vm.WorkId,
                hasEnoughPowder = true,
                powderLeft = 0,
                selectedCheckboxes = true,
                powderWeightUsed = 0,
                dateUsed = DateTime.Now
            });
        }

        private async Task PopulateWorkItemsForCreate(Guid workId)
        {
            var workItems = await _repo.GetWorkItemsFromWork(workId);
            var works = await _repo.GetAllWorkWithBottles();
            var wl = works.ToList();
            List<Work> workList = new List<Work>();
            foreach (Work work in works)
            {
                var list = work.WorkItems.ToList();
                var wList = list.Where(w => w.PartsForWork.Count() == 0).ToList();
                foreach (WorkItem workItem in wList)
                {
                    workList.Insert(0, work);
                }
            }
            ViewData["WorkItemId"] = new SelectList(workItems, "WorkItemId", "StaticPartInfo.DrawingNumber");
            ViewBag.Init = "true";
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateWithWorkItemSet([Bind("WorkId,Work,WorkItemId,WorkItem,PowderBottleId,PowderWeightUsed,CheckBoxes")] PowderForPartViewModel vm)
        {
            return RedirectToAction("CreateWithWorkSet", "PowderForParts", new 
                {
                    workItemId = vm.WorkItemId,
                    workId = vm.WorkId,
                    hasEnoughPowder = true,
                    powderLeft = 0,
                    selectedCheckboxes = true,
                    powderWeightUsed = 0,
                    dateUsed = DateTime.Now
                });
        }

        [HttpGet]
        public async Task<IActionResult> CreateWithWorkSet(Guid workItemId, Guid workId, bool hasEnoughPowder, float powderLeft, bool selectedCheckboxes, float powderWeightUsed, DateTime dateUsed)
        {
            if (!selectedCheckboxes)
            {
                ViewBag.NoCheckboxSelect = "true";
            }
            if (!hasEnoughPowder)
            {
                ViewBag.HasEnoughPowder = "false";
                ViewBag.PowderLeft = powderLeft;
            }
            var wi = await _repo.GetWorkItemsFromWork(workId);
            var workItems = wi.FirstOrDefault(w => w.WorkItemId == workItemId);
            var work = await _repo.GetWork(workId);
            ViewBag.DrawingNumber = workItems.StaticPartInfo.DrawingNumber;
            var vm = new PowderForPartViewModel
            {
                Work = work,
                WorkId = work.WorkId,
                DateUsed = dateUsed,
                CheckBoxes = new List<CheckBoxInfo>()
            };
          
            foreach (PartForWork partForWork in workItems.PartsForWork)
            {
                partForWork.WorkItem.Work = await _repo.GetWork((Guid)partForWork.WorkItem.WorkId);

                var checkBox = new CheckBoxInfo()
                {
                    Label = partForWork.Suffix,
                    PartForWorkId = partForWork.PartForWorkId,
                };
                vm.CheckBoxes.Add(checkBox);
            }
            
           
            // sort by suffix (aka label)
            vm.CheckBoxes = vm.CheckBoxes
                .OrderBy(c => c.Label)
                .AsEnumerable()
                .ToList();

            ViewBag.WorkDescription = work.WorkDescription;
            await SetViewData();
            vm.PowderWeightUsed = powderWeightUsed;
            return View(vm);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithWorkSet(PowderForPartViewModel vm)
        {
            var checkedBoxes = vm.CheckBoxes.Where(c => c.IsChecked).ToList();
            if(checkedBoxes.Count == 0)
            {
                return RedirectToAction("CreateWithWorkSet", new { workId = vm.WorkId, hasEnoughPowder = true, powderLeft = 0,
                    selectedCheckboxes = false, powderWeightUsed = vm.PowderWeightUsed, dateUsed = vm.DateUsed });
            }
            // find the bottle of powder associated with powderForParts
            var powder = await _repo.GetPowderBottle(vm.PowderBottleId);

            // make sure there is enough powder to perform putting powder to part
            if (powder.Weight < vm.PowderWeightUsed)
            {
                ViewBag.HasEnoughPowder = "false";
                ViewBag.PowderLeft = string.Format("{0:0.0000}", powder.Weight);
                await SetViewData();
                return RedirectToAction("CreateWithWorkSet", new { workId = vm.WorkId, hasEnoughPowder = false,
                    powderLeft = powder.Weight, selectedCheckboxes = true, powderWeightUsed = vm.PowderWeightUsed, dateUsed = vm.DateUsed });
            }
            powder.Weight -= vm.PowderWeightUsed;
            // if below threshold after subtracting weight, zero out weight
            if (powder.Weight <= WEIGHT_THRESHOLD)
            {
                powder.Weight = 0.0f;
                await _repo.UpdatePowderBottle(powder);
            }
            var weightPerPart = vm.PowderWeightUsed / vm.CheckBoxes.Where(c => c.IsChecked).Count();
            var usrId = new Guid(HttpContext.Session.GetString("userId"));
            foreach(CheckBoxInfo box in vm.CheckBoxes)
            {
                if (box.IsChecked)
                {
                    var powderForPart = new PowderForPart {
                        PartForWorkId = box.PartForWorkId,
                        PowderBottleId = vm.PowderBottleId,
                        PowderForPartId = Guid.NewGuid(),
                        PowderWeightUsed = weightPerPart,
                        DateUsed = vm.DateUsed,
                        UserId = usrId
                    };
                    await _repo.AddRow(powderForPart);
                }
            }
            return RedirectToAction(nameof(Index));
           
        }

        public async Task<List<PartForWork>> GetPartsForWorkList()
        {
            var partsForWork = await _repo.GetPartsForWork();

            foreach (PartForWork partForWork in partsForWork)
            {
               
            }
            return partsForWork.ToList();
        }

        public async Task<List<PowderBottle>> GetPowderBottleList()
        {
            var powders = await _repo.GetBottlesWithPowder(WEIGHT_THRESHOLD);

            foreach (PowderBottle powder in powders)
            {
                powder.StaticPowderInfo = await _repo.GetStaticPowderInfo((Guid)powder.StaticPowderInfoId);

                powder.PowderName = powder.StaticPowderInfo.PowderName + " - " + powder.BottleNumber;
            }
            return powders.ToList();
        }

        public async Task SetViewData()
        {
            var partsForWork = await GetPartsForWorkList();
            var powders = await GetPowderBottleList();
            foreach (PowderBottle powder in powders)
            {
                powder.PowderName = powder.PowderName + " - " + string.Format("{0:0.0000}", powder.Weight) + " lbs";
            }
            ViewData["PartForWorkId"] = new SelectList(partsForWork, "PartForWorkId", "DrawingNumberSuffix");
            ViewData["PowderBottleId"] = new SelectList(powders, "PowderBottleId", "PowderName");
        }

        [HttpGet("PowderForParts/CreateGetWork/{id}")]
        public async Task<IActionResult> getParts()
        {
            HttpContext.Request.RouteValues.TryGetValue("id", out object obj);
            Guid workId = new Guid(obj.ToString());
            IActionResult ret = null;
            var parts = await _repo.GetPartsForWorkSingle(workId);
            if(parts.Count() != 0)
            {
                ViewBag.GotParts = true;
                ret = StatusCode(StatusCodes.Status200OK, parts);
            }
            else
            {
                ret = StatusCode(StatusCodes.Status400BadRequest, "Invalid entity");
            }
            return ret;
        }
    }
}