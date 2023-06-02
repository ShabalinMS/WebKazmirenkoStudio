using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.MaterialConsumptionFact;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionFact
{
    /// <summary>
    /// Добавление фактического расзода
    /// </summary>
    public class CreateModel : PageModel
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
            GetMaterialConsumptionPlanCollection();
            GetOrderCollection();
            return Page();
        }        

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="materialConsumptionPlan">Плановый расход</param>
        /// <param name="order">ID заказа</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel order, DropdownViewModel materialConsumptionPlan)
        {
            SetBindingEntity(
                new Guid(order.SelectedItem), 
                new Guid(materialConsumptionPlan.SelectedItem)
            );
            _context.MaterialConsumptionFact.Add(MaterialConsumptionFactModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

        /// <summary>
        /// Получение колекции плановых расходов
        /// </summary>
        private void GetMaterialConsumptionPlanCollection()
        {
            materialConsumptionPlan = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (WebKazmirenkoStudio.Model.MaterialConsumptionPlanModel entity in _context.MaterialConsumptionPlan.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Id.ToString(), Value = entity.Id.ToString() });
            }
            materialConsumptionPlan.SelectedItem = list.FirstOrDefault().Value;
            materialConsumptionPlan.Collection = list;
        }

        /// <summary>
        /// Получение колекции заказов
        /// </summary>
        private void GetOrderCollection()
        {
            order = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (WebKazmirenkoStudio.Model.OrderModel entity in _context.Order.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Id.ToString(), Value = entity.Id.ToString() });
            }
            order.SelectedItem = list.FirstOrDefault().Value;
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
