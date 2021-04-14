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
    public class MachiningToolForWorksController : Controller
    {
        private readonly MachiningToolForWorkRepo _machiningToolForWorkRepo;
        private readonly MachiningToolRepo _machiningToolRepo;
        private readonly UserRepo _userRepo;
        private readonly JobRepo _jobRepo;
        private readonly WorkOrderRepo _workOrderRepo;
        public MachiningToolForWorksController(EndlasNetDbContext context)
        {
            _userRepo = new UserRepo(context);
            _machiningToolRepo = new MachiningToolRepo(context);
            _machiningToolForWorkRepo = new MachiningToolForWorkRepo(context);
            _jobRepo = new JobRepo(context);
            _workOrderRepo = new WorkOrderRepo(context);
        }

        // GET: MachiningToolForWorks
        public async Task<IActionResult> Index()
        {
            ViewData["WorkId"] = new SelectList(await _jobRepo.GetAllWork(), "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(await _machiningToolRepo.GetAllRows(), "MachiningToolId", "VendorDescription");

            var toolsForWork = await _machiningToolForWorkRepo.GetAllRows();
            List<MachiningToolForWork> machiningToolForWorkList = new List<MachiningToolForWork>();
            foreach(object obj in toolsForWork)
            {
                machiningToolForWorkList.Add((MachiningToolForWork)obj);
            }

            foreach(MachiningToolForWork toolForWork in machiningToolForWorkList)
            {
                toolForWork.Work = await _jobRepo.GetWork(toolForWork.Work.WorkId);
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

            var machiningToolForWork = await _machiningToolForWorkRepo.GetRow(id);
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

        private async Task SetCreateViewData()
        {
            ViewData["WorkId"] = new SelectList(await _jobRepo.GetAllWork(), "WorkId", "EndlasNumber");
            var availableTools = await _machiningToolRepo.GetAvailableTools();
            
            foreach(MachiningTool machiningTool in availableTools)
            {
                machiningTool.DropDownDisplayReference = machiningTool.VendorDescription + " - " + machiningTool.PurchaseOrderNum;
            }
            // No available tools, so flag a warning to display
            if(availableTools.Count == 0)
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
                var machiningTool = await _machiningToolRepo.GetRow(machiningToolForWork.MachiningToolId);
                machiningTool.ToolCount -= 1;
                machiningToolForWork.MachiningToolForWorkId = Guid.NewGuid();
                machiningToolForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                await _machiningToolForWorkRepo.AddRow(machiningToolForWork);
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
            var machiningToolForWork = await _machiningToolForWorkRepo.GetRow(id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }

            machiningToolForWork.Work = await _jobRepo.GetWork(machiningToolForWork.Work.WorkId);
            machiningToolForWork.MachiningTool = await _machiningToolRepo.GetRow(machiningToolForWork.MachiningToolId);

            ViewData["WorkId"] = new SelectList(await _jobRepo.GetAllWork(), "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(await _machiningToolRepo.GetAllRows(), "MachiningToolId", "VendorDescription");

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
                    await _machiningToolForWorkRepo.UpdateRow(machiningToolForWork);
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

            var machiningToolForWork = await _machiningToolForWorkRepo.GetRow(id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }
            machiningToolForWork.Work = await _jobRepo.GetWork(machiningToolForWork.Work.WorkId);
            machiningToolForWork.MachiningTool = await _machiningToolRepo.GetRow(machiningToolForWork.MachiningToolId);
            machiningToolForWork.User = await _userRepo.GetRow(machiningToolForWork.UserId);
            return View(machiningToolForWork);
        }

        // POST: MachiningToolForWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var machiningToolForWork = await _machiningToolForWorkRepo.GetRow(id);
            machiningToolForWork.MachiningTool = await _machiningToolRepo.GetRow(machiningToolForWork.MachiningToolId);
            machiningToolForWork.MachiningTool.ToolCount++;
            await _machiningToolRepo.UpdateRow(machiningToolForWork.MachiningTool);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MachiningToolForWorkExists(Guid id)
        {
            return await _machiningToolForWorkRepo.RowExists(id);
        }
    }
}
