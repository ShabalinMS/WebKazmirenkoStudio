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
    public class DetailsModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

      public MaterialConsumptionPlanModel MaterialConsumptionPlanModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionPlan == null)
            {
                return NotFound();
            }

            var materialconsumptionplanmodel = await _context.MaterialConsumptionPlan
                .Include(x=>x.Warehouse)
                .Include(x=>x.Warehouse.RawMaterialCaption)
                .Include(x=>x.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
