using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.ProductInOrder;

namespace WebKazmirenkoStudio.Pages.ProductInOrder
{
    /// <summary>
    ///  Получение записей продуктов в заказе
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Продукт в заказе
        /// </summary>
        public IList<ProductInOrderModel> ProductInOrderModel { get; set; } = default!;

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
            if (_context.ProductInOrder != null)
            {
                ProductInOrderModel = await _context.ProductInOrder
                    .Include(x=>x.Product)
                    .Include(x=>x.Order)
                    .ToListAsync();
            }
        }
    }
}
