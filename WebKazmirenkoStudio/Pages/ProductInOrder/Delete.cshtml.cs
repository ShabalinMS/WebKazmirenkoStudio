using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.ProductInOrder;

namespace WebKazmirenkoStudio.Pages.ProductInOrder
{
    /// <summary>
    /// Удаление продукта в заказе
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Продукты в заказе 
        /// </summary>
        [BindProperty]
        public ProductInOrderModel ProductInOrderModel { get; set; } = default!;


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
        /// <param name="id">Id заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.ProductInOrder == null)
            {
                return NotFound();
            }

            var productinordermodel = await _context.ProductInOrder
                .Include(x=>x.Order)
                .Include(x=>x.Product)
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

        /// <summary>
        /// Удаление продукта из заказа
        /// </summary>
        /// <param name="id">ID продукта в заказе</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.ProductInOrder == null)
            {
                return NotFound();
            }
            var productinordermodel = await _context.ProductInOrder.FindAsync(id);

            if (productinordermodel != null)
            {
                ProductInOrderModel = productinordermodel;
                _context.ProductInOrder.Remove(ProductInOrderModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
