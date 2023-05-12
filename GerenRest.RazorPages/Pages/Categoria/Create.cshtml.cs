using System.Net.Http.Headers;
using System.Text;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Categoria
{
    public class Create : PageModel
    {

        [BindProperty]
        public CategoriaModel CatModel { get; set; } = new();
        public Create()
        {
            
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string jsonData = JsonConvert.SerializeObject(CatModel);

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://localhost:5239/Categoria";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

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