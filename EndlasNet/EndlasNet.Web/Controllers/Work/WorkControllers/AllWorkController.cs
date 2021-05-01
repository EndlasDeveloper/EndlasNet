using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Web.Controllers
{
    public class AllWorkController : Controller
    {
        private readonly EndlasNetDbContext _context;
        public AllWorkController(EndlasNetDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allWork = await _context.Work.ToListAsync();
            foreach(var work in allWork)
            {
               work.WorkType = _context.Entry(work).Property("Discriminator").CurrentValue.ToString();
            }
            return View(allWork);
        }
    }
}
