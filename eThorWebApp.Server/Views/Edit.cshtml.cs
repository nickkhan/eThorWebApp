using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eThorWebApp.Shared.Data;
using eThorWebApp.Shared.Models;

namespace eThorWebApp.Server.Views
{
    public class EditModel : PageModel
    {
        private readonly eThorWebApp.Shared.Data.eThorTestEntityContext _context;

        public EditModel(eThorWebApp.Shared.Data.eThorTestEntityContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(eThorTestEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eThorTestEntityExists(eThorTestEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool eThorTestEntityExists(int id)
        {
            return _context.eThorTestEntites.Any(e => e.Id == id);
        }
    }
}
