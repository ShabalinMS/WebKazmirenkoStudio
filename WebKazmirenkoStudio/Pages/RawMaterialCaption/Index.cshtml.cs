using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebKazmirenkoStudio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        public IList<WebKazmirenkoStudio.Model.RawMaterialCaption> RawMaterialCaptionModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RawMaterialCaption != null)
            {
                RawMaterialCaptionModel = await _context.RawMaterialCaption.ToListAsync();
            }
        }
    }
}
