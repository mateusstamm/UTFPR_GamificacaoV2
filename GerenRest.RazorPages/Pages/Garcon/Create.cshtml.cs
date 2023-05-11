using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Create : PageModel
    {
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new();
        public Create()
        {
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            return Page();
        }
    }
}