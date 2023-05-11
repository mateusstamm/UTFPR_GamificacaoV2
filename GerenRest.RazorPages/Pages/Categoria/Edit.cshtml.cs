using System.Net.Http.Headers;
using System.Text;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Edit : PageModel
    {
        [BindProperty]
        public CategoriaModel CatModel { get; set; } = new();
        public Edit()
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
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonData = JsonConvert.SerializeObject(CatModel);

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"http://localhost:5239/Categoria/{id}";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Categoria/Index");
                }               
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
    }
}