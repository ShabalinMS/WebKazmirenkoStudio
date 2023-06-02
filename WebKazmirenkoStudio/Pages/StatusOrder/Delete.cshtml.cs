using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.StatusOrder
{
    /// <summary>
    /// Страница подстверждения удаления статуса заказа
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Статус заказа
        /// </summary>
        [BindProperty]
        public StatusOrderModel StatusOrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Public

        /// <summary>
        /// Получение данных
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

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id">ID статуса заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.StatusOrder == null)
            {
                return NotFound();
            }
            var statusordermodel = await _context.StatusOrder.FindAsync(id);

            if (statusordermodel != null)
            {
                StatusOrderModel = statusordermodel;
                _context.StatusOrder.Remove(StatusOrderModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        #endregion
    }
}
