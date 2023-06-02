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
    public class IndexModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        public IList<WebKazmirenkoStudio.Model.MeasureOfMeasurement> MeasureOfMeasurement { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MeasureOfMeasurement != null)
            {
                MeasureOfMeasurement = await _context.MeasureOfMeasurement.ToListAsync();
            }
        }
    }
}
