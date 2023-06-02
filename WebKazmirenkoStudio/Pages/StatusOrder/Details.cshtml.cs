using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.StatusOrder
{
    /// <summary>
    /// Детализация статуса заказа
    /// </summary>
    public class DetailsModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Статус заказа
        /// </summary>
        public StatusOrderModel StatusOrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">контекст</param>
        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение записи
        /// </summary>
        /// <param name="id">ID статуса заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.StatusOrder == null)
            {
                return NotFound();
            }

            var statusordermodel = await _context.StatusOrder.FirstOrDefaultAsync(m => m.Id == id);
            if (statusordermodel == null)
            {
                return NotFound();
            }
            else 
            {
                StatusOrderModel = statusordermodel;
            }
            return Page();
        }
    }
}
