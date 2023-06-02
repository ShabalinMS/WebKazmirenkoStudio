using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WebKazmirenkoStudio.Model.RawMaterialCaption RawMaterialCaptionModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.RawMaterialCaption == null)
            {
                return NotFound();
            }

            var rawmaterialcaptionmodel = await _context.RawMaterialCaption.FirstOrDefaultAsync(m => m.Id == id);

            if (rawmaterialcaptionmodel == null)
            {
                return NotFound();
            }
            else 
            {
                RawMaterialCaptionModel = rawmaterialcaptionmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.RawMaterialCaption == null)
            {
                return NotFound();
            }
            var rawmaterialcaptionmodel = await _context.RawMaterialCaption.FindAsync(id);

            if (rawmaterialcaptionmodel != null)
            {
                RawMaterialCaptionModel = rawmaterialcaptionmodel;
                _context.RawMaterialCaption.Remove(RawMaterialCaptionModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
