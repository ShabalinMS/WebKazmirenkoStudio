using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages
{
    public class EditModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
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

            var rawmaterialcaptionmodel =  await _context.RawMaterialCaption.FirstOrDefaultAsync(m => m.Id == id);
            if (rawmaterialcaptionmodel == null)
            {
                return NotFound();
            }
            RawMaterialCaptionModel = rawmaterialcaptionmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RawMaterialCaptionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RawMaterialCaptionModelExists(RawMaterialCaptionModel.Id))
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

        private bool RawMaterialCaptionModelExists(Guid id)
        {
          return (_context.RawMaterialCaption?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
