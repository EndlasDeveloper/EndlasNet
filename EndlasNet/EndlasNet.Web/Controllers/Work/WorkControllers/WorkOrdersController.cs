using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EndlasNet.Web.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly IWorkOrderRepo _workOrderRepo;
 
        public WorkOrdersController(IWorkOrderRepo repo)
        {
            _workOrderRepo = repo;
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            return View(await _workOrderRepo.GetAllWorkOrders());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = (WorkOrder)await _workOrderRepo.GetWorkOrder(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            if (workOrder.ProcessSheetNotesPdfBytes != null)
                ViewBag.HasProcessSheet = "true";
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await _workOrderRepo.GetAllCustomers(), "CustomerId", "CustomerName");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] WorkOrder workOrder)
        {
            var work = await _workOrderRepo.GetWorkWithEndlasNumber(workOrder.EndlasNumber);
            var quotes = await _workOrderRepo.GetQuotesWithEndlasNumber(workOrder.EndlasNumber);
            if (work.Count() > 0 || quotes.Count() > 0)
            {
                ViewBag.EndlasNumberConflict = "true";
                ViewData["CustomerId"] = new SelectList(await _workOrderRepo.GetAllCustomers(), "CustomerId", "CustomerName");
                return View(workOrder);
            }
            if (ModelState.IsValid)
            {
                workOrder.WorkId = Guid.NewGuid();
                if (workOrder.ProcessSheetNotesFile != null)
                {
                    workOrder.ProcessSheetNotesPdfBytes = await FileURL.GetFileBytes(workOrder.ProcessSheetNotesFile);
                }
                await _workOrderRepo.AddWorkOrder(workOrder);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(await _workOrderRepo.GetAllCustomers(), "CustomerId", "CustomerAddress", workOrder.CustomerId);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _workOrderRepo.FindWorkOrder(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(await _workOrderRepo.GetAllCustomers(), "CustomerId", "CustomerName", workOrder.CustomerId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,StartDate,PoDate,CompleteDate,UserId,CustomerId,ProcessSheetNotesFile")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var work = await _workOrderRepo.FindDuplicateWork(workOrder);
                    var quotes = await _workOrderRepo.FindDuplicateQuote(workOrder);

                    if (work.Count() > 0 || quotes.Count() > 0)
                    {
                        ViewBag.EndlasNumberConflict = "true";
                        ViewData["CustomerId"] = new SelectList(await _workOrderRepo.GetAllCustomers(), "CustomerId", "CustomerName");
                        return View(workOrder);
                    }

                    if (workOrder.ProcessSheetNotesFile != null)
                    {
                        workOrder.ProcessSheetNotesPdfBytes = await FileURL.GetFileBytes(workOrder.ProcessSheetNotesFile);
                    }
                    if (workOrder.ClearPdf)
                    {
                        workOrder.ProcessSheetNotesPdfBytes = null;
                    }
                    await _workOrderRepo.UpdateWorkOrder(workOrder);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await WorkOrderExists(workOrder.WorkId)))
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
            ViewData["CustomerId"] = new SelectList(await _workOrderRepo.GetAllCustomers(), "CustomerId", "CustomerName", workOrder.CustomerId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = (WorkOrder)await _workOrderRepo.GetWorkOrder(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            if (workOrder.ProcessSheetNotesPdfBytes != null)
                ViewBag.HasProcessSheet = "true";
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _workOrderRepo.DeleteWorkOrder(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkOrderExists(Guid id)
        {
            return await _workOrderRepo.WorkOrderExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadProcessPdf(Guid? myvar)
        {
            if (myvar == null)
            {
                return NotFound();
            }

            var workOrder = await _workOrderRepo.GetWorkOrder(myvar);

            var fileName = workOrder.EndlasNumber + "_process_notes.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(workOrder.ProcessSheetNotesPdfBytes);
            if (ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName);
        }
    }
}
