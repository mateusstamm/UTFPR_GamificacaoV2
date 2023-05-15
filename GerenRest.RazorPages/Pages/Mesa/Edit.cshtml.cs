using System.Net.Http.Headers;
using System.Text;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Edit : PageModel
    {
        [BindProperty]
        public MesaModel? MesaModel { get; set; }
        public Edit()
        {
            
        }

        public async Task<IActionResult> OnGetAsync(int? id) {
            
            if(id == null) {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Mesa/{id}";
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                var content = await response.Content.ReadAsStringAsync();
                MesaModel = JsonConvert.DeserializeObject<MesaModel>(content)!;
            }

            if(MesaModel == null) {
                return NotFound();
            }

            MesaModel!.Ocupada = "Livre";
            MesaModel!.HoraAbertura = null;
            string jsonData = JsonConvert.SerializeObject(MesaModel);

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"http://localhost:5239/Mesa/{id}";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

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
    }
}