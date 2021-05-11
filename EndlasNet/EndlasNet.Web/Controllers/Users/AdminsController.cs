using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;

namespace EndlasNet.Web.Controllers
{
    public class AdminsController : Controller
    {
        private IAdminRepo _adminRepo;
        public AdminsController(IAdminRepo repo)
        {
            _adminRepo = repo;
        }

        // GET: Admins
        public async Task<IActionResult> Index(string sortOrder)
        {
         
            ViewBag.FirstNameDescSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
            ViewBag.FirstNameAscSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_asc" : "";

            ViewBag.LastNameDescSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";
            ViewBag.LastNameAscSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_asc" : "";

            ViewBag.EmailDescSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.EmailAscSortParm = String.IsNullOrEmpty(sortOrder) ? "email_asc" : "";

            var admins = await _adminRepo.GetAllAdmins();
            
            switch (sortOrder)
            {
                case "first_name_desc":
                    admins = admins.OrderByDescending(a => a.FirstName);
                    break;
                case "first_name_asc":
                    admins = admins.OrderByDescending(a => a.FirstName);
                    admins = admins.Reverse();
                    break;
                case "last_name_desc":
                    admins = admins.OrderByDescending(a => a.LastName);
                    break;
                case "last_name_asc":
                    admins = admins.OrderByDescending(a => a.LastName);
                    admins = admins.Reverse();
                    break;
                case "email_desc":
                    admins = admins.OrderByDescending(a => a.EndlasEmail);
                    break;
                case "email_asc":
                    admins = admins.OrderByDescending(a => a.EndlasEmail);
                    admins = admins.Reverse();
                    break;
                default:
                    break;
            }
            return View(admins.ToList());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var admin = await _adminRepo.GetRowNoTracking(id);
           
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,EndlasEmail,AuthString")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.UserId = Guid.NewGuid();
                // save emails as lower case
                admin.EndlasEmail = admin.EndlasEmail.ToLower();
                // **** HASH AUTH STRING ****
                admin.AuthString = Utility.Security.ComputeSha256Hash(admin.AuthString);
                // update shadow properties
                await _adminRepo.AddAdmin(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _adminRepo.GetAdmin((Guid)id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,FirstName,LastName,EndlasEmail,AuthString")] Admin admin)
        {
            if (id != admin.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // **** HASH AUTH STRING ****
                    admin.AuthString = Utility.Security.ComputeSha256Hash(admin.AuthString);
                    // update email as lower case
                    admin.EndlasEmail = admin.EndlasEmail.ToLower();
                    await _adminRepo.UpdateAdmin(admin);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await AdminExists(admin.UserId)))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _adminRepo.GetAdmin((Guid)id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _adminRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AdminExists(Guid id)
        {
            return await _adminRepo.RowExists(id);
        }
    }
}
