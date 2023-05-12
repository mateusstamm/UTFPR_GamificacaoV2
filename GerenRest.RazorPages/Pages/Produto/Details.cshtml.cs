using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Details : PageModel
    {
        public ProdutoModel ProdModel { get; set; } = new();
        public Details()
        {
            
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Produto/{id}";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                ProdModel = JsonConvert.DeserializeObject<ProdutoModel>(content)!;
            }

            return Page();
        }
    }
}