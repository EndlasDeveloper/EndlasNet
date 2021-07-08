﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using EndlasNet.Web.Models.DropDownViewModels;
namespace EndlasNet.Web.Controllers
{
    public class MachiningToolForWorksController : Controller
    {
        private readonly IMachiningToolForWorkRepo _repo;
        public MachiningToolForWorksController(IMachiningToolForWorkRepo repo)
        {
            _repo = repo;
        }

        // GET: MachiningToolForWorks
        public async Task<IActionResult> Index()
        {
            ViewData["WorkId"] = new SelectList(await _repo.GetAllMachiningToolsForWork(), "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(await _repo.GetAllMachiningTools(), "MachiningToolId", "VendorDescription");

            var toolsForWork = await _repo.GetAllMachiningToolsForWork();
            List<MachiningToolForWork> machiningToolForWorkList = new List<MachiningToolForWork>();
            foreach(MachiningToolForWork tool in toolsForWork)
            {
                machiningToolForWorkList.Add(tool);
            }

            foreach(MachiningToolForWork toolForWork in machiningToolForWorkList)
            {
                toolForWork.Work = await _repo.GetWork((Guid)toolForWork.WorkId);
            }
            return View(toolsForWork);
        }

        // GET: MachiningToolForWorks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningToolForWork = await _repo.GetMachiningToolForWork(id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }
           
            return View(machiningToolForWork);
        }

        // GET: MachiningToolForWorks/Create
        public async Task<IActionResult> Create()
        {
            await SetCreateViewData();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateGetWork()
        {
            ViewData["WorkId"] = new SelectList(await _repo.GetAllWork(), "WorkId", "WorkDescription");
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateGetWork([Bind("WorkId,QuoteId,EndlasNumber,WorkDescription,NumWorkItems,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] Work work)
        {
            return RedirectToAction("CreateGetWorkItems", "MachiningToolForWorks", new { workId = work.WorkId });
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateGetWorkItems(Guid workId)
        {
            var work = await _repo.GetWork(workId);
            ViewBag.WorkDescription = work.WorkDescription;
            var workItems = await _repo.GetWorkItemsForWork(workId);
            List<WorkItemDropDownViewModel> list = new List<WorkItemDropDownViewModel>();
            foreach(WorkItem workItem in workItems)
            {
                var vm = new WorkItemDropDownViewModel(workItem);
                list.Insert(0, vm);
            }
            ViewData["WorkItemId"] = new SelectList(list, "WorkItemId", "DropDownWorkItemDisplayStr");
            return View();
        }

        private async Task SetCreateViewData()
        {

            ViewData["WorkId"] = new SelectList(await _repo.GetAllWork(), "WorkId", "EndlasNumber");
            var availableTools = await _repo.GetAvailableTools();
            
            foreach(MachiningTool machiningTool in availableTools)
            {
                machiningTool.DropDownDisplayReference = machiningTool.VendorDescription + " - " + machiningTool.PurchaseOrderNum;
            }
            // No available tools, so flag a warning to display
            if(availableTools.Count() == 0)
            {
                ViewBag.HasAvailableTools = "false";
            }
            ViewData["MachiningToolId"] = new SelectList(availableTools, "MachiningToolId", "DropDownDisplayReference");            
        }

        // POST: MachiningToolForWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachiningToolForWorkId,DateUsed,WorkId,UserId,MachiningType,Comment,MachiningToolId")] MachiningToolForWork machiningToolForWork)
        {
            if (ModelState.IsValid)
            {
                var machiningTool = await _repo.GetMachiningTool(machiningToolForWork.MachiningToolId);
                machiningTool.ToolCount -= 1;
                machiningToolForWork.MachiningToolForWorkId = Guid.NewGuid();
                machiningToolForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                await _repo.AddMachiningToolForWork(machiningToolForWork);
                return RedirectToAction(nameof(Index));
            }
            return View(machiningToolForWork);
        }

        // GET: MachiningToolForWorks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var machiningToolForWork = await _repo.GetMachiningToolForWork(id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }

            machiningToolForWork.Work = await _repo.GetWork(machiningToolForWork.Work.WorkId);
            machiningToolForWork.MachiningTool = await _repo.GetMachiningTool(machiningToolForWork.MachiningToolId);

            ViewData["WorkId"] = new SelectList(await _repo.GetAllWork(), "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(await _repo.GetAllMachiningTools(), "MachiningToolId", "VendorDescription");

            return View(machiningToolForWork);
        }

        // POST: MachiningToolForWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MachiningToolForWorkId,DateUsed,WorkId,UserId,MachiningType,Comment,MachiningToolId")] MachiningToolForWork machiningToolForWork)
        {
            if (id != machiningToolForWork.MachiningToolForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    machiningToolForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    await _repo.UpdateRow(machiningToolForWork);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await MachiningToolForWorkExists(machiningToolForWork.MachiningToolForWorkId)))
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
            return View(machiningToolForWork);
        }

        // GET: MachiningToolForWorks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningToolForWork = await _repo.GetMachiningToolForWork(id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }
            machiningToolForWork.Work = await _repo.GetWork(machiningToolForWork.Work.WorkId);
            machiningToolForWork.MachiningTool = await _repo.GetMachiningTool(machiningToolForWork.MachiningToolId);
            machiningToolForWork.User = await _repo.GetUser((Guid)machiningToolForWork.UserId);
            return View(machiningToolForWork);
        }

        // POST: MachiningToolForWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var machiningToolForWork = await _repo.GetMachiningToolForWork(id);
            machiningToolForWork.MachiningTool = await _repo.GetMachiningTool(machiningToolForWork.MachiningToolId);
            machiningToolForWork.MachiningTool.ToolCount++;
            await _repo.UpdateMachiningTool(machiningToolForWork.MachiningTool);
            await _repo.DeleteMachiningToolForWork(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MachiningToolForWorkExists(Guid id)
        {
            return await _repo.MachiningToolForWorkExists(id);
        }
    }
}
