using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.MeasureOfMeasurement
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WebKazmirenkoStudio.Model.MeasureOfMeasurement MeasureOfMeasurement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MeasureOfMeasurement == null)
            {
                return NotFound();
            }

            var measureofmeasurement = await _context.MeasureOfMeasurement.FirstOrDefaultAsync(m => m.Id == id);

            if (measureofmeasurement == null)
            {
                return NotFound();
            }
            else 
            {
                MeasureOfMeasurement = measureofmeasurement;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.MeasureOfMeasurement == null)
            {
                return NotFound();
            }
            var measureofmeasurement = await _context.MeasureOfMeasurement.FindAsync(id);

            if (measureofmeasurement != null)
            {
                MeasureOfMeasurement = measureofmeasurement;
                _context.MeasureOfMeasurement.Remove(MeasureOfMeasurement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
