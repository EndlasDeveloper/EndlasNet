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
    public class IndexModel : PageModel
    {
        private readonly EndlasNet.Data.EndlasNetDbContext _context;

        public IndexModel(EndlasNet.Data.EndlasNetDbContext context)
        {
            _context = context;
        }

        public IList<Admin> Admin { get;set; }

        public async Task OnGetAsync()
        {
            Admin = await _context.Admins.ToListAsync();
        }
    }
}
