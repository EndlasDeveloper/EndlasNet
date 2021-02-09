using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;

namespace EndlasNet.Web.Razor.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly EndlasNet.Data.EndlasNetDbContext _context;

        public DetailsModel(EndlasNet.Data.EndlasNetDbContext context)
        {
            _context = context;
        }

        public Admin Admin { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Admin = await _context.Admins.FirstOrDefaultAsync(m => m.UserId == id);

            if (Admin == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
