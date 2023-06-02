using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebKazmirenkoStudio.Pages.RawMaterialCaption
{
    public class CreateModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public CreateModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WebKazmirenkoStudio.Model.RawMaterialCaption RawMaterialCaptionModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RawMaterialCaption == null || RawMaterialCaptionModel == null)
            {
                return Page();
            }

            _context.RawMaterialCaption.Add(RawMaterialCaptionModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
