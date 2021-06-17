using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using EndlasNet.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndlasNet.Web.Controllers
{
    public class WorkItemsController : Controller
    {
        private readonly IWorkItemRepo _repo;
        public WorkItemsController(IWorkItemRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index(Guid workId)
        {
            Work work = await _repo.GetWork(workId);
            var list = await _repo.GetWorkItemsForWork(work.WorkId);
            ViewBag.EndlasNumber = work.EndlasNumber;
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Initialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _repo.GetRow(id);
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllPartInfo(), "StaticPartInfoId", "PartDescription");
            return View(CreateWorkItemViewModel(workItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initialize(Guid id, [Bind("WorkItemId,WorkItem,StaticPartInfoId,StaticPartInfo,NumPartsForWork,StartDate,CompleteDate,WorkId")] WorkItemViewModel vm)
        {
            if (id != vm.WorkItemId)
            {
                return NotFound();
            }
/*            var resultList = await _repo.GetPartsForJobsWithPartInfo(vm.StaticPartInfoId);
            var count = resultList.Count();
            int max = -1;
            foreach (PartForJob pForJob in resultList)
            {
                var temp = PartSuffixGenerator.SuffixToIndex(pForJob.Suffix);
                if (temp > max)
                    max = temp;
            }
*/
            if (ModelState.IsValid)
            {
                var workItem = await _repo.GetRow(vm.WorkItemId);
                if(workItem == null)
                {
                    return NotFound();
                }

                workItem = vm.CombineWorkItemData(workItem);

                for(int i = 0; i < vm.NumPartsForWork; i++)
                {
                }

                workItem.IsInitialized = true;
                await _repo.UpdateRow(workItem);
                return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Uninitialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _repo.GetRow(id);
            return View(CreateWorkItemViewModel(workItem));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Uninitialize(Guid id, [Bind("WorkItemId,NumPartsForWork,StaticPartInfoId,WorkId")] WorkItemViewModel vm)
        {
            if (id != vm.WorkItemId)
            {
                return NotFound();
            }
            var workItem = await _repo.GetRow(id);

            if (ModelState.IsValid)
            {
                workItem.IsInitialized = false;
                await _repo.UpdateRow(workItem);
                return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _repo.GetRow(id);
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllPartInfo(), "StaticPartInfoId", "PartDescription");
            return View(workItem);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkItemId,StaticPartInfoId,StartDate,CompleteDate,WorkId,IsInitialized")] WorkItem workItem)
        {
            if (id != workItem.WorkItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repo.UpdateRow(workItem);
                return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
            }
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllPartInfo(), "StaticPartInfoId", "PartDescription");

            return View(workItem);

        }
        public async Task<IActionResult> Details(Guid id)
        {
            var workItem = await _repo.GetRow(id);

            if (workItem == null)
            {
                return NotFound();
            }

            return View(CreateWorkItemViewModel(workItem));
        }

        public async Task<IActionResult> ManagePartsForWork(Guid workItemId)
        {
            var items = await _repo.GetAllPartInfo();
            var workItem = await _repo.GetRow(workItemId);

            return RedirectToAction("Index", "PartForJobs", new { workItemId = workItemId });
        }

        private WorkItemViewModel CreateWorkItemViewModel(WorkItem workItem)
        {
            WorkItemViewModel vm = new WorkItemViewModel();
            vm.WorkItemId = workItem.WorkItemId;
            vm.WorkItem = workItem;
            vm.WorkId = workItem.Work.WorkId;
            if(workItem.PartsForWork != null && workItem.PartsForWork.Count() > 0)
            {
                vm.NumPartsForWork = workItem.PartsForWork.Count();
                return vm;
            }
            if(workItem.StaticPartInfoId != null)
            {
                vm.StaticPartInfoId = workItem.StaticPartInfoId;
            }
            if(workItem.StartDate != null)
            {
                vm.StartDate = workItem.StartDate;
            }
            if(workItem.CompleteDate != null)
            {
                vm.CompleteDate = workItem.CompleteDate;
            }
            return vm;

        }
    }
}
