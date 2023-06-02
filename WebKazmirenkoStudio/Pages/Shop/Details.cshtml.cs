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
    public class DetailsModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

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
    }
}
