using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.ProductInOrder;

namespace WebKazmirenkoStudio.Pages.ProductInOrder
{
    /// <summary>
    /// Детализация по продукту в заказе
    /// </summary>
    public class DetailsModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Продукт в заказе
        /// </summary>
        public ProductInOrderModel ProductInOrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение данных по заказу
        /// </summary>
        /// <param name="id">ID товара в заказе</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.ProductInOrder == null)
            {
                return NotFound();
            }

            var productinordermodel = await _context.ProductInOrder
                .Include(x=>x.Product)
                .Include(x=>x.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productinordermodel == null)
            {
                return NotFound();
            }
            else 
            {
                ProductInOrderModel = productinordermodel;
            }
            return Page();
        }
    }
}
