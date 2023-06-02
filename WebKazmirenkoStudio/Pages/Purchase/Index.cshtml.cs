using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebKazmirenkoStudio.Pages.Purchase
{
    /// <summary>
    /// Закупки
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Ьекущий контекст</param>
        public IndexModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Данные закупки
        /// </summary>
        public IList<WebKazmirenkoStudio.Model.Purchase> Purchase { get;set; } = default!;

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            if (_context.Purchase != null)
            {
                Purchase = await _context.Purchase.Include(x => x.Shop).ToListAsync();
            }
        }
    }
}
