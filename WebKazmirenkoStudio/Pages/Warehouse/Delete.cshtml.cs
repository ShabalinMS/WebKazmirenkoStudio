using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Warehouse
{
    /// <summary>
    /// Удаление сущности
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Сущность склада
        /// </summary>
        [BindProperty]
        public WarehouseModel WarehouseModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение данных для отображения
        /// </summary>
        /// <param name="id">Id запис</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehousemodel = await _context.Warehouse.FirstOrDefaultAsync(m => m.Id == id);

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

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }
            var warehousemodel = await _context.Warehouse.FindAsync(id);

            if (warehousemodel != null)
            {
                WarehouseModel = warehousemodel;
                _context.Warehouse.Remove(WarehouseModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
