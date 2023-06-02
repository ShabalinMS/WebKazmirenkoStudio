using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionPlan
{
    public class DeleteModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
      public MaterialConsumptionPlanModel MaterialConsumptionPlanModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionPlan == null)
            {
                return NotFound();
            }

            var materialconsumptionplanmodel = await _context.MaterialConsumptionPlan.FirstOrDefaultAsync(m => m.Id == id);

            if (materialconsumptionplanmodel == null)
            {
                return NotFound();
            }
            else 
            {
                MaterialConsumptionPlanModel = materialconsumptionplanmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionPlan == null)
            {
                return NotFound();
            }
            var materialconsumptionplanmodel = await _context.MaterialConsumptionPlan.FindAsync(id);

            if (materialconsumptionplanmodel != null)
            {
                MaterialConsumptionPlanModel = materialconsumptionplanmodel;
                _context.MaterialConsumptionPlan.Remove(MaterialConsumptionPlanModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
