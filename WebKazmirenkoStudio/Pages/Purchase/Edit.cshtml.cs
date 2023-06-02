using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Purchase
{
    /// <summary>
    /// Изменение записи закупка
    /// </summary>
    public class EditModel : PageModel
    {
        /// <summary>
        /// Текущий контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Магазины
        /// </summary>
        public DropdownViewModel Shop { get; set; } = default!;

        /// <summary>
        /// Сущность закупки
        /// </summary>
        [BindProperty]
        public WebKazmirenkoStudio.Model.Purchase Purchase { get; set; } = default!;


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
        /// Получение параметров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase =  await _context.Purchase.Include(x =>x.Shop).FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }
            Purchase = purchase;
            GetShopCollection();
            return Page();
        }

        /// <summary>
        /// Сохранение записи
        /// </summary>
        /// <param name="shopId">ID магазина</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel shop)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Purchase).State = EntityState.Modified;

            try
            {
                SetBindingEntity(new Guid(shop.SelectedItem));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(Purchase.Id))
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

        #region Mrthod Private

        /// <summary>
        /// Получение коллекции записей закупок
        /// </summary>
        private void GetShopCollection()
        {
            Shop = new DropdownViewModel();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (WebKazmirenkoStudio.Model.ShopModel entity in _context.Shop.ToList())
            {
                list.Add(new SelectListItem() { Text = entity.Caption, Value = entity.Id.ToString() });
            }
            Shop.SelectedItem = Purchase.Shop.Id.ToString();
            Shop.Collection = list;
        }

        /// <summary>
        /// Проверка существования записи
        /// </summary>
        /// <param name="id">Id записи</param>
        /// <returns>Запись существует</returns>
        private bool PurchaseExists(Guid id)
        {
          return (_context.Purchase?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Установка связанных элементов
        /// </summary>
        /// <param name="shopId">ID shop</param>
        private void SetBindingEntity(Guid shopId)
        {
            Purchase.Shop = _context.Shop.Where(x => x.Id.Equals(shopId)).FirstOrDefault();
            Purchase.Caption = $"{Purchase.Shop.Caption} {Purchase.Date.ToLongDateString()}";
        }

        #endregion
    }
}
