using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.RawMaterial
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WebKazmirenkoStudio.Model.RawMaterialModel RawMaterial { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.RawMaterial == null)
            {
                return NotFound();
            }

            var rawmaterial = await _context.RawMaterial.FirstOrDefaultAsync(m => m.Id == id);

            if (rawmaterial == null)
            {
                return NotFound();
            }
            else 
            {
                RawMaterial = rawmaterial;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.RawMaterial == null)
            {
                return NotFound();
            }
            var rawmaterial = await _context.RawMaterial.FindAsync(id);

            if (rawmaterial != null)
            {
                RawMaterial = rawmaterial;
                _context.RawMaterial.Remove(RawMaterial);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
