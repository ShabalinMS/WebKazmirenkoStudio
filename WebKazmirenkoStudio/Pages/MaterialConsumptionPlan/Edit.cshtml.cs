using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionPlan
{
    /// <summary>
    /// Редактирование планового расхода на товар
    /// </summary>
    public class EditModel : PageModel
    {

        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Прановый расход модель
        /// </summary>
        [BindProperty]
        public MaterialConsumptionPlanModel MaterialConsumptionPlanModel { get; set; } = default!;

        /// <summary>
        /// Склад
        /// </summary>
        public DropdownViewModel rawMaterialCaption { get; set; } = default!;

        /// <summary>
        /// Продукт
        /// </summary>
        public DropdownViewModel product { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Текущий контекст</param>
        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        #region Method Public

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="id">ID пранового расхода</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionPlan == null)
            {
                return NotFound();
            }

            var materialconsumptionplanmodel =  await _context.MaterialConsumptionPlan.FirstOrDefaultAsync(m => m.Id == id);
            if (materialconsumptionplanmodel == null)
            {
                return NotFound();
            }
            GetWarehouseCollection();
            GetProductCollection();
            MaterialConsumptionPlanModel = materialconsumptionplanmodel;
            return Page();
        }

        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="rawMaterialCaption">Наименование сыптя</param>
        /// <param name="product">Продукт</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel rawMaterialCaption, DropdownViewModel product)
        {
            _context.Attach(MaterialConsumptionPlanModel).State = EntityState.Modified;

            try
            {
                SetBindingEntity(new Guid(rawMaterialCaption.SelectedItem), new Guid(product.SelectedItem));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialConsumptionPlanModelExists(MaterialConsumptionPlanModel.Id))
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
        /// Проверка существования записи
        /// </summary>
        /// <param name="id">ID планового расхода</param>
        /// <returns></returns>
        private bool MaterialConsumptionPlanModelExists(Guid id)
        {
          return (_context.MaterialConsumptionPlan?.Any(e => e.Id == id)).GetValueOrDefault();
        }

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
