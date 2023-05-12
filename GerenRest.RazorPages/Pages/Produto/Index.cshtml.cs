using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Index : PageModel
    {
        public List<ProdutoModel> ProdModel { get; set; } = new();
        public Index()
        {
            
        }
        public async Task<IActionResult> OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string url = "http://localhost:5239/Produto";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                
                ProdModel = JsonConvert.DeserializeObject<List<ProdutoModel>>(content)!;
            }

            return Page();
        }
    }
}