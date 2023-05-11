using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Details : PageModel
    {
        public CategoriaModel CatModel { get; set; } = new();
        public Details()
        {
            
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null) {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Categoria/{id}";

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                
                var content = await response.Content.ReadAsStringAsync();
                CatModel = JsonConvert.DeserializeObject<CategoriaModel>(content)!;
            }

            if(CatModel == null) {
                return NotFound();
            }

            return Page();
        }
    }
}