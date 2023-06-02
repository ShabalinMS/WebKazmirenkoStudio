using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model.MaterialConsumptionFact;

namespace WebKazmirenkoStudio.Pages.MaterialConsumptionFact
{
    /// <summary>
    /// Удаление фактического расхода
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        /// <summary>
        /// Фактический расход
        /// </summary>
        [BindProperty]
        public MaterialConsumptionFactModel MaterialConsumptionFactModel { get; set; } = default!;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст</param>
        public DeleteModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="id">ID фактического расхода</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionFact == null)
            {
                return NotFound();
            }

            var materialconsumptionfactmodel = await _context.MaterialConsumptionFact
                .Include(x => x.MaterialConsumptionPlan)
                .Include(x => x.Order)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (materialconsumptionfactmodel == null)
            {
                return NotFound();
            }
            else 
            {
                MaterialConsumptionFactModel = materialconsumptionfactmodel;
            }
            return Page();
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.MaterialConsumptionFact == null)
            {
                return NotFound();
            }
            var materialconsumptionfactmodel = await _context.MaterialConsumptionFact.FindAsync(id);

            if (materialconsumptionfactmodel != null)
            {
                MaterialConsumptionFactModel = materialconsumptionfactmodel;
                _context.MaterialConsumptionFact.Remove(MaterialConsumptionFactModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
