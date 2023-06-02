using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model.Contact;

namespace WebKazmirenkoStudio.Pages.Contact
{
    public class DetailsModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public DetailsModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

      public ContactModel ContactModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contactmodel = await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);
            if (contactmodel == null)
            {
                return NotFound();
            }
            else 
            {
                ContactModel = contactmodel;
            }
            return Page();
        }
    }
}
