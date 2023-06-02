using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebKazmirenkoStudio.Pages.Purchase
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WebKazmirenkoStudio.Model.Purchase Purchase { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FirstOrDefaultAsync(m => m.Id == id);

            if (purchase == null)
            {
                return NotFound();
            }
            else 
            {
                Purchase = purchase;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }
            var purchase = await _context.Purchase.FindAsync(id);

            purchase.Shop = default;

            if (purchase != null)
            {
                Purchase = purchase;
                _context.Purchase.Remove(Purchase);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
