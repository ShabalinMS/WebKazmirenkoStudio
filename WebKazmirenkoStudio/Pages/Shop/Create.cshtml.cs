﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebKazmirenkoStudio.Data;
using WebKazmirenkoStudio.Model;

namespace WebKazmirenkoStudio.Pages.Shop
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
        public ShopModel ShopModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Shop == null || ShopModel == null)
            {
                return Page();
            }

            _context.Shop.Add(ShopModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
