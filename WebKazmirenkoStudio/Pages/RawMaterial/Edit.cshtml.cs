using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;
using System.Linq;

namespace WebKazmirenkoStudio.Pages.RawMaterial
{
    /// <summary>
    /// Страница редактирования сырья
    /// </summary>
    public class EditModel : PageModel
    {
        #region View params 
        
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

        #endregion

        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Коллекция сырья
        /// </summary>
        [BindProperty]
        public WebKazmirenkoStudio.Model.RawMaterial RawMaterial { get; set; } = default!;

        /// <summary>
        /// Получить запись
        /// </summary>
        /// <param name="id">Id записи</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.RawMaterial == null)
            {
                return NotFound();
            }

            var rawmaterial =  await _context.RawMaterial
                .Include(x=>x.RawMaterialCaption)
                .Include(x => x.Purchase)
                .Include(x=>x.MeasureOfMeasurement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rawmaterial == null)
            {
                return NotFound();
            }

            await SetRawMaterialCaptionList(rawmaterial);
            await SetPurchaseList(rawmaterial);
            await SetMeasureOfMeasurementList(rawmaterial);

            RawMaterial = rawmaterial;
            

            return Page();
        }

        /// <summary>
        /// Сохранение записи
        /// </summary>
        /// <param name="rawMaterialCaption">Название материала</param>
        /// <param name="purchase">Закупка</param>
        /// <param name="measureOfMeasurement">Мера измерения</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel rawMaterialCaption, DropdownViewModel purchase, DropdownViewModel measureOfMeasurement)
        {
            _context.Attach(RawMaterial).State = EntityState.Modified;

            SetBindingEntity(
                new Guid(rawMaterialCaption.SelectedItem), 
                new Guid(purchase.SelectedItem), 
                new Guid(measureOfMeasurement.SelectedItem)
            );

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RawMaterialExists(RawMaterial.Id))
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

        #region Method Private

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

        /// <summary>
        /// Проверка сущности на существование
        /// </summary>
        /// <param name="id">ID записи</param>
        /// <returns>Существование записи</returns>
        private bool RawMaterialExists(Guid id)
        {
          return (_context.RawMaterial?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Получить список наименований сырья
        /// </summary>
        private async Task SetRawMaterialCaptionList(WebKazmirenkoStudio.Model.RawMaterial rawmaterial)
        {
            rawMaterialCaption = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.RawMaterialCaption entity in _context.RawMaterialCaption.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }

            rawMaterialCaption.SelectedItem = rawmaterial.RawMaterialCaption.Id.ToString();
            rawMaterialCaption.Collection = list;
        }

        /// <summary>
        /// Установка коллекции меры измерений
        /// </summary>
        /// <returns></returns>
        private async Task SetMeasureOfMeasurementList(WebKazmirenkoStudio.Model.RawMaterial rawmaterial)
        {
            measureOfMeasurement = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.MeasureOfMeasurement entity in _context.MeasureOfMeasurement.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }

            measureOfMeasurement.SelectedItem = rawmaterial.MeasureOfMeasurement.Id.ToString();
            measureOfMeasurement.Collection = list;
        }

        /// <summary>
        /// Установить коллекция записей закупок
        /// </summary>
        /// <returns></returns>
        private async Task SetPurchaseList(WebKazmirenkoStudio.Model.RawMaterial rawmaterial)
        {
            purchase = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.Purchase entity in _context.Purchase.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }

            purchase.SelectedItem = rawmaterial.Purchase.Id.ToString();
            purchase.Collection = list;
        }

        #endregion
    }
}
