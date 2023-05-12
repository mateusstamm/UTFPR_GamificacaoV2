using System.Net.Http.Headers;
using System.Text;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Produto
{
    public class Create : PageModel
    {
        [BindProperty]
        public ProdutoModel ProdModel { get; set; } = new();
        public List<CategoriaModel>? CatModel { get; set; } = new();
        public Create()
        {
            
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonData = JsonConvert.SerializeObject(ProdModel);

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://localhost:5239/Produto";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
        }

        public async Task<IActionResult> OnGetAsync() {
            
            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Categoria";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                CatModel = JsonConvert.DeserializeObject<List<CategoriaModel>>(content)!;
            }
            
            return Page();
        }
    }
}