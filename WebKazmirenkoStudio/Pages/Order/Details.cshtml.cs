using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Order
{
    /// <summary>
    /// Детализация заказа
    /// </summary>
    public class DetailsModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Заказ
        /// </summary>
        public OrderModel OrderModel { get; set; } = default!; 

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var ordermodel = await _context.Order
                .Include(x=>x.Contact)
                .Include(x=>x.StatusOrder)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ordermodel == null)
            {
                return NotFound();
            }
            else 
            {
                OrderModel = ordermodel;
            }
            return Page();
        }
    }
}
