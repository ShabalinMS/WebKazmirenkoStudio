using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Warehouse
{
    /// <summary>
    /// Дутализация сущности
    /// </summary>
    public class DetailsModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Сущность склада
        /// </summary>
        public WarehouseModel WarehouseModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение данных для карточки
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehousemodel = await _context.Warehouse
                .Include(x => x.MeasureOfMeasurement)
                .Include(x => x.RawMaterialCaption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehousemodel == null)
            {
                return NotFound();
            }
            else 
            {
                WarehouseModel = warehousemodel;
            }
            return Page();
        }
    }
}
