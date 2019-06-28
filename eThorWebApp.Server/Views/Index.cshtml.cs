using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eThorWebApp.Shared.Data;
using eThorWebApp.Shared.Models;

namespace eThorWebApp.Server.Views
{
    public class IndexModel : PageModel
    {
        private readonly eThorWebApp.Shared.Data.eThorTestEntityContext _context;

        public IndexModel(eThorWebApp.Shared.Data.eThorTestEntityContext context)
        {
            _context = context;
        }

        public IList<eThorTestEntity> eThorTestEntity { get;set; }

        public async Task OnGetAsync()
        {
            eThorTestEntity = await _context.eThorTestEntites.ToListAsync();
        }
    }
}
