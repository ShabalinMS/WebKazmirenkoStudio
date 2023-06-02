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

namespace WebKazmirenkoStudio.Pages.MeasureOfMeasurement
{
    public class EditModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
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

            var measureofmeasurement =  await _context.MeasureOfMeasurement.FirstOrDefaultAsync(m => m.Id == id);
            if (measureofmeasurement == null)
            {
                return NotFound();
            }
            MeasureOfMeasurement = measureofmeasurement;
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

            _context.Attach(MeasureOfMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasureOfMeasurementExists(MeasureOfMeasurement.Id))
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

        private bool MeasureOfMeasurementExists(Guid id)
        {
          return (_context.MeasureOfMeasurement?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
