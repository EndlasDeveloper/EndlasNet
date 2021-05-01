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
            List<Work> allWork = new List<Work>();
            var allJobs = await _context.Jobs.ToListAsync();
            foreach(var job in allJobs)
            {
                job.WorkType = "Job";
                allWork.Add(job);
            }
            var allWorkOrders = await _context.WorkOrders.ToListAsync();
            foreach(var workOrder in allWorkOrders)
            {
                workOrder.WorkType = "Work order";
                allWork.Add(workOrder);
            }
            return View(allWork);
        }
    }
}
