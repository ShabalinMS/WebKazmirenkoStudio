using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.Contact;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;

namespace WebKazmirenkoStudio.Pages.Order
{
    /// <summary>
    /// Редактирования заказа
    /// </summary>
    public class EditModel : PageModel
    {
        /// <summary>
        /// Контекст
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
        /// <param name="context">Контекст</param>
        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Public

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

            var ordermodel =  await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (ordermodel == null)
            {
                return NotFound();
            }
            GetOrderStatusCollection(ordermodel);
            GetContactCollection(ordermodel);

            OrderModel = ordermodel;
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
            _context.Attach(OrderModel).State = EntityState.Modified;

            try
            {
                SetBindingEntity(
                    new Guid(contact.SelectedItem),
                    new Guid(statusOrder.SelectedItem)
                );
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(OrderModel.Id))
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
        /// Получение колекции статусов заказа
        /// </summary>
        /// <param name="order">Заказа</param>
        private void GetOrderStatusCollection(WebKazmirenkoStudio.Model.OrderModel order)
        {
            statusOrder = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (StatusOrderModel entity in _context.StatusOrder.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            statusOrder.SelectedItem = order.StatusOrder.Id.ToString();
            statusOrder.Collection = list;
        }

        /// <summary>
        /// Получение контактов
        /// </summary>
        /// <param name="order">Заказа</param>
        private void GetContactCollection(WebKazmirenkoStudio.Model.OrderModel order)
        {
            contact = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ContactModel entity in _context.Contact.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.FIO, Value = entity.Id.ToString() });
            }
            contact.SelectedItem = order.Contact.Id.ToString();
            contact.Collection = list;
        }

        /// <summary>
        /// Проверка существования записи
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns></returns>
        private bool OrderModelExists(Guid id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
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
