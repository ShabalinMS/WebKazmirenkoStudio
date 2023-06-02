using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Warehouse
{
    /// <summary>
    /// Общий список склада
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Сущности склада
        /// </summary>
        public IList<WarehouseModel> WarehouseModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить данные для страницы
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            if (_context.Warehouse != null)
            {
                WarehouseModel = 
                    await _context.Warehouse
                    .Include(x => x.MeasureOfMeasurement)
                    .Include(x => x.RawMaterialCaption)
                    .ToListAsync();
            }
        }
    }
}
