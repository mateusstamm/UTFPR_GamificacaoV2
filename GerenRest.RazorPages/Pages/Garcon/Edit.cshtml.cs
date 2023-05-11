using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Edit : PageModel
    {
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new();
        public Edit()
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