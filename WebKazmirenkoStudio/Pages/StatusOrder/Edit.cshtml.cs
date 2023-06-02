using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.StatusOrder
{
    /// <summary>
    /// Страница редактирования статуса заказа
    /// </summary>
    public class EditModel : PageModel
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
        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Public

        /// <summary>
        /// Получение записи
        /// </summary>
        /// <param name="id">ID статус заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.StatusOrder == null)
            {
                return NotFound();
            }

            var statusordermodel =  await _context.StatusOrder.FirstOrDefaultAsync(m => m.Id == id);
            if (statusordermodel == null)
            {
                return NotFound();
            }
            StatusOrderModel = statusordermodel;
            return Page();
        }

        /// <summary>
        /// Изменение записи
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StatusOrderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusOrderModelExists(StatusOrderModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

        /// <summary>
        /// Проверка существования записи
        /// </summary>
        /// <param name="id">ID статуса заказа</param>
        /// <returns></returns>
        private bool StatusOrderModelExists(Guid id)
        {
          return (_context.StatusOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #endregion
    }
}
