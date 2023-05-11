using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GerenRest.RazorPages.Pages.Garcon
{
    public class Index : PageModel
    {
        public List<GarconModel> GarconModel { get; set; } = new();
        public Index()
        {
            
        }
        public async Task<IActionResult> OnGetAsync()
        {
            
            return Page();
        }
    }
}