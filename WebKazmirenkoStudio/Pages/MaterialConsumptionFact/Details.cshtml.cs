using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.MaterialConsumptionFact;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionFact
{
    /// <summary>
    /// Детализация фактического расхода
    /// </summary>
    public class DetailsModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Фактический расход
        /// </summary>
        public MaterialConsumptionFactModel MaterialConsumptionFactModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение информации
        /// </summary>
        /// <param name="id">ID фактического расхода</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionFact == null)
            {
                return NotFound();
            }

            var materialconsumptionfactmodel = await _context.MaterialConsumptionFact
                .Include(x => x.MaterialConsumptionPlan)
                .Include(x => x.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materialconsumptionfactmodel == null)
            {
                return NotFound();
            }
            else 
            {
                MaterialConsumptionFactModel = materialconsumptionfactmodel;
            }
            return Page();
        }
    }
}
