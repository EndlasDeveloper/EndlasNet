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
    public class PowderBottlesController : Controller
    {
        private readonly IPowderBottleRepo _repo;
       
        public PowderBottlesController(IPowderBottleRepo repo)
        {
            _repo = repo;
        }

        // GET: PowderBottles
        public async Task<IActionResult> Index(Guid lineItemId)
        {
            var lineItem = await _repo.GetLineItem(lineItemId);
            var powOrder = await _repo.GetPowderOrder(lineItem.PowderOrderId);
            var staticPow = await _repo.GetStaticPowderInfo((Guid)lineItem.StaticPowderInfoId);

            ViewBag.LineItemVendorDescription = lineItem.VendorDescription;
            ViewBag.PowderOrderNum = powOrder.PurchaseOrderNum;
            ViewBag.PowderName = staticPow.PowderName;
            ViewBag.LineItemId = lineItemId.ToString();

            var powders = await _repo.GetLineItemPowders(lineItemId);
           
            return View(powders);
        }


        public async Task<IActionResult> BackToLineItems(Guid lineItemId)
        {
            var lineItem = await _repo.GetLineItem(lineItemId);
            return RedirectToAction("Index", "LineItems", new { powderOrderId = lineItem.PowderOrderId });
        }

        private void SetIndexViewData(string sortOrder)
        {
            ViewBag.PowderNameDescSortParm = String.IsNullOrEmpty(sortOrder) ? "powder_name_desc" : "";
            ViewBag.PowderNameAscSortParm = String.IsNullOrEmpty(sortOrder) ? "powder_name_asc" : "";

            ViewBag.BottleNumberDescSortParm = String.IsNullOrEmpty(sortOrder) ? "bottle_number_desc" : "";
            ViewBag.BottleNumberAscSortParm = String.IsNullOrEmpty(sortOrder) ? "bottle_number_asc" : "";

            ViewBag.LotNumberDescSortParm = String.IsNullOrEmpty(sortOrder) ? "lot_number_desc" : "";
            ViewBag.LotNumberAscSortParm = String.IsNullOrEmpty(sortOrder) ? "lot_number_asc" : "";

            ViewBag.WeightDescSortParm = String.IsNullOrEmpty(sortOrder) ? "weight_desc" : "";
            ViewBag.WeightAscSortParm = String.IsNullOrEmpty(sortOrder) ? "weight_asc" : "";

            ViewBag.CostPerPoundDescSortParm = String.IsNullOrEmpty(sortOrder) ? "cost_per_lb_desc" : "";
            ViewBag.CostPerPoundAscSortParm = String.IsNullOrEmpty(sortOrder) ? "cost_per_lb_asc" : "";
        }

        private async Task<IEnumerable<PowderBottle>> InitializePowderViewForIndex()
        {
            var powderOrders = await _repo.GetAllPowderOrders();

            List<List<PowderBottle>> lineItemBottles = await _repo.SetCostPerPound(powderOrders.ToList());

            List<PowderBottle> powders = new List<PowderBottle>();
            foreach (List<PowderBottle> list in lineItemBottles)
            {
                foreach (PowderBottle b in list)
                {
                    powders.Add(b);
                }
            }
            // default orderinig to ascending wrt bottle number
            return powders.OrderBy(p => p.BottleNumber);
        }

        private IEnumerable<PowderBottle> SortPowderBottlesForIndex(IEnumerable<PowderBottle> powders, string sortOrder)
        {
            switch (sortOrder)
            {
                case "powder_name_desc":
                    powders = powders.OrderByDescending(p => p.PowderName).ToList();
                    break;
                case "powder_name_asc":
                    powders = powders.OrderBy(p => p.PowderName).ToList();
                    break;
                case "bottle_number_desc":
                    powders = powders.OrderByDescending(p => p.BottleNumber).ToList();
                    break;
                case "bottle_number_asc":
                    powders = powders.OrderBy(p => p.BottleNumber).ToList();
                    break;
                case "lot_number_desc":
                    powders = powders.OrderByDescending(p => p.LotNumber).ToList();
                    break;
                case "lot_number_asc":
                    powders = powders.OrderBy(p => p.LotNumber).ToList();
                    break;
                case "weight_desc":
                    powders = powders.OrderByDescending(p => p.Weight).ToList();
                    break;
                case "weight_asc":
                    powders = powders.OrderBy(p => p.Weight).ToList();
                    break;
                case "cost_per_lb_desc":
                    powders = powders.OrderByDescending(p => p.CostPerPound).ToList();
                    break;
                case "cost_per_lb_asc":
                    powders = powders.OrderBy(p => p.CostPerPound).ToList();
                    break;
                default:
                    powders = powders.OrderBy(p => p.BottleNumber).ToList();
                    break;
            }
            return powders;
        }

        public async Task<IActionResult> AllPowderIndex(string sortOrder)
        {
            SetIndexViewData(sortOrder);
            var powders = await InitializePowderViewForIndex();
            powders = SortPowderBottlesForIndex(powders, sortOrder);
            return View(powders);
        }


        // GET: PowderBottles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _repo.GetRowNoTracking(id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // GET: PowderBottles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowderBottles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderBottleId,BottleNumber,InitWeight,Weight,LotNumber,LineItemId,UserId,StaticPowderInfoId")] PowderBottle powder, Guid id)
        {
            if (ModelState.IsValid)
            {
                powder.PowderBottleId = Guid.NewGuid();
                _repo.SetCreatedDate(powder);
                _repo.SetUpdatedDate(powder);
                powder.Weight = powder.InitWeight;
                await _repo.AddRow(powder);
                return RedirectToAction(nameof(Index));
            }

            return View(powder);
        }

        // GET: PowderBottles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _repo.GetRow(id);
            if (powder == null)
            {
                return NotFound();
            }
 
            return View(powder);
        }

        // POST: PowderBottles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderBottleId,BottleNumber,InitWeight,Weight,LotNumber,LineItemId,UserId,StaticPowderInfoId")] PowderBottle powder)
        {
            if (id != powder.PowderBottleId)
            {
                return NotFound();
            }

            if (!powder.IsWeightValid)
            {
                ViewBag.IsWeightValid = "false";
                return View(powder);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.SetCreatedDate(powder);
                    _repo.SetUpdatedDate(powder);
                    await _repo.UpdateRow(powder);
                    _repo.ModifyRow(powder);
                    if(await _repo.BottleNumberLotNumberExists(powder))
                    {
                        ViewBag.BottleNumberConflict = "true";
                        return View(powder);
                    }
                    await _repo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await PowderExists(powder.PowderBottleId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PowderBottles", new { lineItemId = powder.LineItemId });
            }

            return View(powder);
        }



        // GET: PowderBottles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _repo.GetRow(id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // POST: PowderBottles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powder = await _repo.GetRow(id);
            await _repo.DeleteRow(powder);
            return RedirectToAction("Index", new { lineItemId = powder.LineItemId });
        }

        private async Task<bool> PowderExists(Guid id)
        {
            return await _repo.RowExists(id);
        }
        private async Task<string> GetPowderName(Guid? staticInfoId)
        {
            var staticPowInfo = await _repo.GetStaticPowderInfo((Guid)staticInfoId);
            return staticPowInfo.PowderName;
        }
    }
}
