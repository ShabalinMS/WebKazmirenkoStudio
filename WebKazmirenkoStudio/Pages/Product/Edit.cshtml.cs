﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext _context;

        public EditModel(WebKazmirenkoStudio.Data.WebKazmirenkoStudioContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductModel ProductModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productmodel =  await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (productmodel == null)
            {
                return NotFound();
            }
            ProductModel = productmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(ProductModel.Id))
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

        private bool ProductModelExists(Guid id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
