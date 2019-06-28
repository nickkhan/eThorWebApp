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
    public class DeleteModel : PageModel
    {
        private readonly eThorWebApp.Shared.Data.eThorTestEntityContext _context;

        public DeleteModel(eThorWebApp.Shared.Data.eThorTestEntityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public eThorTestEntity eThorTestEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            eThorTestEntity = await _context.eThorTestEntites.FirstOrDefaultAsync(m => m.Id == id);

            if (eThorTestEntity == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            eThorTestEntity = await _context.eThorTestEntites.FindAsync(id);

            if (eThorTestEntity != null)
            {
                _context.eThorTestEntites.Remove(eThorTestEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
