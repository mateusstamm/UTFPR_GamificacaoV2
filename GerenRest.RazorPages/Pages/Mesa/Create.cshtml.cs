using System.Net.Http.Headers;
using System.Text;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Mesa
{
    public class Create : PageModel
    {
        [BindProperty]
        public MesaModel MesaModel { get; set; } = new();
        public List<MesaModel>? ListMesa { get; set; } = new();
        public Create()
        {

        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(!ModelState.IsValid)
                return Page();

            using (var httpClient = new HttpClient())
            {
                string url = "http://localhost:5239/Mesa";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                ListMesa = JsonConvert.DeserializeObject<List<MesaModel>>(content)!;
            }

            if(ListMesa != null) {
                foreach(var listMesa in ListMesa) {
                    if(listMesa.Numero == MesaModel.Numero) {
                        TempData["ErroMesa"] = $"Já existe a mesa {MesaModel.Numero}!";
                        return RedirectToPage("/Mesa/Create");
                    }
                }
            }

            MesaModel.Ocupada = "Livre";
            MesaModel.HoraAbertura = DateTime.Now;
            string jsonData = JsonConvert.SerializeObject(MesaModel);

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://localhost:5239/Mesa";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Mesa/Index");
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
    }
}