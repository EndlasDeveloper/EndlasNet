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
            return View(await CreateWorkItemViewModel(workItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initialize(Guid id, [Bind("WorkItemId,NumPartsForWork,StaticPartInfoId,WorkId")] WorkItemViewModel vm)
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
            return View(await CreateWorkItemViewModel(workItem));
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

        public IActionResult Edit()
        {
            return View();
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var workItem = await _repo.GetRow(id);

            if (workItem == null)
            {
                return NotFound();
            }

            return View(await CreateWorkItemViewModel(workItem));
        }

        public async Task<IActionResult> ManagePartsForWork(Guid workItemId)
        {
            var items = await _repo.GetAllPartInfo();
            var workItem = await _repo.GetRow(workItemId);

            return RedirectToAction("Index", "PartForJobs", new { workItemId = workItemId });
        }

        private async Task<WorkItemViewModel> CreateWorkItemViewModel(WorkItem workItem)
        {
            if(workItem.PartsForWork != null && workItem.PartsForWork.Count() > 0)
            {
                var part = workItem.PartsForWork.FirstOrDefault();
             
                return new WorkItemViewModel
                {
     
                    WorkItem = workItem,
                    WorkItemId = workItem.WorkItemId,
                    WorkId = workItem.WorkId,
                    NumPartsForWork = workItem.PartsForWork.Count()
            };
            }
            return new WorkItemViewModel { WorkItem = workItem, WorkItemId = workItem.WorkItemId, WorkId = workItem.WorkId, NumPartsForWork = 0 };

        }
    }
}
