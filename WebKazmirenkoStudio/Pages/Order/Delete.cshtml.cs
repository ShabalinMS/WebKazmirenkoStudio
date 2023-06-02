using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Order
{
    /// <summary>
    /// Удаление заказа
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Заказ
        /// </summary>
        [BindProperty]
        public OrderModel OrderModel { get; set; } = default!;

        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

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
                .Include(x => x.Contact)
                .Include(x => x.StatusOrder)
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

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }
            var ordermodel = await _context.Order.FindAsync(id);

            if (ordermodel != null)
            {
                OrderModel = ordermodel;
                _context.Order.Remove(OrderModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
