using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.ProductInOrder;

namespace WebKazmirenkoStudio.Pages.ProductInOrder
{
    /// <summary>
    /// Редактирование продукта в заказе
    /// </summary>
    public class EditModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Продукты
        /// </summary>
        public DropdownViewModel product { get; set; } = default!;

        /// <summary>
        /// Заказы
        /// </summary>
        public DropdownViewModel order { get; set; } = default!;

        /// <summary>
        /// Продукт в заказе
        /// </summary>
        [BindProperty]
        public ProductInOrderModel ProductInOrderModel { get; set; } = default!;

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
        /// Получение заказа
        /// </summary>
        /// <param name="id">ID продукта в заказе</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.ProductInOrder == null)
            {
                return NotFound();
            }

            var productinordermodel =  await _context.ProductInOrder.FirstOrDefaultAsync(m => m.Id == id);
            if (productinordermodel == null)
            {
                return NotFound();
            }

            GetOrderCollection(productinordermodel);
            GetProductCollection(productinordermodel);

            ProductInOrderModel = productinordermodel;
            return Page();
        }

        /// <summary>
        /// Добавление продукта в заказ
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(ProductInOrderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInOrderModelExists(ProductInOrderModel.Id))
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
        /// Проверка записи
        /// </summary>
        /// <param name="id">ID продукта в заказе</param>
        /// <returns></returns>
        private bool ProductInOrderModelExists(Guid id)
        {
          return (_context.ProductInOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Получение коллекции заказов
        /// </summary>
        /// <param name="productInOrder">Продукт в заказе</param>
        private void GetOrderCollection(ProductInOrderModel productInOrder)
        {
            order = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (OrderModel entity in _context.Order.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Id.ToString(), Value = entity.Id.ToString() });
            }
            order.SelectedItem = productInOrder.Order.Id.ToString();
            order.Collection = list;
        }


        /// <summary>
        /// Получение коллекции продуктов
        /// </summary>
        /// <param name="productInOrder">Продукт в заказе</param>
        private void GetProductCollection(ProductInOrderModel productInOrder)
        {
            product = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ProductModel entity in _context.Product.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            product.SelectedItem = productInOrder.Product.Id.ToString();
            product.Collection = list;
        }

        #endregion
    }
}
