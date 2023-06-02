using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionPlan
{
    /// <summary>
    /// Создание планового расхода сырья
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Склад
        /// </summary>
        public DropdownViewModel rawMaterialCaption { get; set; } = default!;

        /// <summary>
        /// Продукт
        /// </summary>
        public DropdownViewModel product { get; set; } = default!;

        /// <summary>
        /// Плановый расход
        /// </summary>
        [BindProperty]
        public MaterialConsumptionPlanModel MaterialConsumptionPlanModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Текущий контекст</param>
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
            GetWarehouseCollection();
            GetProductCollection();
            return Page();
        }

        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="rawMaterialCaption">Наименование сыптя</param>
        /// <param name="product">Продукт</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel rawMaterialCaption, DropdownViewModel product)
        {
            SetBindingEntity(new Guid(rawMaterialCaption.SelectedItem), new Guid(product.SelectedItem));

            _context.MaterialConsumptionPlan.Add(MaterialConsumptionPlanModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

        /// <summary>
        /// Получение товаров со склада
        /// </summary>
        private void GetWarehouseCollection()
        {
            rawMaterialCaption = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (WebKazmirenkoStudio.Model.RawMaterialCaption entity in _context.RawMaterialCaption.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            rawMaterialCaption.SelectedItem = list.FirstOrDefault().Value;
            rawMaterialCaption.Collection = list;
        }

        /// <summary>
        /// Получение колекции продуктов
        /// </summary>
        private void GetProductCollection()
        {
            product = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (ProductModel entity in _context.Product.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            product.SelectedItem = list.FirstOrDefault().Value;
            product.Collection = list;
        }

        /// <summary>
        /// Установить связанные параметры
        /// </summary>
        /// <param name="rawMaterialCaptionId">ID наименование сырья</param>
        /// <param name="productId">ID продукта</param>
        private void SetBindingEntity(Guid rawMaterialCaptionId, Guid productId)
        {
            MaterialConsumptionPlanModel.Warehouse = _context.Warehouse.Where(x => x.RawMaterialCaption.Id.Equals(rawMaterialCaptionId)).FirstOrDefault();
            MaterialConsumptionPlanModel.Product = _context.Product.Where(x => x.Id.Equals(productId)).FirstOrDefault();
        }

        #endregion
    }
}
