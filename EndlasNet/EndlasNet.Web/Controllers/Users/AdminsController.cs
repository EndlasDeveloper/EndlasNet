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
        private readonly EndlasNetDbContext _context;
        private AdminRepo repo;
        public AdminsController(EndlasNetDbContext context)
        {
            _context = context;
            repo = new AdminRepo(context);
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

            var admins = await repo.GetAll();
            
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

            var admin = await _context.Admins.AsNoTracking()
                .FirstOrDefaultAsync(m => m.UserId == id);
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

                // **** HASH AUTH STRING ****
                admin.AuthString = ShaHash.ComputeSha256Hash(admin.AuthString);
                // update shadow properties
                _context.Add(admin);
                await _context.SaveChangesAsync();
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

            var admin = await _context.Admins.FindAsync(id);
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
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.UserId))
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

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.UserId == id);
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
            var admin = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(Guid id)
        {
            return _context.Admins.Any(e => e.UserId == id);
        }
    }
}
