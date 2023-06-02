using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.MaterialConsumptionFact;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionFact
{
    /// <summary>
    /// Все записи фактического расхода
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Фактический расход
        /// </summary>
        public IList<MaterialConsumptionFactModel> MaterialConsumptionFactModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение информации
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            if (_context.MaterialConsumptionFact != null)
            {
                MaterialConsumptionFactModel = await _context.MaterialConsumptionFact
                    .Include(x=>x.MaterialConsumptionPlan)
                    .Include(x=>x.Order)
                    .ToListAsync();
            }
        }
    }
}
