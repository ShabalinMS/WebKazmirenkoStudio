using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.Contact;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.Order
{
    /// <summary>
    /// Страница создания заказа
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Статус заказа
        /// </summary>
        public DropdownViewModel statusOrder { get; set; } = default!;

        /// <summary>
        /// Контакт
        /// </summary>
        public DropdownViewModel contact { get; set; } = default!;

        /// <summary>
        /// Заказ
        /// </summary>
        [BindProperty]
        public OrderModel OrderModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Текущей контекст</param>
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
            GetOrderStatusCollection();
            GetContactCollection();
            return Page();
        }
        
        /// <summary>
        /// Сохранение записи
        /// </summary>
        /// <param name="contact">Контакт</param>
        /// <param name="statusOrder">Статус заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel contact, DropdownViewModel statusOrder)
        {
            SetBindingEntity(
                new Guid(contact.SelectedItem),
                new Guid(statusOrder.SelectedItem)
            );

            _context.Order.Add(OrderModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

        /// <summary>
        /// Получение колекции статусов заказа
        /// </summary>
        private void GetOrderStatusCollection()
        {
            statusOrder = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (StatusOrderModel entity in _context.StatusOrder.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            statusOrder.SelectedItem = list.FirstOrDefault().Value;
            statusOrder.Collection = list;
        }

        /// <summary>
        /// Получение контактов
        /// </summary>
        private void GetContactCollection()
        {
            contact = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ContactModel entity in _context.Contact.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.FIO, Value = entity.Id.ToString() });
            }
            contact.SelectedItem = list.FirstOrDefault().Value;
            contact.Collection = list;
        }

        /// <summary>
        /// Привязка данных
        /// </summary>
        /// <param name="contact">ID конакта</param>
        /// <param name="status">ID статуса</param>
        private void SetBindingEntity(Guid contact, Guid status)
        {
            OrderModel.Contact = _context.Contact.Where(x => x.Id.Equals(contact)).FirstOrDefault();
            OrderModel.StatusOrder = _context.StatusOrder.Where(x => x.Id.Equals(status)).FirstOrDefault();
        }

        #endregion
    }
}
