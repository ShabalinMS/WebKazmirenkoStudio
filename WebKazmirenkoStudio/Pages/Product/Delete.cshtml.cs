using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ProductModel ProductModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productmodel = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (productmodel == null)
            {
                return NotFound();
            }
            else 
            {
                ProductModel = productmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }
            var productmodel = await _context.Product.FindAsync(id);

            if (productmodel != null)
            {
                ProductModel = productmodel;
                _context.Product.Remove(ProductModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
