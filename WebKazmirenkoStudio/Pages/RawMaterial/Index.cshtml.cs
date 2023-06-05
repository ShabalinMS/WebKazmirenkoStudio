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
    public class IndexModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        public IList<RawMaterialModel> RawMaterial { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RawMaterial != null)
            {
                RawMaterial = await _context.RawMaterial
                    .Include(x=>x.Purchase)
                    .Include(x=>x.RawMaterialCaption)
                    .Include(x => x.MeasureOfMeasurement)
                    .ToListAsync();
            }
        }
    }
}
