using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Warehouse
{
    /// <summary>
    /// Страница редактирования сущности склада
    /// </summary>
    public class EditModel : PageModel
    {
        #region Params private

        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        #endregion

        #region Params public

        /// <summary>
        /// Сущность склада
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
        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Public

        /// <summary>
        /// Получение сущности и связанных сущностей склада
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Warehouse == null)
            {
                return NotFound();
            }

            var warehousemodel =  await _context.Warehouse
                .Include(x => x.MeasureOfMeasurement)
                .Include(x => x.RawMaterialCaption)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (warehousemodel == null)
            {
                return NotFound();
            }

            WarehouseModel = warehousemodel;

            GetRawMaterialCollection();
            GetMeasureOfMeasurementCollection();

            return Page();
        }

        /// <summary>
        /// Сохранение сущности
        /// </summary>
        /// <param name="rawMaterial">Сырье</param>
        /// <param name="measureOfMeasurement">Мера измерений</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel rawMaterial, DropdownViewModel measureOfMeasurement)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WarehouseModel).State = EntityState.Modified;
            SetBindingEntity(new Guid(rawMaterial.SelectedItem), new Guid(measureOfMeasurement.SelectedItem));
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseModelExists(WarehouseModel.Id))
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
        /// Проверка сущестования записи
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Запись существует</returns>
        private bool WarehouseModelExists(Guid id)
        {
          return (_context.Warehouse?.Any(e => e.Id == id)).GetValueOrDefault();
        }

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
                list.Add(new SelectListItem() {
                    Text = $"entity.Caption", 
                    Value = entity.Id.ToString() 
                });
            }

            RawMaterialCaption.SelectedItem = WarehouseModel.RawMaterialCaption.Id.ToString();
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

            MeasureOfMeasurement.SelectedItem = WarehouseModel.MeasureOfMeasurement.Id.ToString();
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
