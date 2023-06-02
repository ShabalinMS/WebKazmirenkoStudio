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
    public class DetailsModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

      public WebKazmirenkoStudio.Model.RawMaterial RawMaterial { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.RawMaterial == null)
            {
                return NotFound();
            }

            var rawmaterial = await _context.RawMaterial.Include(x => x.Purchase).Include(x => x.RawMaterialCaption).FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
