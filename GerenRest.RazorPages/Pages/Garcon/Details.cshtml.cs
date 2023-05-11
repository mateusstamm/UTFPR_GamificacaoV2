using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Details : PageModel
    {
        public GarconModel GarconModel { get; set; } = new();
        public Details()
        {
            
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            return Page();
        }
    }
}