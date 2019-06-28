using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eThorWebApp.Shared.Data;
using eThorWebApp.Shared.Models;

namespace eThorWebApp.Server.Views
{
    public class CreateModel : PageModel
    {
        private readonly eThorWebApp.Shared.Data.eThorTestEntityContext _context;

        public CreateModel(eThorWebApp.Shared.Data.eThorTestEntityContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public eThorTestEntity eThorTestEntity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.eThorTestEntites.Add(eThorTestEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}