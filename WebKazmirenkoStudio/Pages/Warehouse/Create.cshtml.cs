using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Warehouse
{
    /// <summary>
    /// Создание новой записи
    /// </summary>
    public class CreateModel : PageModel
    {
        #region Params Private

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        #endregion

        #region Method Public

        /// <summary>
        /// Сущность склад
        /// </summary>
        [BindProperty]
        public WarehouseModel WarehouseModel { get; set; } = default!;

        /// <summary>
        /// Наименование сырья
        /// </summary>
        public DropdownViewModel RawMaterialCaption { get; set; } = default!;

        /// <summary>
        /// Мера измерений
        /// </summary>
        public DropdownViewModel MeasureOfMeasurement { get; set; } = default!;

        #endregion

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
        /// Получение нужных данных для отображения
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            GetRawMaterialCollection();
            GetMeasureOfMeasurementCollection();
            return Page();
        }

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="rawMaterialCaption">Сырье</param>
        /// <param name="measureOfMeasurement">Мера измерения</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel rawMaterialCaption, DropdownViewModel measureOfMeasurement)
        {
            if (!ModelState.IsValid || _context.Warehouse == null || WarehouseModel == null)
            {
                return Page();
            }

            SetBindingEntity(new Guid(rawMaterialCaption.SelectedItem), new Guid(measureOfMeasurement.SelectedItem));

            _context.Warehouse.Add(WarehouseModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

        /// <summary>
        /// Получения коллекции сырья и текущую запись сырья
        /// </summary>
        private void GetRawMaterialCollection()
        {
            RawMaterialCaption = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (
                WebKazmirenkoStudio.Model.RawMaterialCaption entity
                in
                _context.RawMaterialCaption
                .ToList()
            )
            {
                list.Add(new SelectListItem()
                {
                    Text = entity.Caption,
                    Value = entity.Id.ToString()
                });
            }

            RawMaterialCaption.SelectedItem = list.FirstOrDefault().Value;
            RawMaterialCaption.Collection = list;

        }

        /// <summary>
        /// Получения коллекции меры измерения и текущую запись
        /// </summary>
        private void GetMeasureOfMeasurementCollection()
        {
            MeasureOfMeasurement = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.MeasureOfMeasurement entity in _context.MeasureOfMeasurement.ToList())
            {
                list.Add(new SelectListItem()
                {
                    Text = entity.Caption,
                    Value = entity.Id.ToString()
                });
            }

            MeasureOfMeasurement.SelectedItem = list.FirstOrDefault().Value;
            MeasureOfMeasurement.Collection = list;
        }

        /// <summary>
        /// Установка связанных элементов
        /// </summary>
        /// <param name="rawMaterial">Сырье</param>
        /// <param name="measureOfMeasurement">Мера измерения</param>
        private void SetBindingEntity(Guid rawMaterial, Guid measureOfMeasurement)
        {
            WarehouseModel.RawMaterialCaption = _context.RawMaterialCaption.Where(x => x.Id.Equals(rawMaterial)).FirstOrDefault();
            WarehouseModel.MeasureOfMeasurement = _context.MeasureOfMeasurement.Where(x => x.Id.Equals(measureOfMeasurement)).FirstOrDefault();
        }

        #endregion
    }
}
