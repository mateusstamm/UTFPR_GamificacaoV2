using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Delete : PageModel
    {
        [BindProperty]
        public GarconModel GarconModel { get; set; } = new();
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