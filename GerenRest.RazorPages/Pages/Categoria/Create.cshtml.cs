using GerenRest.RazorPages.Data;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Create : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public CategoriaModel CatModel { get; set; } = new();
        public Create(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            try {
                _context.Add(CatModel);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Categoria/Index");
            } catch(DbUpdateException) {
                return Page();
            }
        }
    }
}