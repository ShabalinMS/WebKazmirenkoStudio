using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Purchase
{
    /// <summary>
    /// Добавление новой записи закупка
    /// </summary>
    public class CreateModel : PageModel
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
            GetShopCollection();
            return Page();
        }

        /// <summary>
        /// Привязка данных закупка
        /// </summary>
        [BindProperty]
        public WebKazmirenkoStudio.Model.Purchase Purchase { get; set; } = default!;
        

        /// <summary>
        /// Добавление новой записи закупка
        /// </summary>
        /// <param name="shopEntity">Cущность магазин</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(DropdownViewModel shop)
        {
            SetBindingEntity(new Guid(shop.SelectedItem));

            _context.Purchase.Add(Purchase);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        #endregion

        #region Method Private

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
            Shop.SelectedItem = list.FirstOrDefault().Value;
            Shop.Collection = list;
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
