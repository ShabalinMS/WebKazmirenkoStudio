using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Order
{
    /// <summary>
    /// Все записи заказа
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Заказ
        /// </summary>
        public IList<OrderModel> OrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            if (_context.Order != null)
            {
                OrderModel = await _context.Order
                    .Include(x=>x.Contact)
                    .Include(x=>x.StatusOrder)
                    .ToListAsync();
            }
        }
    }
}
