using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Index : PageModel
    {
        public List<CategoriaModel> CategoriaList { get; set; } = new();
        public Index()
        {
            
        }
        public async Task<IActionResult> OnGetAsync()
        {
            //CategoriaList = await _context.Categorias!.ToListAsync();
            using (var httpClient = new HttpClient())
            {
                var url = $"http://localhost:5239/categoria";
                var response = await httpClient.GetAsync(url);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    CategoriaList = JsonConvert.DeserializeObject<List<CategoriaModel>>(content)!;
                }

            }
            return Page();
        }
    }
}