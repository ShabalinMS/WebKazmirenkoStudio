using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Shop
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ShopModel ShopModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Shop == null)
            {
                return NotFound();
            }

            var shopmodel = await _context.Shop.FirstOrDefaultAsync(m => m.Id == id);

            if (shopmodel == null)
            {
                return NotFound();
            }
            else 
            {
                ShopModel = shopmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Shop == null)
            {
                return NotFound();
            }
            var shopmodel = await _context.Shop.FindAsync(id);

            if (shopmodel != null)
            {
                ShopModel = shopmodel;
                _context.Shop.Remove(ShopModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
