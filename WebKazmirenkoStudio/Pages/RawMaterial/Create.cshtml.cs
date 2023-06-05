using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Utils.Event;

namespace WebKazmirenkoStudio.Pages.RawMaterial
{
    /// <summary>
    /// Создание новой записи сырья
    /// </summary>
    public class CreateModel : PageModel
    {
        #region Params Private

        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        #endregion

        #region Params Public

        /// <summary>
        /// Меры измерений
        /// </summary>
        public DropdownViewModel measureOfMeasurement { get; set; } = default!;


        /// <summary>
        /// Модель названий сырья
        /// </summary>
        public DropdownViewModel rawMaterialCaption { get; set; } = default!;

        /// <summary>
        /// Модель закупок
        /// </summary>
        public DropdownViewModel purchase { get; set; } = default!;

        /// <summary>
        /// Основная модель
        /// </summary>
        [BindProperty]
        public RawMaterialModel RawMaterial { get; set; } = default!;

        #endregion

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
            GetRawMaterialCaptionCollection();
            GetPurchaseCollection();
            SetMeasureOfMeasurementList();
            return Page();
        }

        /// <summary>
        /// Создание новой записи
        /// </summary>
        /// <param name="rawMaterialCaption">Название материала</param>
        /// <param name="purchase">Закупка</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel rawMaterialCaption, DropdownViewModel purchase, DropdownViewModel measureOfMeasurement)
        {
            SetBindingEntity(
                new Guid(rawMaterialCaption.SelectedItem), 
                new Guid(purchase.SelectedItem), 
                new Guid(measureOfMeasurement.SelectedItem)
            );

            _context.RawMaterial.Add(RawMaterial);
            await _context.SaveChangesAsync();
            RecalculationQuantityGoodsHelper.Recalculation(_context, new Guid(rawMaterialCaption.SelectedItem), RawMaterial.Quantity);
            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

        /// <summary>
        /// Получение коллекции записей названий материалов
        /// </summary>
        private void GetRawMaterialCaptionCollection()
        {
            rawMaterialCaption = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.RawMaterialCaption entity in _context.RawMaterialCaption.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            rawMaterialCaption.SelectedItem = list.FirstOrDefault().Value;
            rawMaterialCaption.Collection = list;
        }

        /// <summary>
        /// Получение коллекции записей закупок
        /// </summary>
        private void GetPurchaseCollection()
        {
            purchase = new DropdownViewModel();
            List<SelectListItem> list = new();
            foreach (WebKazmirenkoStudio.Model.Purchase entity in _context.Purchase.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            purchase.SelectedItem = list.FirstOrDefault().Value;
            purchase.Collection = list;
        }
        
        /// <summary>
        /// Получение коллекции меры измерений
        /// </summary>
        private void SetMeasureOfMeasurementList()
        {
            measureOfMeasurement = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.MeasureOfMeasurement entity in _context.MeasureOfMeasurement.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }

            measureOfMeasurement.SelectedItem = list.FirstOrDefault().Value;
            measureOfMeasurement.Collection = list;
        }

        /// <summary>
        /// Установка связанных элементов
        /// </summary>
        /// <param name="rawMaterialCaption">Название материала</param>
        /// <param name="purchase">Закупка</param>
        /// <param name="measureOfMeasurement">Мера измерений</param>
        private void SetBindingEntity(Guid rawMaterialCaption, Guid purchase, Guid measureOfMeasurement)
        {
            RawMaterial.RawMaterialCaption = _context.RawMaterialCaption.Where(x => x.Id.Equals(rawMaterialCaption)).FirstOrDefault();
            RawMaterial.Purchase = _context.Purchase.Where(x => x.Id.Equals(purchase)).FirstOrDefault();
            RawMaterial.MeasureOfMeasurement = _context.MeasureOfMeasurement.Where(x => x.Id.Equals(measureOfMeasurement)).FirstOrDefault();
        }
        #endregion
    }
}
