using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.StatusOrder
{
    /// <summary>
    /// Страница создания статуса заказа
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Текущий контест
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
        public CreateModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            return Page();
        }        

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.StatusOrder == null || StatusOrderModel == null)
            {
                return Page();
            }

            _context.StatusOrder.Add(StatusOrderModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
