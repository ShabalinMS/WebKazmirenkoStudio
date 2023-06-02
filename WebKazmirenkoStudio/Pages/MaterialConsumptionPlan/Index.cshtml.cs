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
    public class IndexModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        public IList<MaterialConsumptionPlanModel> MaterialConsumptionPlanModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MaterialConsumptionPlan != null)
            {
                MaterialConsumptionPlanModel = await _context.MaterialConsumptionPlan
                    .Include(x=>x.Product)
                    .Include(x=>x.Warehouse)
                    .Include(x=>x.Warehouse.RawMaterialCaption)
                    .ToListAsync();
            }
        }
    }
}
