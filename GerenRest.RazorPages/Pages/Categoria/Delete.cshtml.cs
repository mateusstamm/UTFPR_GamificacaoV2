using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Delete : PageModel
    {
        [BindProperty]
        public CategoriaModel CatModel { get; set; } = new();
        public Delete()
        {

        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            return Page();
        }
    }
}