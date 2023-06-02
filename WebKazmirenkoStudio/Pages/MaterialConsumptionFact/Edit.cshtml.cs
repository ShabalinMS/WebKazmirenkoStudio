using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.MaterialConsumptionFact;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionFact
{
    /// <summary>
    /// Редатирование фактического расхода
    /// </summary>
    public class EditModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Заказ
        /// </summary>
        public DropdownViewModel order { get; set; } = default!;

        /// <summary>
        /// Плановый расход
        /// </summary>
        public DropdownViewModel materialConsumptionPlan { get; set; } = default!;

        /// <summary>
        /// Фактический расход
        /// </summary>
        [BindProperty]
        public MaterialConsumptionFactModel MaterialConsumptionFactModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Piblic

        /// <summary>
        /// Получение информации
        /// </summary>
        /// <param name="id">ID фактического расхода</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionFact == null)
            {
                return NotFound();
            }

            var materialconsumptionfactmodel =  await _context.MaterialConsumptionFact.FirstOrDefaultAsync(m => m.Id == id);
            if (materialconsumptionfactmodel == null)
            {
                return NotFound();
            }
            GetMaterialConsumptionPlanCollection(materialconsumptionfactmodel);
            GetOrderCollection(materialconsumptionfactmodel);
            MaterialConsumptionFactModel = materialconsumptionfactmodel;
            return Page();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        /// <param name="materialConsumptionPlan">Плановый расход</param>
        /// <param name="order">ID заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel order, DropdownViewModel materialConsumptionPlan)
        {
            _context.Attach(MaterialConsumptionFactModel).State = EntityState.Modified;

            try
            {
                SetBindingEntity(
                    new Guid(order.SelectedItem),
                    new Guid(materialConsumptionPlan.SelectedItem)
                );

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialConsumptionFactModelExists(MaterialConsumptionFactModel.Id))
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
        /// Проверка записи фактического расхода
        /// </summary>
        /// <param name="id">ID фактического расхода</param>
        /// <returns>Запись существует</returns>
        private bool MaterialConsumptionFactModelExists(Guid id)
        {
          return (_context.MaterialConsumptionFact?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Получение колекции плановых расходов
        /// </summary>
        /// <param name="materialConsumptionFact">Расход по факту</param>
        private void GetMaterialConsumptionPlanCollection(MaterialConsumptionFactModel materialConsumptionFact)
        {
            materialConsumptionPlan = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (WebKazmirenkoStudio.Model.MaterialConsumptionPlanModel entity in _context.MaterialConsumptionPlan.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Id.ToString(), Value = entity.Id.ToString() });
            }
            materialConsumptionPlan.SelectedItem = materialConsumptionFact.MaterialConsumptionPlan.Id.ToString();
            materialConsumptionPlan.Collection = list;
        }

        /// <summary>
        /// Получение колекции заказов
        /// </summary>
        /// <param name="materialConsumptionFact">Расход по факту</param>
        private void GetOrderCollection(MaterialConsumptionFactModel materialConsumptionFact)
        {
            order = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (WebKazmirenkoStudio.Model.OrderModel entity in _context.Order.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Id.ToString(), Value = entity.Id.ToString() });
            }
            order.SelectedItem = materialConsumptionFact.Order.Id.ToString();
            order.Collection = list;
        }

        /// <summary>
        /// Установить связанные параметры
        /// </summary>
        /// <param name="order">ID заказа</param>
        /// <param name="materialConsumptionPlan">ID планового расхода</param>
        private void SetBindingEntity(Guid order, Guid materialConsumptionPlan)
        {
            MaterialConsumptionFactModel.Order = _context.Order.Where(x => x.Id.Equals(order)).FirstOrDefault();
            MaterialConsumptionFactModel.MaterialConsumptionPlan = _context.MaterialConsumptionPlan.Where(x => x.Id.Equals(materialConsumptionPlan)).FirstOrDefault();
        }

        #endregion
    }
}
