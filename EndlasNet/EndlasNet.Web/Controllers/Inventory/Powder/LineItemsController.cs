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
    public class LineItemsController : Controller
    {
        private readonly ILineItemRepo _repo;

        public LineItemsController(ILineItemRepo repo)
        {
            _repo = repo;
        }

        // GET: LineItems

        public async Task<IActionResult> Index(Guid powderOrderId)
        {
            ViewBag.PurchaseOrderNum = await _repo.GetPurchaseOrderNumber(powderOrderId);
            return View(await _repo.GetLineItems(powderOrderId));  
        }

        // GET: LineItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _repo.GetRow(id);
            lineItem.PowderOrder = await _repo.GetPowderOrder(lineItem.PowderOrderId);
            if (lineItem == null)
            {
                return NotFound();
            }
            
            return View(lineItem);
        }

        public IActionResult ManagePowders(Guid lineItemId, string powderName)
        {
            return RedirectToAction("Index", "PowderBottles", new { lineItemId = lineItemId, powderName = powderName});
        }


        public async Task<IActionResult> Create(Guid powderOrderId)
        {
            ViewBag.PowderOrderId = powderOrderId;
            ViewData["StaticPowderInfoId"] = new SelectList(await _repo.GetAllStaticPowderInfo(), "StaticPowderInfoId", "PowderName");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Initialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _repo.GetLineItemInclude(id);

            ViewData["StaticPowderInfoId"] = new SelectList(await _repo.GetAllStaticPowderInfo(), "StaticPowderInfoId", "PowderName");
            return View(lineItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initialize([Bind("LineItemId,StaticPowderInfoId,VendorDescription,Weight,LineItemCost,ParticleSizeMin,ParticleSizeMax,PowderOrderId,NumBottles,CertPdfFile")] LineItem lineItem)
        {
            lineItem.IsInitialized = true;
            lineItem.StaticPowderInfo = await _repo.GetStaticPowderInfo((Guid)lineItem.StaticPowderInfoId);
            if(lineItem.CertPdfFile != null)
            {
                lineItem.CertPdfBytes = await FileURL.GetFileBytes(lineItem.CertPdfFile);
            }
            await _repo.UpdateRow(lineItem);
            List<PowderBottle> bottles = new List<PowderBottle>();
            for (int i = 0; i < lineItem.NumBottles; i++)
            {
                bottles.Add(new PowderBottle
                {
                    PowderBottleId = Guid.NewGuid(),
                    BottleNumber = "",
                    LotNumber = "",
                    InitWeight = 0,
                    Weight = 0,
                    UserId = new Guid(HttpContext.Session.GetString("userId")),
                    LineItem = lineItem,
                    LineItemId = lineItem.LineItemId,
                    StaticPowderInfo = lineItem.StaticPowderInfo,
                    StaticPowderInfoId = lineItem.StaticPowderInfo.StaticPowderInfoId
                });
                
            }
            await _repo.AddPowderBottles(bottles);

            lineItem.PowderOrder = await _repo.GetPowderOrder(lineItem.PowderOrderId);
            return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
        }
        
        // POST: LineItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineItemId,StaticPowderInfoId,VendorDescription,Weight,LineItemCost,ParticleSizeMin,ParticleSizeMax,PowderOrderId,NumBottles")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                lineItem.LineItemId = Guid.NewGuid();
                lineItem.StaticPowderInfo = await _repo.GetStaticPowderInfo((Guid)lineItem.StaticPowderInfoId);
                lineItem.StaticPowderInfoId = lineItem.StaticPowderInfo.StaticPowderInfoId;

                await _repo.AddRow(lineItem);
                List<PowderBottle> bottles = new List<PowderBottle>();
                for (int i = 0; i < lineItem.NumBottles; i++)
                {
                    var newPowder = new PowderBottle
                    {
                        PowderBottleId = Guid.NewGuid(),
                        BottleNumber = "",
                        LotNumber = "",
                        InitWeight = 0,
                        Weight = 0,
                        UserId = new Guid(HttpContext.Session.GetString("userId")),
                        LineItem = lineItem,
                        LineItemId = lineItem.LineItemId,
                        StaticPowderInfo = lineItem.StaticPowderInfo,
                        StaticPowderInfoId = lineItem.StaticPowderInfo.StaticPowderInfoId
                    };
                    bottles.Add(newPowder);
                }
                await _repo.AddPowderBottles(bottles);
                return RedirectToAction("Index", "PowderOrders");
            }
            return View(lineItem);
        }

        // GET: LineItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _repo.GetLineItemInclude(id);

            if (lineItem == null)
            {
                return NotFound();
            }

            ViewData["StaticPowderInfoId"] = new SelectList(await _repo.GetAllStaticPowderInfo(), "StaticPowderInfoId", "PowderName");
            return View(lineItem);
        }

        // POST: LineItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LineItemId,PowderName,VendorDescription,Weight,LineItemCost,ParticleSizeMin,ParticleSizeMax,NumBottles,PowderOrderId,StaticPowderInfoId,NumBottles")] LineItem lineItem)
        {
            if (id != lineItem.LineItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lineItem.IsInitialized = true;
                    await _repo.UpdateRow(lineItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await LineItemExists(lineItem.LineItemId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
            }
            return View(lineItem);
        }

        // GET: LineItems/Delete/5
        public async Task<IActionResult> Uninitialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _repo.GetLineItemInclude(id);

            if (lineItem == null)
            {
                return NotFound();
            }
          
            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Uninitialize")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UninitializeConfirmed(Guid id)
        {
            var lineItem = (LineItem)await _repo.GetRow(id);
            var powders = await _repo.GetLineItemPowder(lineItem);

            // delete lineItem's powders
            _repo.RemovePowderBottles(powders.ToList());
            lineItem.IsInitialized = false;

            await _repo.UpdateRow(lineItem);

            return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
        }

        public ActionResult ViewList(Guid lineItemId, Guid powderOrderId)
        {
            return RedirectToAction("Index", "PowderBottles", new {powderOrderId = powderOrderId, lineItemId = lineItemId});
        }
        private async Task<bool> LineItemExists(Guid id)
        {
            return await _repo.RowExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadCertPdf(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItem = await _repo.GetRow(id);

            var fileName = "LineItemCert-" + DateTime.Now.ToString() + ".pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(lineItem.CertPdfBytes);
            if (ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName);
        }

    }
}
