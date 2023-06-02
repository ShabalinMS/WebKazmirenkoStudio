using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.Contact;
using WebKazmirenkoStudio.Model.ProductInOrder;

namespace WebKazmirenkoStudio.Pages.ProductInOrder
{
    /// <summary>
    /// Страница создания продуктов в заказе
    /// </summary>
    public class CreateModel : PageModel
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
        /// Продукты в заказе
        /// </summary>
        [BindProperty]
        public ProductInOrderModel ProductInOrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public CreateModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Public

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            GetOrderCollection();
            GetProductCollection();
            return Page();
        }

        /// <summary>
        /// Сохранение записи
        /// </summary>
        /// <param name="order">Заказ</param>
        /// <param name="product">Продукт</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel product, DropdownViewModel order)
        {
            SetBindingEntity(
                new Guid(product.SelectedItem),
                new Guid(order.SelectedItem)

            );

            _context.ProductInOrder.Add(ProductInOrderModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private
        
        /// <summary>
        /// Получение коллекции заказов
        /// </summary>
        private void GetOrderCollection()
        {
            order = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (OrderModel entity in _context.Order.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Id.ToString(), Value = entity.Id.ToString() });
            }
            order.SelectedItem = list.FirstOrDefault().Value;
            order.Collection = list;
        }

        /// <summary>
        /// Получение коллекции продуктов
        /// </summary>
        private void GetProductCollection()
        {
            product = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ProductModel entity in _context.Product.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            product.SelectedItem = list.FirstOrDefault().Value;
            product.Collection = list;
        }

        // <summary>
        /// Привязка данных
        /// </summary>
        /// <param name="contact">ID товара</param>
        /// <param name="status">ID заказа</param>
        private void SetBindingEntity(Guid productId, Guid orderId)
        {
            ProductInOrderModel.Product = _context.Product.Where(x => x.Id.Equals(productId)).FirstOrDefault();
            ProductInOrderModel.Order = _context.Order.Where(x => x.Id.Equals(orderId)).FirstOrDefault();
        }

        #endregion
    }
}
