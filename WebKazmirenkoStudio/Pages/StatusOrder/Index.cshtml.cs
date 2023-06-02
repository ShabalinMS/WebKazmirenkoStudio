using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.StatusOrder
{
    /// <summary>
    /// Клавная страница статуса заказа
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Статусы заказа
        /// </summary>
        public IList<StatusOrderModel> StatusOrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">контекст</param>
        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех данных
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            if (_context.StatusOrder != null)
            {
                StatusOrderModel = await _context.StatusOrder.ToListAsync();
            }
        }
    }
}
