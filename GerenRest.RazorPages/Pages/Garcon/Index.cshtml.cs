using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
            using (var httpClient = new HttpClient())
            {
                string url = "http://localhost:5239/Garcon";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                GarconModel = JsonConvert.DeserializeObject<List<GarconModel>>(content)!;
            }

            return Page();
        }
    }
}