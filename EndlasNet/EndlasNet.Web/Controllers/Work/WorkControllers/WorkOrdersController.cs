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
    public class WorkOrdersController : Controller
    {
        private readonly WorkOrderRepo _workOrderRepo;
        private readonly CustomerRepo _customerRepo;
        private readonly UserRepo _userRepo;
        public WorkOrdersController(EndlasNetDbContext context)
        {
            _userRepo = new UserRepo(context);
            _workOrderRepo = new WorkOrderRepo(context);
            _customerRepo = new CustomerRepo(context);
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            return View(await _workOrderRepo.GetAllRows());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = (WorkOrder)await _workOrderRepo.GetRow(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId,CustomerId")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                workOrder.WorkId = Guid.NewGuid();
                workOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                await _workOrderRepo.AddRow(workOrder);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerAddress", workOrder.CustomerId);
            ViewData["UserId"] = new SelectList(await _userRepo.GetAllRows(), "UserId", "AuthString", workOrder.UserId);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _workOrderRepo.FindRow(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", workOrder.CustomerId);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkId,EndlasNumber,WorkDescription,Status,PurchaseOrderNum,DueDate,UserId,CustomerId")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    workOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));

                    await _workOrderRepo.UpdateRow(workOrder);
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
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", workOrder.CustomerId);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = (WorkOrder)await _workOrderRepo.GetRow(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _workOrderRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkOrderExists(Guid id)
        {
            return await _workOrderRepo.RowExists(id);
        }
    }
}
