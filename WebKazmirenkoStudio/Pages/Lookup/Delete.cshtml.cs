using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Lookup
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public LookupModel LookupModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Lookup == null)
            {
                return NotFound();
            }

            var lookupmodel = await _context.Lookup.FirstOrDefaultAsync(m => m.Id == id);

            if (lookupmodel == null)
            {
                return NotFound();
            }
            else 
            {
                LookupModel = lookupmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Lookup == null)
            {
                return NotFound();
            }
            var lookupmodel = await _context.Lookup.FindAsync(id);

            if (lookupmodel != null)
            {
                LookupModel = lookupmodel;
                _context.Lookup.Remove(LookupModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
